using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TakeAHike.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string FireBaseUserId { get; set; }
        public int userTypeId { get; set; }
        public userType userType { get; set; }
    }
}
