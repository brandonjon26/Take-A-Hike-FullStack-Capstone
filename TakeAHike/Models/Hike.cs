using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TakeAHike.Models
{
    public class Hike
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ParkId { get; set; }
        public DateTime DateOfHike { get; set; }
        public Users Users { get; set; }
        public Park Park { get; set; }
        public bool isDeleted { get; set; }
    }
}
