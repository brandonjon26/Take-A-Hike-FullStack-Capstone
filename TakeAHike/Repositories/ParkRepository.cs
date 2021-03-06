using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tabloid.Utils;
using TakeAHike.Models;

namespace TakeAHike.Repositories
{
    public class ParkRepository : BaseRepository, IParkRepository
    {
        public ParkRepository(IConfiguration configuration) : base(configuration) { }

        public List<Park> GetAllParks()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT id, parkName, description, contactInfo, 
                               imageUrl, address, websiteLink, isDeleted
                        FROM parks 
                        WHERE isDeleted = 0";
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Park> Parks = new List<Park>() { };
                    Park park = null;
                    while (reader.Read())
                    {
                        park = new Park()
                        {
                            Id = DbUtils.GetInt(reader, "id"),
                            ParkName = DbUtils.GetString(reader, "parkName"),
                            Description = DbUtils.GetString(reader, "description"),
                            ContactInfo = DbUtils.GetString(reader, "contactInfo"),
                            ImageUrl = DbUtils.GetString(reader, "imageUrl"),
                            Address = DbUtils.GetString(reader, "address"),
                            WebsiteLink = DbUtils.GetString(reader, "websiteLink"),
                            isDeleted = DbUtils.GetBool(reader, "isDeleted")
                        };
                        Parks.Add(park);
                    }
                    reader.Close();
                    return Parks;
                }
            }
        }

        public void AddPark(Park park)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO parks (parkName, description, contactInfo, imageURL, address, websiteLink)
                                        OUTPUT INSERTED.ID
                                        VALUES (@parkName, @description, @contactInfo, @imageUrl, @address, @websiteLink)
                    ";

                    DbUtils.AddParameter(cmd, "@parkName", park.ParkName);
                    DbUtils.AddParameter(cmd, "@description", park.Description);
                    DbUtils.AddParameter(cmd, "@contactInfo", park.ContactInfo);
                    DbUtils.AddParameter(cmd, "@imageURL", park.ImageUrl);
                    DbUtils.AddParameter(cmd, "@address", park.Address);
                    DbUtils.AddParameter(cmd, "@websiteLink", park.WebsiteLink);

                    int id = (int)cmd.ExecuteScalar();

                    park.Id = id;
                }
            }
        }

        public Park GetById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                SELECT id, parkName, description, contactInfo, imageURL, address, websiteLink, isDeleted
                                FROM parks
                                WHERE id = @id";

                    DbUtils.AddParameter(cmd, "@id", id);

                    var reader = cmd.ExecuteReader();

                    Park park = null;
                    if (reader.Read())
                    {
                        park = new Park()
                        {
                            Id = DbUtils.GetInt(reader, "id"),
                            ParkName = DbUtils.GetString(reader, "parkName"),
                            Description = DbUtils.GetString(reader, "description"),
                            ContactInfo = DbUtils.GetString(reader, "contactInfo"),
                            ImageUrl = DbUtils.GetString(reader, "imageUrl"),
                            Address = DbUtils.GetString(reader, "address"),
                            WebsiteLink = DbUtils.GetString(reader, "websiteLink"),
                            isDeleted = DbUtils.GetBool(reader, "isDeleted")

                        };
                    }
                    reader.Close();
                    return park;
                }
            }
        }

        public void UpdatePark(Park park)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            UPDATE parks
                                SET parkName = @parkName,
                                    description = @description,
                                    contactInfo = @contactInfo,
                                    imageURL = @imageURL,
                                    address = @address,
                                    websiteLink = @websiteLink
                            WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@parkName", park.ParkName);
                    DbUtils.AddParameter(cmd, "@description", park.Description);
                    DbUtils.AddParameter(cmd, "@contactInfo", park.ContactInfo);
                    DbUtils.AddParameter(cmd, "@imageURL", park.ImageUrl);
                    DbUtils.AddParameter(cmd, "@address", park.Address);
                    DbUtils.AddParameter(cmd, "@websiteLink", park.WebsiteLink);
                    DbUtils.AddParameter(cmd, "@Id", park.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE parks
                                        SET isDeleted = 1
                                        WHERE Id = @id";

                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Activate(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE parks
                                        SET isDeleted = 0
                                        WHERE Id = @id";

                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
