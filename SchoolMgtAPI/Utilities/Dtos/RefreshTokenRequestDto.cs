using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Dtos
{
    public class RefreshTokenRequestDto
    {
        public string RefreshToken { get; set; }
        public string UserId { get; set; }
    }
}
