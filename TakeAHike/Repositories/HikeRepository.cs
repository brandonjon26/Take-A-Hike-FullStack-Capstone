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
    public class HikeRepository : BaseRepository, IHikeRepository
    {
        public HikeRepository(IConfiguration configuration) : base(configuration) { }
        public List<Hike> GetAllHikes(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT h.id, h.userId, h.parkId, h.dateOfHike, h.isDeleted,
                               u.id AS hikeUserId, u.name, u.email, u.FirebaseUserId, u.userTypeId,
                               p.id AS parkUserId, p.parkName, p.description, p.contactInfo, p.imageURL, p.address, p.websiteLink, p.isDeleted AS parkIsDeleted

                        FROM myHikes h
                        LEFT JOIN users u ON u.id = h.userId
                        JOIN parks p ON p.id = h.parkId

                        WHERE h.isDeleted = 0 AND u.id = @id
                        ORDER BY dateOfHike
                        ";

                    DbUtils.AddParameter(cmd, "@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Hike> hikes = new List<Hike>();
                    while (reader.Read())
                    {
                        hikes.Add(new Hike()
                        {
                            Id = DbUtils.GetInt(reader, "id"),
                            UserId = DbUtils.GetInt(reader, "userId"),
                            Users = new Users()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                FireBaseUserId = DbUtils.GetString(reader, "FireBaseUserId"),
                                name = DbUtils.GetString(reader, "name"),
                                email = DbUtils.GetString(reader, "email"),
                                userTypeId = DbUtils.GetInt(reader, "userTypeId"),
                            },
                            ParkId = DbUtils.GetInt(reader, "parkId"),
                            Park = new Park()
                            {
                                Id = DbUtils.GetInt(reader, "id"),
                                ParkName = DbUtils.GetString(reader, "parkName"),
                                Description = DbUtils.GetString(reader, "description"),
                                ContactInfo = DbUtils.GetString(reader, "contactInfo"),
                                ImageUrl = DbUtils.GetString(reader, "imageUrl"),
                                Address = DbUtils.GetString(reader, "address"),
                                WebsiteLink = DbUtils.GetString(reader, "websiteLink"),
                                isDeleted = DbUtils.GetBool(reader, "parkIsDeleted")
                            },
                            DateOfHike = DbUtils.GetDateTime(reader, "dateOfHike"),
                            isDeleted = DbUtils.GetBool(reader, "isDeleted")
                        });
                    }

                    reader.Close();

                    return hikes;
                }
            }
        }

        public void AddHike(Hike hike)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO myHikes (userId, parkId, dateOfHike)
                                        OUTPUT INSERTED.ID
                                        VALUES (@userId, @parkId, @dateOfHike)
                    ";

                    DbUtils.AddParameter(cmd, "@userId", hike.UserId);
                    DbUtils.AddParameter(cmd, "@parkId", hike.ParkId);
                    DbUtils.AddParameter(cmd, "@dateOfHike", hike.DateOfHike);

                    int id = (int)cmd.ExecuteScalar();

                    hike.Id = id;
                }
            }
        }

        public Hike GetById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                SELECT h.id, h.userId, h.parkId, h.dateOfHike, h.isDeleted,
                                       p.id AS parkUserId, p.parkName, p.description, p.contactInfo, p.imageURL, p.address, p.websiteLink, p.isDeleted AS parkIsDeleted
                                FROM myHikes h

                                LEFT JOIN parks p ON p.id = h.parkId

                                WHERE h.id = @id";
                    DbUtils.AddParameter(cmd, "@id", id);

                    var reader = cmd.ExecuteReader();

                    Hike hike = null;
                    if (reader.Read())
                    {
                        hike = new Hike()
                        {
                            Id = DbUtils.GetInt(reader, "id"),
                            ParkId = DbUtils.GetInt(reader, "parkId"),
                            Park = new Park()
                            {
                                Id = DbUtils.GetInt(reader, "id"),
                                ParkName = DbUtils.GetString(reader, "parkName"),
                                Description = DbUtils.GetString(reader, "description"),
                                ContactInfo = DbUtils.GetString(reader, "contactInfo"),
                                ImageUrl = DbUtils.GetString(reader, "imageUrl"),
                                Address = DbUtils.GetString(reader, "address"),
                                WebsiteLink = DbUtils.GetString(reader, "websiteLink"),
                                isDeleted = DbUtils.GetBool(reader, "parkIsDeleted")
                            },
                            UserId = DbUtils.GetInt(reader, "userId"),
                            DateOfHike = DbUtils.GetDateTime(reader, "dateOfHike"),
                            isDeleted = DbUtils.GetBool(reader, "isDeleted")
                        };
                    }
                    reader.Close();
                    return hike;
                }
            }
        }

        public void UpdateHike(Hike hike)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            UPDATE myHikes
                                SET parkId = @parkId,
                                    userId = @userId,
                                    dateOfHike = @dateOfHike
                            WHERE Id = @Id";

                    cmd.Parameters.AddWithValue("@id", hike.Id);

                    DbUtils.AddParameter(cmd, "@parkId", hike.ParkId);
                    DbUtils.AddParameter(cmd, "@userId", hike.UserId);
                    DbUtils.AddParameter(cmd, "@dateOfHike", hike.DateOfHike);

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
                    cmd.CommandText = @"UPDATE myHikes
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
                    cmd.CommandText = @"UPDATE myHikes
                                        SET isDeleted = 0
                                        WHERE Id = @id";

                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
