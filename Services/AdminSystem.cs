using AdministrationServiceBackEnd.DtoParameters;
using AdministrationServiceBackEnd.Helpers;
using AdministrationServiceBackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdministrationServiceBackEnd.Services
{
    public static class AdminSystem
    {
        private static MuseumContext _context = new MuseumContext();
        private static IManageAdmin _manage = new ManageAdmin(_context);
        public static async Task<PagedList<Admin>> GetAdminsAsync(AdminDtoParameters parameters)
        {
            return await _manage.GetAdminsAsync(parameters);
        }
        public static Admin GetAdminById(Admin admin)
        {
            return _manage.GetAdminById(admin.Id);
        }
        public static Admin GetAdminByUsername(Admin admin)
        {
            return _manage.GetAdminByUsername(admin.Username);
        }
        public static bool LogIn(Admin admin)
        {
            if (_manage.GetAdminByUsername(admin.Username) == null)
                return false;
            if (_manage.CheckPassword(admin.Username, admin.Password))
                return true;
            return false;
        }
        public static bool ChangePassword(Admin admin)
        {
            Admin to_change = GetAdminById(admin);
            if (to_change == null)
                return false;
            _context.Remove(to_change);
            _context.SaveChanges();
            to_change.Password = admin.Password;
            _context.Add(to_change);
            _context.SaveChanges();
            return true;
        }
        public static bool ChangeRoles(Admin admin)
        {
            if (_manage.GetAdminById(admin.Id) == null)
                return false;
            Admin to_change = _manage.GetAdminById(admin.Id);
            _context.Remove(to_change);
            _context.SaveChanges();
            to_change.Roles = admin.Roles;
            _context.Add(to_change);
            _context.SaveChanges();
            return true;
        }
        public static bool ChangeName(Admin admin)
        {
            Admin to_change = _manage.GetAdminById(admin.Id);
            if (to_change==null)
                return false;
            _context.Remove(to_change);
            _context.SaveChanges();
            to_change.Username = admin.Username;
            _context.Add(to_change);
            _context.SaveChanges();
            return true;
        }
        public static bool AddAdmin(Admin admin)
        {
            if (_manage.GetAdminByUsername(admin.Username) != null)
                return false;
            _context.Add(admin);
            _context.SaveChanges();
            return true;
        }
        public static bool DeleteAdmin(Admin admin)
        {
            admin = _manage.GetAdminById(admin.Id);
            if (admin == null)
                return false;
            _context.Remove(admin);
            _context.SaveChanges();
            return true;
        }
    }
}
