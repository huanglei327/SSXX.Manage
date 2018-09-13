using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSXX.Manage.Models
{
    public class PageList
    {
        public string table { get; set; }
        public string tableField { get; set; }
        public string strWhere { get; set; }
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
        public string sort { get; set; }
        public int isGetCount { get; set; }
    }
}
