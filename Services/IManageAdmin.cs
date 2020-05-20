using AdministrationServiceBackEnd.DtoParameters;
using AdministrationServiceBackEnd.Helpers;
using AdministrationServiceBackEnd.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdministrationServiceBackEnd.Services
{
    public interface IManageAdmin
    {
        IEnumerable<Admin> GetAllAdmins();
        Task<PagedList<Admin>> GetAdminsAsync(AdminDtoParameters parameters);
        Admin GetAdminById(int id);
        Admin GetAdminByUsername(string username);
        void AddAdmin(string username, string pwd, int roles);
        void ModifyUsername(int id, string new_name);
        void ChangePassword(int id, string password);
        void RemoveAdmin(int id);
        bool CheckPassword(string username, string password);
        int GetIdByUsername(string username);
        int GetRolesById(int id);
    }
}