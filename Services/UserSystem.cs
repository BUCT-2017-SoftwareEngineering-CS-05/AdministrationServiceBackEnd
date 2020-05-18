using AdministrationServiceBackEnd.Models;
using AdministrationServiceBackEnd.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MuseumBackend.Models
{
    public static class UserSystem
    {
        private static MuseumContext _context = new MuseumContext();
        private static IManageUser _user = new ManageUser(_context);
        private static IManageComment _comment = new ManageComment(_context);
        public static void Mute(string user)
        {
            _user.MuteUser(user, 0);
        }
        public static void CancelMute(string user)
        {
            _user.MuteUser(user, 1);
        }
        public static void DeleteOneComment(string user, int midex)
        {
            _comment.DeleteComment(user, midex);
        }
        public static IEnumerable<Comment> GetCommentsByUser(string user)
        {
            return _comment.GetCommentsByUser(user);
        }
        public static void DeleteAllCommentByUser(string user)
        {
            IEnumerable<Comment> comments = GetCommentsByUser(user);
            foreach(var comment in comments)
                _context.Remove(comment);
            _context.SaveChanges();
        }
        
    }
}
