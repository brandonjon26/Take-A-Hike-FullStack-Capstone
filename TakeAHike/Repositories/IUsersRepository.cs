using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TakeAHike.Models;

namespace TakeAHike.Repositories
{
    public class IUsersRepository
    {
        public void Add(Users users);
        public Users GetByFirebaseUserId(string firebaseUserId);
    }
}
