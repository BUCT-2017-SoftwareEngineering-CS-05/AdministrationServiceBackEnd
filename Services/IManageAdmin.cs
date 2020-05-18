using AdministrationServiceBackEnd.Models;
using System.Collections.Generic;

namespace AdministrationServiceBackEnd.Services
{
    public interface IManageAdmin
    {
        IEnumerable<Admin> GetAllAdmins();
        Admin GetAdminById(int id);
        Admin GetAdminByUsername(string username);
        void AddAdmin(string username, string pwd, int level);
        void ModifyUsername(int id, string new_name);
        void ChangePassword(int id, string password);
        void RemoveAdmin(int id);
        bool CheckPassword(string username, string password);
        int GetIdByUsername(string username);
        int GetLevelById(int id);
    }
}