using AdministrationServiceBackEnd.Models;
using System.Collections.Generic;

namespace MuseumBackend.Services
{
    interface IManageComment
    {
        IEnumerable<Comment> GetAllComments();
        void DeleteComment(string id, int midex);
    }
}
