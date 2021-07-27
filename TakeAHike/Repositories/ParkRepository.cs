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
                               imageUrl, address, websiteLink
                        From parks";
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
                            WebsiteLink = DbUtils.GetString(reader, "websiteLink")
                        };
                        Parks.Add(park);
                    }
                    reader.Close();
                    return Parks;
                }
            }
        }
    }
}
