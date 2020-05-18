using System;
using System.Collections.Generic;

namespace AdministrationServiceBackEnd.Models
{
    public partial class Comment
    {
        public int Midex { get; set; }
        public string Userid { get; set; }
        public int? Exhscore { get; set; }
        public int? Serscore { get; set; }
        public int? Envscore { get; set; }
        public string Msg { get; set; }
    }
}
