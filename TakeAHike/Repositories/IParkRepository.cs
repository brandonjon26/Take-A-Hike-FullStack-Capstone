using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TakeAHike.Models;

namespace TakeAHike.Repositories
{
    public interface IParkRepository
    {
        List<Park> GetAllParks();
    }
}
