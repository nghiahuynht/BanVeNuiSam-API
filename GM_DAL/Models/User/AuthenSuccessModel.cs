using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM_DAL.Models.User
{
    public class AuthenSuccessModel
    {
        public string userName { get; set; }
        public string fullName {  get; set; }
        public string roleCode { get; set; }
        public string token { get; set; }
        public DateTime? expires { get; set; }
    }
}
