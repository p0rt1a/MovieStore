using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.TokenOperations.Models
{
    public class Token
    {
        public string AccessToken { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string RefreshToken { get; set; }
    }
}
