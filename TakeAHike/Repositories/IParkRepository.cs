using System.Collections.Generic;
using TakeAHike.Models;

namespace TakeAHike.Repositories
{
    public interface IParkRepository
    {
        List<Park> GetAllParks();
        public void AddPark(Park park);
        public Park GetById(int id);
        public void UpdatePark(Park park);
        public void Delete(int id);
    }
}
