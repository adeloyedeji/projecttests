using ProjectTest.Libs.Models;
using System.Threading.Tasks;

namespace ProjectTest.Libs.Contracts
{
    public interface IUser
    {
        Task<User> GetAsync(string id);
        Task<User> CreateAsync(User user);
        Task<User> UpdateAsync(User user);
        Task<bool> ExistsAsync(string id);
        Task<bool> DeleteAsync(string id);
    }
}
