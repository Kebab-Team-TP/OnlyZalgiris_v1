using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zalgiris.ArticleScraper
{
    public class PaginationItem
    {
        public int? PageNumber { get; set; }
        public bool IsPageNumber {  get; set; }
    }
}