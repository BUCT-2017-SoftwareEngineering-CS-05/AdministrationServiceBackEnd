using AdministrationServiceBackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdministrationServiceBackEnd.Services
{
    public class ManageAdmin : IManageAdmin
    {
        private MuseumContext _context;
        public ManageAdmin(MuseumContext museumContext)
        {
            _context = museumContext;
        }
        public void AddAdmin(string username, string password, int roles)
        {
            Admin admin = new Admin();
            admin.Username = username;
            admin.Password = password;
            admin.Roles = roles;
            _context.Add(admin);
            _context.SaveChanges();
        }

        public void ChangePassword(int id, string password)
        {
            GetAdminById(id).Password = password;
            _context.SaveChanges();
        }

        public IEnumerable<Admin> GetAllAdmins()
        {
            return _context.Admin;
        }

        public Admin GetAdminById(int id)
        {
            return _context.Admin.FirstOrDefault(n => n.Id == id);
        }
        public Admin GetAdminByUsername(string username)
        {
            Admin admin = _context.Admin.FirstOrDefault(n => n.Username == username);
            if (admin.Username != username)
                return null;
            return admin;
        }

        public void ModifyUsername(int id, string new_name)
        {
            GetAdminById(id).Username = new_name;
            _context.SaveChanges();
        }

        public void RemoveAdmin(int id)
        {
            _context.Remove(GetAdminById(id));
            _context.SaveChanges();
        }
        public bool CheckPassword(string username, string password)
        {
            Console.WriteLine("input:" + password + " correct:" + GetAdminByUsername(username).Password);
            return GetAdminByUsername(username).Password == password;
        }
        public int GetIdByUsername(string username)
        {
            return GetAdminByUsername(username).Id;
        }
        public int GetRolesById(int id)
        {
            return (int)GetAdminById(id).Roles;
        }
    }
}
