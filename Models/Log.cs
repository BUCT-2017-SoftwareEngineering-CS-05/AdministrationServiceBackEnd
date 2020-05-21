using System;
using System.Collections.Generic;

namespace AdministrationServiceBackEnd.Models
{
    public partial class Log
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Operation { get; set; }
        public string Time { get; set; }
    }
}
