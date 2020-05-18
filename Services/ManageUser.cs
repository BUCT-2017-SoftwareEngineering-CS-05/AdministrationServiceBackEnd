using AdministrationServiceBackEnd.Models;
using AdministrationServiceBackEnd.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MuseumBackend.Models
{
    public class ManageUser : IManageUser
    {
        private MuseumContext _context;
        public ManageUser(MuseumContext appDbContext)
        {
            _context = appDbContext;
        }
        public IEnumerable<User> GetAllUsers()
        {
            return _context.User;
        }

        public User GetUserById(string id)
        {
            return _context.User.FirstOrDefault(n => n.Userid == id);
        }

        public void MuteUser(string id, int mute)
        {
            GetUserById(id).Coright = mute;
            _context.SaveChanges();
        }
    }
}
