using TakeAHike.Models;

namespace TakeAHike.Repositories
{
    public interface IUsersRepository
    {
        public void Add(Users users);
        public Users GetByFirebaseUserId(string firebaseUserId);
    }
}
