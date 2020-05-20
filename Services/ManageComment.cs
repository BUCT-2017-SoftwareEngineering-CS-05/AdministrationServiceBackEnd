﻿using AdministrationServiceBackEnd.Models;
using System.Collections.Generic;
using System.Linq;

namespace AdministrationServiceBackEnd.Services
{
    public class ManageComment : IManageComment
    {
        private MuseumContext _context;
        public ManageComment(MuseumContext appDbContext)
        {
            _context = appDbContext;
        }
        public void DeleteComment(string user, int midex)
        {
            _context.Remove(GetComment(user, midex));
            _context.SaveChanges();
        }

        public IEnumerable<Comment> GetAllComments()
        {
            return _context.Comment;
        }

        public Comment GetComment(string user, int midex)
        {
            foreach (var comment in _context.Comment)
                if (comment.Midex == midex && comment.Userid == user)
                    return comment;
            return null;
        }
        public IEnumerable<Comment> GetCommentsByMidex(int midex)
        {
            var comments = new List<Comment>();
            foreach (var comment in _context.Comment)
                if (comment.Midex == midex)
                    comments.Add(comment);
            IEnumerable<Comment> EComment = comments.AsEnumerable<Comment>();
            return EComment;
        }
        public IEnumerable<Comment> GetCommentsByUser(string user)
        {
            var comments = new List<Comment>();
            foreach (var comment in _context.Comment)
                if (comment.Userid == user)
                    comments.Add(comment);
            IEnumerable<Comment> EComment = comments.AsEnumerable<Comment>();
            return EComment;
        }
    }
}
