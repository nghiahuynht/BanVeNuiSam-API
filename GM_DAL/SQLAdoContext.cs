using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace GM_DAL
{
    public class SQLAdoContext
    {
        private IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        private string _connectionString;
        public SQLAdoContext(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _connectionString = "";


        }


        public IDbConnection CreateConnection()
        {
            _connectionString = _configuration["ConnectionStrings:DefaultConnection"];
            return new SqlConnection(_connectionString);
        }
    }
}
