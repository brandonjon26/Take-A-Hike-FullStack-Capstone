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
        public List<Hike> GetAllHikes()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT h.id, h.userId, h.parkId, h.dateOfHike,
                               u.id AS hikeUserId, u.firstName, u.lastName, u.email, u.FirebaseUserId, u.userTypeId,
                               p.id AS parkUserId, p.parkName, p.description, p.contactInfo, p.imageURL, p.address, p.websiteLink, p.isDeleted AS parkIsDeleted

                        FROM myHikes h
                        LEFT JOIN users u ON u.id = h.userId
                        JOIN parks p ON p.id = h.parkId

                        WHERE p.isDeleted = 0
                        ORDER BY dateOfHike
                        ";

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
                                firstName = DbUtils.GetString(reader, "firstName"),
                                lastName = DbUtils.GetString(reader, "lastName"),
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
                            DateOfHike = DbUtils.GetDateTime(reader, "dateOfHike")
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
                                SELECT id, parkId, userId, dateOfHike
                                FROM myHikes
                                WHERE id = @id";
                    DbUtils.AddParameter(cmd, "@id", id);

                    var reader = cmd.ExecuteReader();

                    Hike hike = null;
                    if (reader.Read())
                    {
                        hike = new Hike()
                        {
                            Id = DbUtils.GetInt(reader, "id"),
                            ParkId = DbUtils.GetInt(reader, "parkId"),
                            UserId = DbUtils.GetInt(reader, "userId"),
                            DateOfHike = DbUtils.GetDateTime(reader, "dateOdHike")
                        };
                    }
                    reader.Close();
                    return hike;
                }
            }
        }
    }
}
