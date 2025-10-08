using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM_DAL.Models.Ticket
{
    public class TicketModel
    {
        public int id { get; set; }
        public string code { get; set; }
        public string? description { get; set; }
        public decimal? price { get; set; }
    }
}
