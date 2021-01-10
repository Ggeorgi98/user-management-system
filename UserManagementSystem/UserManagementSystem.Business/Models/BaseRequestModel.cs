using System;
using System.Collections.Generic;
using System.Text;

namespace UserManagementSystem.Business.Models
{
    public class BaseRequestModel
    {
        public int? PageNumber { get; set; } = 1;

        public int? ItemsPerPage { get; set; } = 10;

        public string OrderBy { get; set; }
    }
}
