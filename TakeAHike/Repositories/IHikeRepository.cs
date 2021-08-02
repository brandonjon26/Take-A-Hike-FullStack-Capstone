using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TakeAHike.Models;

namespace TakeAHike.Repositories
{
    public interface IHikeRepository
    {
        List<Hike> GetAllHikes();
        public void AddHike(Hike hike);
    }
}
