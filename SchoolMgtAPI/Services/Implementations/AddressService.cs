using Data;
using Microsoft.AspNetCore.Identity;
using Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.AppUnitOfWork;
using Utilities.Dtos;

namespace Services.Implementations
{
    public class AddressService : IAddressService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public AddressService(SchoolDbContext context, UserManager<AppUser> userManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public async Task UpdateAddressAsync(UpdateAddressDto addressDto, string userId)
        {
          var user =  await _userManager.FindByIdAsync(userId);
          var addressId = user.Address.Id;
          var address = await _unitOfWork.Address.GetAddressAsync(addressId);
          address.StreetNumber = addressDto.StreetNumber;
          address.City = addressDto.City;
          address.State = addressDto.State;
          address.Country = addressDto.Country;
          _unitOfWork.Address.Update(address);
          await _unitOfWork.SaveChangesAsync();

          user.Address = address; 
          await _userManager.UpdateAsync(user);
        }
    }
}
