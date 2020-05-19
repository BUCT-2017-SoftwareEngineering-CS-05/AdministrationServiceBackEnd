using AdministrationServiceBackEnd.Models;
using System;

namespace AdministrationServiceBackEnd.Services
{
    public static class AccountSystem
    {
        private static MuseumContext _context = new MuseumContext();
        private static IManageAdmin _manage = new ManageAdmin(_context);
        public static bool isLogged = false;
        public static int log_id;
        public static string log_username;
        public static int log_roles;
        public static bool LogIn(string username, string password)
        {
            Console.WriteLine(username + "   " + password);
            if (_manage.GetAdminByUsername(username) == null)
            {
                Console.WriteLine("not found");
                return false;
            }
            Console.WriteLine(username + "  " + _manage.GetAdminByUsername(username).Username);
            if (_manage.CheckPassword(username, password))
            {
                isLogged = true;
                log_username = username;
                log_id = _manage.GetAdminByUsername(username).Id;
                log_roles = _manage.GetRolesById(log_id);
                return true;
            }
            Console.WriteLine("password error");
            return false;
        }
        public static void LogOut()
        {
            isLogged = false;
        }
        public static void ChangeOwnPassword(string password)
        {
            if (isLogged == false)
                return;
            _manage.GetAdminById(log_id).Password = password;
            _context.SaveChanges();
        }
        public static void AddAdmin(string username, string pwd, int roles)
        {
            if (log_roles < 3)
                return;
            _manage.AddAdmin(username, pwd, roles);
        }
        public static void ChangeRolesById(int id, int new_roles)
        {
            if (log_roles < 3 || new_roles < 1 || new_roles > 3)
                return;
            _manage.GetAdminById(id).Roles = new_roles;
        }
    }
}
