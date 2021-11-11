using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.PaymentTokenGenerator
{
    public static class PaymentToken
    {
        public static string GenerateToken()
        {
            Random random = new Random((int)DateTime.UtcNow.Ticks);
            return random.Next(111111, 999999999).ToString();
        }
    }
}
