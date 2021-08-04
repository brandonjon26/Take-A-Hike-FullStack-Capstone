using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TakeAHike.Models;

namespace TakeAHike.Repositories
{
    public interface IHikeRepository
    {
        List<Hike> GetAllHikes(int id);
        public void AddHike(Hike hike);
        public Hike GetById(int id);
        public void UpdateHike(Hike hike);
        public void Delete(int id);
        public void Activate(int id);
    }
}
