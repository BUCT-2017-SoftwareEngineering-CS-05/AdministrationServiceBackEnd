using System;
using System.Collections.Generic;

namespace AdministrationServiceBackEnd.Models
{
    public partial class Collection
    {
        public uint Oid { get; set; }
        public int Midex { get; set; }
        public string Oname { get; set; }
        public string Ointro { get; set; }
        public string Ophoto { get; set; }
    }
}
