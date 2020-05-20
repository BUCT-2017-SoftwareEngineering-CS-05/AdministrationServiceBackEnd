using AdministrationServiceBackEnd.Models;
using System.Collections.Generic;

namespace AdministrationServiceBackEnd.Services
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
