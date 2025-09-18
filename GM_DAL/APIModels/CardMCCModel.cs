using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM_DAL.APIModels
{
    public class CardMCCModel
    {
        public long id { get; set; }
        public string cardCode { get; set; }
        public string mccCode { get; set; }
        public string mccName { get; set;}
        public decimal? percentReturn { get; set; }
        public string? returmTerm { get; set; }

    }
}
