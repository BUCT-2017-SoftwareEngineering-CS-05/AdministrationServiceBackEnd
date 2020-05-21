using System;
using System.Collections.Generic;

namespace AdministrationServiceBackEnd.Models
{
    public partial class Academic
    {
        public uint AdId { get; set; }
        public int Mid { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
