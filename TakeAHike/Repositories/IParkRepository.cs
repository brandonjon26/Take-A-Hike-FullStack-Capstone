using System.Collections.Generic;
using TakeAHike.Models;

namespace TakeAHike.Repositories
{
    public interface IParkRepository
    {
        List<Park> GetAllParks();
    }
}
