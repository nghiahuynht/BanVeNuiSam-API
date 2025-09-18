using GM_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM_DAL.APIModels
{
    public class PageSizeParamModel<T>
    {
        public long totalRow { get; set; }
        public int page { get; set; }
        public int pageSize { get; set; }
        public List<T> results { get; set; }
        public APIMessageResponse message { get; set; }
    }
}
