using AutoMapper;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Dtos;

namespace Utilities.Mappings
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            //mapping for returning user details to view.
            CreateMap<Student, ReadStudentResponseDto>()
                .ForMember(x => x.AppUserId, y => y.MapFrom(z => z.AppUserId))
                .ForMember(x => x.RegistrationNumber, y => y.MapFrom(z => z.RegistrationNumber))
                .ForMember(x => x.FullName, y => y.MapFrom(z => z.AppUser.FirstName + " " + z.AppUser.MiddleName + " " + z.AppUser.LastName))
                .ForMember(x => x.Avatar, y => y.MapFrom(z => z.AppUser.Avatar))
                .ForMember(x => x.IsActive, y => y.MapFrom(z => z.AppUser.IsActive))
                .ForMember(x => x.BirthDate, y => y.MapFrom(z => z.AppUser.BirthDate))
                .ForMember(x => x.DateCreated, y => y.MapFrom(z => z.AppUser.DateCreated))
                .ForMember(x => x.Class, y => y.MapFrom(z => z.Class))
                .ForMember(x => x.Level, y => y.MapFrom(z => z.Level))
                .ForMember(x => x.Id, y => y.MapFrom(z => z.ClassAdviser.Lecturer.AppUserId))
                .ForMember(x => x.Department, y => y.MapFrom(z => z.Department.Name))
                .ForMember(x => x.Faculty, y => y.MapFrom(z => z.Faculty.Name))
                .ForMember(x => x.StreetNumber, y => y.MapFrom(z => z.AppUser.Addresses.StreetNumber))
                .ForMember(x => x.StreetNumber, y => y.MapFrom(z => z.AppUser.Addresses.StreetNumber))
                .ForMember(x => x.StreetNumber, y => y.MapFrom(z => z.AppUser.Addresses.StreetNumber))
                .ForMember(x => x.StreetNumber, y => y.MapFrom(z => z.AppUser.Addresses.StreetNumber))
                .ForMember(x => x.StreetNumber, y => y.MapFrom(z => z.AppUser.Addresses.StreetNumber));

            //mapping  for course lecturers details
            CreateMap<Course, LecturerModel>()
                .ForMember(x => x.Lecturer, y => y.MapFrom(z => z.Lecturers));

            //mapping for course students details
            CreateMap<Course, StudentModel>()
                .ForMember(x => x.Students, y => y.MapFrom(z => z.Students));
            
            CreateMap<CourseUpdateDto, Course>()
                .ForMember(x => x.Name, y => y.MapFrom(z => z.Name))
                .ForMember(x => x.CourseCode, y => y.MapFrom(z => z.CourseCode));

        }
    }
}