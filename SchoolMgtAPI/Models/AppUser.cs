﻿using Microsoft.AspNetCore.Identity;
using System;

namespace Models
{
    public class AppUser : IdentityUser
    { 
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Avatar { get; set; }  
        public bool IsActive { get; set; }
        public string BirthDate { get; set; }
        public string DateCreated { get; set; }
        public string DateModified { get; set; }
        public string RefereshToken { get; set; } 
        public DateTime RefereshTokenExpiry { get; set; }
        public Address Address { get; set; }
        public Lecturer Lecturer {  get; set; }
        public Student Student { get; set; }
        public NonAcademicStaff NonAcademicStaff {  get; set;}
    }
}