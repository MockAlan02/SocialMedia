﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.QueryFilters
{
    public class PostQueryFilter
    {
        public int? IdUser { get; set; }
        public DateTime? Date { get; set; }
        public string? Description { get; set; }
    }
}
