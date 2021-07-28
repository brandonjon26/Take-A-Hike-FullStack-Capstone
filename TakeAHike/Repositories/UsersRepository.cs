﻿using Microsoft.Extensions.Configuration;
using Tabloid.Utils;
using TakeAHike.Models;

namespace TakeAHike.Repositories
{
    public class UsersRepository : BaseRepository, IUsersRepository
    {
        public UsersRepository(IConfiguration configuration) : base(configuration) { }

        public Users GetByFirebaseUserId(string firebaseUserId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT u.Id, u.FireBaseUserId, u.firstName, u.lastName, 
                               u.email, u.userTypeId,
                               ut.name AS UserTypeName
                          FROM users u
                               LEFT JOIN userType ut on u.userTypeId = ut.Id
                         WHERE FireBaseUserId = @FireBaseUserId";

                    DbUtils.AddParameter(cmd, "@FirebaseUserId", firebaseUserId);

                    Users users = null;

                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        users = new Users()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            FireBaseUserId = DbUtils.GetString(reader, "FireBaseUserId"),
                            firstName = DbUtils.GetString(reader, "firstName"),
                            lastName = DbUtils.GetString(reader, "lastName"),
                            email = DbUtils.GetString(reader, "email"),
                            userTypeId = DbUtils.GetInt(reader, "userTypeId"),
                            userType = new userType()
                            {
                                Id = DbUtils.GetInt(reader, "UserTypeId"),
                                Name = DbUtils.GetString(reader, "UserTypeName"),
                            }
                        };
                    }
                    reader.Close();

                    return users;
                }
            }
        }

        public void Add(Users users)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO UserProfile (FireBaseUserId, firstName, lastName, 
                                                                 email, userTypeId)
                                        OUTPUT INSERTED.ID
                                        VALUES (@FireBaseUserId, @firstName, @lastName, 
                                                @email, @userTypeId)";
                    DbUtils.AddParameter(cmd, "@FireBaseUserId", users.FireBaseUserId);
                    DbUtils.AddParameter(cmd, "@firstName", users.firstName);
                    DbUtils.AddParameter(cmd, "@lastName", users.lastName);
                    DbUtils.AddParameter(cmd, "@email", users.email);
                    DbUtils.AddParameter(cmd, "@userTypeId", users.userTypeId);

                    users.Id = (int)cmd.ExecuteScalar();
                }
            }
        }
    }
}