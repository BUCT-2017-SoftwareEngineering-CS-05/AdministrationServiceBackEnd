using AdministrationServiceBackEnd.Models;
using System.Collections.Generic;

namespace AdministrationServiceBackEnd.Services
{
    interface IManageUser
    {
        IEnumerable<User> GetAllUsers();
        User GetUserById(string id);
        void MuteUser(string id, int mute);
    }
}
