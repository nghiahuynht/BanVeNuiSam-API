﻿using GM_DAL.Models;
using GM_DAL.Models.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM_DAL.IServices
{
    public interface ITicketService
    {
        Task <APIResultObject<List<TicketUserModel>>> GetTicketByUser(int UserId);
    }
}
