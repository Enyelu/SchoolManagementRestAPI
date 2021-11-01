using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;
using System;

namespace Data
{
    public class SchoolDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<ClassAdviser> ClassAdvisersers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<NonAcademicStaffPosition> NonAcademicStaffDutyPost { get; set; }
        public DbSet<Lecturer> Lecturers { get; set; }
        public DbSet<NonAcademicStaff> NonAcademicStaff { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options){}
    }
}
