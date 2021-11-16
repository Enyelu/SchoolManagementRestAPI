using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Dtos;

namespace Services.Interfaces
{
    public interface IAddressService 
    {
        Task UpdateAddressAsync(UpdateAddressDto addressDto, string userId);
    }
}
