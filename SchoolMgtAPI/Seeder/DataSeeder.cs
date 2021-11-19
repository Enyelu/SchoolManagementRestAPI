using Data;
using Microsoft.AspNetCore.Identity;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Seeder
{
    public class DataSeeder
    {
        public static async Task SeedData(SchoolDbContext dbContext, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await dbContext.Database.EnsureCreatedAsync();

            if (!dbContext.Users.Any())
            {
                List<string> roles = new List<string> { "Admin", "Student", "Lecturer","NonAcademicStaff" };

                foreach (var role in roles)
                {
                    await roleManager.CreateAsync(new IdentityRole { Name = role });
                }

                var address = new Address()
                {
                    Id = Guid.NewGuid().ToString(),
                    StreetNumber = "205",
                    City = "Owerri",
                    State = "Imo",
                    Country = "Nigeria"
                };
                //await dbContext.Addresses.AddAsync(address);

                var user = new AppUser
                {
                    Id = Guid.NewGuid().ToString(),
                    FirstName = "Asiabak",
                    MiddleName = "Josiah",
                    LastName = "Cyril",
                    UserName = "hbapp",
                    BirthDate = DateTime.UtcNow.ToShortDateString(),
                    Email = "info@hba.com",
                    PhoneNumber = "09043546576",
                    Gender = "Male",
                    Age = 54,
                    IsActive = true,
                    Avatar = "http://placehold.it/32x32",
                    DateCreated = DateTime.UtcNow.ToString(),
                    DateModified = DateTime.UtcNow.ToString(),
                };
                user.EmailConfirmed = true;
                user.PhoneNumberConfirmed = true;
                user.Address = address;
                await userManager.CreateAsync(user, "Password@123");
                await userManager.AddToRoleAsync(user, "Admin");
                
            }
            await dbContext.SaveChangesAsync();
        }
    }
}
