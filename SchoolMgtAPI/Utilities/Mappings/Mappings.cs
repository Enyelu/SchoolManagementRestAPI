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

            //mapping for course registration
            CreateMap<CourseDto, Course>()
                .ForMember(x => x.Id, y => y.MapFrom(z => z.Id))
                .ForMember(x => x.Name, y => y.MapFrom(z => z.Name))
                .ForMember(x => x.CourseCode, y => y.MapFrom(z => z.CourseCode))
                .ForMember(x => x.DateCreated, y => y.MapFrom(z => z.DateCreated))
                .ForMember(x => x.IsActive, y => y.MapFrom(z => z.IsActive))
                .ForMember(x => x.Department, y => y.MapFrom(z => z.Department));

            //mapping  for course lecturers details
            CreateMap<Course, LecturerModel>()
                .ForMember(x => x.Lecturer, y => y.MapFrom(z => z.Lecturers));

            //mapping for course students details
            CreateMap<Course, StudentModel>()
                .ForMember(x => x.Students, y => y.MapFrom(z => z.Students));
            
            CreateMap<CourseUpdateDto, Course>()
                .ForMember(x => x.Name, y => y.MapFrom(z => z.Name))
                .ForMember(x => x.CourseCode, y => y.MapFrom(z => z.NewCourseCode));

            //mapping to read non-academic staff response
            CreateMap<NonAcademicStaff, NonAcademicStaffResponseDto>()
                .ForMember(x => x.FullName, y => y.MapFrom(z => z.AppUser.FirstName + " " + z.AppUser.MiddleName + " " + z.AppUser.LastName))
                .ForMember(x => x.Position, y => y.MapFrom(z => z.DutyPost.Name))
                .ForMember(x => x.Avatar, y => y.MapFrom(z => z.AppUser.Avatar))
                .ForMember(x => x.IsActive, y => y.MapFrom(z => z.AppUser.IsActive))
                .ForMember(x => x.BirthDate, y => y.MapFrom(z => z.AppUser.BirthDate))
                .ForMember(x => x.DateCreated, y => y.MapFrom(z => z.AppUser.DateCreated))
                .ForMember(x => x.StreetNumber, y => y.MapFrom(z => z.AppUser.Addresses.StreetNumber))
                .ForMember(x => x.City, y => y.MapFrom(z => z.AppUser.Addresses.City))
                .ForMember(x => x.State, y => y.MapFrom(z => z.AppUser.Addresses.State))
                .ForMember(x => x.Country, y => y.MapFrom(z => z.AppUser.Addresses.Country))
                .ForMember(x => x.Email, y => y.MapFrom(z => z.AppUser.Email))
                .ForMember(x => x.Department, y => y.MapFrom(z => z.Department.Name));

            //mapping for lecturer response
            CreateMap<AppUser, LecturerResponseDto>()
                .ForMember(x => x.FirstName, y => y.MapFrom(z => z.FirstName))
                .ForMember(x => x.MiddleName, y => y.MapFrom(z => z.MiddleName))
                .ForMember(x => x.LastName, y => y.MapFrom(z => z.LastName))
                .ForMember(x => x.Email, y => y.MapFrom(z => z.Email))
                .ForMember(x => x.Avatar, y => y.MapFrom(z => z.Avatar))
                .ForMember(x => x.IsActive, y => y.MapFrom(z => z.IsActive))
                .ForMember(x => x.BirthDate, y => y.MapFrom(z => z.BirthDate))
                .ForMember(x => x.DateCreated, y => y.MapFrom(z => z.DateCreated))
                .ForMember(x => x.DateModified, y => y.MapFrom(z => z.DateModified))
                .ForMember(x => x.StreetNumber, y => y.MapFrom(z => z.Addresses.StreetNumber))
                .ForMember(x => x.City, y => y.MapFrom(z => z.Addresses.City))
                .ForMember(x => x.State, y => y.MapFrom(z => z.Addresses.State))
                .ForMember(x => x.Country, y => y.MapFrom(z => z.Addresses.Country))
                .ForMember(x => x.Department, y => y.MapFrom(z => z.Lecturer.Department.Name))
                .ForMember(x => x.Courses, y => y.MapFrom(z => z.Lecturer.Courses));

            //mapping for getting details of departments in a faculty
            CreateMap<Faculty, FacultyDepartmentsResponseDto>()
                .ForMember(x => x.Name, y => y.MapFrom(z => z.Name))
                .ForMember(x => x.Date, y => y.MapFrom(z => z.Date))
                .ForMember(x => x.IsActive, y => y.MapFrom(z => z.IsActive))
                .ForMember(x => x.NoOfLecturer, y => y.MapFrom(z => z.Lecturer.Count()))
                .ForMember(x => x.NoOfNonAcademicStaff, y => y.MapFrom(z => z.NonAcademicStaff.Count()))
                .ForMember(x => x.NoOfCourses, y => y.MapFrom(z => z.Courses.Count()));

            CreateMap<Lecturer, FacultyLecturerResponseDto>()
                .ForMember(x => x.FirstName, y => y.MapFrom(z => z.AppUser.FirstName))
                .ForMember(x => x.MiddleName, y => y.MapFrom(z => z.AppUser.MiddleName))
                .ForMember(x => x.LastName, y => y.MapFrom(z => z.AppUser.LastName))
                .ForMember(x => x.Avatar, y => y.MapFrom(z => z.AppUser.Avatar))
                .ForMember(x => x.IsActive, y => y.MapFrom(z => z.AppUser.IsActive))
                .ForMember(x => x.Department, y => y.MapFrom(z => z.Department.Name))
                .ForMember(x => x.BirthDate, y => y.MapFrom(z => z.AppUser.BirthDate))
                .ForMember(x => x.DateCreated, y => y.MapFrom(z => z.AppUser.DateCreated))
                .ForMember(x => x.DateModified, y => y.MapFrom(z => z.AppUser.DateModified));

            CreateMap<NonAcademicStaff, FacultyLecturerResponseDto>()
               .ForMember(x => x.FirstName, y => y.MapFrom(z => z.AppUser.FirstName))
               .ForMember(x => x.MiddleName, y => y.MapFrom(z => z.AppUser.MiddleName))
               .ForMember(x => x.LastName, y => y.MapFrom(z => z.AppUser.LastName))
               .ForMember(x => x.Avatar, y => y.MapFrom(z => z.AppUser.Avatar))
               .ForMember(x => x.IsActive, y => y.MapFrom(z => z.AppUser.IsActive))
               .ForMember(x => x.Department, y => y.MapFrom(z => z.Department.Name))
               .ForMember(x => x.BirthDate, y => y.MapFrom(z => z.AppUser.BirthDate))
               .ForMember(x => x.DateCreated, y => y.MapFrom(z => z.AppUser.DateCreated))
               .ForMember(x => x.DateModified, y => y.MapFrom(z => z.AppUser.DateModified));
        }
    }
}