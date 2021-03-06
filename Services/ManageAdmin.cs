﻿using AdministrationServiceBackEnd.DtoParameters;
using AdministrationServiceBackEnd.Helpers;
using AdministrationServiceBackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            if (admin == null || admin.Username != username  )
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
            return GetAdminByUsername(username).Password == password;
        }
        public int GetIdByUsername(string username)
        {
            return GetAdminByUsername(username).Id;
        }
        public int GetRolesById(int id)
        {
            return GetAdminById(id).Roles;
        }
        public async Task<PagedList<Admin>> GetAdminsAsync(AdminDtoParameters parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }
            var queryExpression = _context.Admin as IQueryable<Admin>;
            return await PagedList<Admin>.CreateAsync(queryExpression, parameters.PageNumber, parameters.PageSize);
        }
    }
}
