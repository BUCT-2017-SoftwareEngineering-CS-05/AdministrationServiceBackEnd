using AdministrationServiceBackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MuseumBackend.Models
{
    interface IManageComment
    {
        IEnumerable<Comment> GetAllComments();
        Comment GetComment(string user, int midex);
        void DeleteComment(string user, int midex);
        IEnumerable<Comment> GetCommentsByMidex(int midex);
        IEnumerable<Comment> GetCommentsByUser(string user);
    }
}
