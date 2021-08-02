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
                        SELECT h.id, h.userId, h.parkId, h.dateOdHike,
                               u.id AS hikeUserId, u.firstName, u.lastName, u.email, u.userTypeId,
                               p.id AS parkUserId, p.parkName, p.description, p.contactInfo, p.imageURL, p.address, p.websiteLink, p.isDeleted AS parkIsDeleted

                        FROM myHikes h
                        LEFT JOIN users u ON u.id = h.userId
                        JOIN parks p ON p.id = h.parkId

                        WHERE p.isDeleted = 0
                        ORDER BY dateOfHike
                        ";

                    var reader = cmd.ExecuteReader();

                    var hikes = new List<Hike>();
                    while (reader.Read())
                    {
                        hikes.Add(new Hike()
                        {
                            Id = DbUtils.GetInt(reader, "id"),
                            UserId = DbUtils.GetInt(reader, "userId"),
                            users = new Users()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                FireBaseUserId = DbUtils.GetString(reader, "FireBaseUserId"),
                                firstName = DbUtils.GetString(reader, "firstName"),
                                lastName = DbUtils.GetString(reader, "lastName"),
                                email = DbUtils.GetString(reader, "email"),
                                userTypeId = DbUtils.GetInt(reader, "userTypeId"),
                            },
                            ParkId = DbUtils.GetInt(reader, "parkId"),
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
                            },
                            DateOfHike = DbUtils.GetDateTime(reader, "dateOfHike")
                        });
                    }

                    reader.Close();

                    return hikes;
                }
            }
        }
    }
}
