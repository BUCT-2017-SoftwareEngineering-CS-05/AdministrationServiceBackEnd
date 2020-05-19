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
        
        public static bool Mute(User user)
        {
            if (_user.GetUserById(user.Userid) == null)
                return false;
            _user.MuteUser(user.Userid, 0);
            return true;
        }
        public static bool CancelMute(User user)
        {
            if (_user.GetUserById(user.Userid) == null)
                return false;
            _user.MuteUser(user.Userid, 0);
            return true;
        }
        public static bool DeleteOneComment(User user, int midex)
        {
            if (_user.GetUserById(user.Userid) == null)
                return false;
            _comment.DeleteComment(user.Userid, midex);
            return true;
        }
        public static IEnumerable<Comment> GetCommentsByUser(User user)
        {
            return _comment.GetCommentsByUser(user.Userid);
        }
        public static bool DeleteAllCommentByUser(User user)
        {
            if (_user.GetUserById(user.Userid) == null)
                return false;
            IEnumerable<Comment> comments = GetCommentsByUser(user);
            foreach(var comment in comments)
                _context.Remove(comment);
            _context.SaveChanges();
            return true;
        }
        public static bool ChangePassword(User user)
        {
            if (_user.GetUserById(user.Userid) == null)
                return false;
            User to_change = _user.GetUserById(user.Userid);
            _context.Remove(to_change);
            to_change.Userpwd = user.Userpwd;
            _context.Add(to_change);
            _context.SaveChanges();
            return true;
        }
    }
}
