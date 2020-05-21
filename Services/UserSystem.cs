using AdministrationServiceBackEnd.Models;
using System;
using System.Collections.Generic;

namespace AdministrationServiceBackEnd.Services
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
        public static bool DeleteOneComment(Comment comment)
        {
            if (_user.GetUserById(comment.Userid) == null)
                return false;
            _comment.DeleteComment(comment.Userid, comment.Midex);
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
            User to_change = _user.GetUserById(user.Userid);
            if (to_change == null)
                return false;
            _context.Remove(to_change);
            _context.SaveChanges();
            to_change.Userpwd = user.Userpwd;
            _context.Add(to_change);
            _context.SaveChanges();
            return true;
        }
        public static bool DeleteUser(User user)
        {
            if (_user.GetUserById(user.Userid) == null)
                return false;
            _context.Remove(_user.GetUserById(user.Userid));
            _context.SaveChanges();
            return true;
        }

        internal static bool ChangeMute(User user)
        {
            user = _user.GetUserById(user.Userid);
            if (user == null)
                return false;
            _context.Remove(user);
            _context.SaveChanges();
            if (user.Coright == 1) user.Coright = 0;
            else user.Coright = 1;
            _context.Add(user);
            _context.SaveChanges();
            return true;
        }
    }
}
