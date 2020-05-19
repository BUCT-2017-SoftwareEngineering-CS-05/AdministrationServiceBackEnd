using AdministrationServiceBackEnd.DtoParameters;
using AdministrationServiceBackEnd.Helpers;
using AdministrationServiceBackEnd.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdministrationServiceBackEnd.Services
{
    interface IManageUser
    {
        Task<PagedList<User>> GetCompaniesAsync(UserDtoParameters parameters);
        IEnumerable<User> GetAllUsers();
        User GetUserById(string id);
        void MuteUser(string id, int mute);
        void ChangePassword(string id, string new_pwd);
    }
}
