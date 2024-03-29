﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.CustomEntities
{
    public class Metadata
    {

        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int TotalPage {get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get;set;}
        public string? NextPageUrl { get; set; }
        public string? PreviousPageUrl { get;set; }
    }
}
