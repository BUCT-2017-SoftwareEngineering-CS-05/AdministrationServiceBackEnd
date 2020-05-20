﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdministrationServiceBackEnd.DtoParameters
{
    public class UserDtoParameters
    {
        public int PageNumber { get; set; } = 1;
        private const int MaxPageSize = 50;
        private int _pageSize = 5;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
        //public string OrderBy { get; set; } = "CompanyName";
        //public string Fields { get; set; }
    }
}
