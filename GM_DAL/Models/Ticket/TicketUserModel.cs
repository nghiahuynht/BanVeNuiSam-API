using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM_DAL.Models.Ticket
{
    public class TicketUserModel
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public int UserId { get; set; }
    }
}
