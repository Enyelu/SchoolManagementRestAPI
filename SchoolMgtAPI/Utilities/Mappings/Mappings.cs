﻿using AutoMapper;
using Models;
using System.Linq;
using Utilities.Dtos;

namespace Utilities.Mappings
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            //mapping for returning user details to view.
            CreateMap<Student, ReadStudentResponseDto>()
                .ForMember(x => x.RegistrationNumber, y => y.MapFrom(z => z.RegistrationNumber))
                .ForMember(x => x.FullName, y => y.MapFrom(z => z.AppUser.FirstName + " " + z.AppUser.MiddleName + " " + z.AppUser.LastName))
                .ForMember(x => x.Avatar, y => y.MapFrom(z => z.AppUser.Avatar))
                .ForMember(x => x.IsActive, y => y.MapFrom(z => z.AppUser.IsActive))
                .ForMember(x => x.BirthDate, y => y.MapFrom(z => z.AppUser.BirthDate))
                .ForMember(x => x.Email, y => y.MapFrom(z => z.AppUser.Email))
                .ForMember(x => x.DateCreated, y => y.MapFrom(z => z.AppUser.DateCreated))
                .ForMember(x => x.Class, y => y.MapFrom(z => z.Class))
                .ForMember(x => x.Level, y => y.MapFrom(z => z.Level))
                .ForMember(x => x.Department, y => y.MapFrom(z => z.Department.Name))
                .ForMember(x => x.Faculty, y => y.MapFrom(z => z.Faculty.Name))
                .ForMember(x => x.StreetNumber, y => y.MapFrom(z => z.AppUser.Address.StreetNumber))
                .ForMember(x => x.City, y => y.MapFrom(z => z.AppUser.Address.City))
                .ForMember(x => x.State, y => y.MapFrom(z => z.AppUser.Address.State))
                .ForMember(x => x.Country, y => y.MapFrom(z => z.AppUser.Address.Country));

            //mapping for course registration
            CreateMap<CourseDto, Course>();

            //mapping for course registration
            CreateMap<Course, CourseDto>();
               
            //mapping  for course lecturers details
            CreateMap<Lecturer, LecturerResponseModel>()
                    .ForMember(x => x.Avatar, y => y.MapFrom(z => z.AppUser.Avatar))
                    .ForMember(x => x.IsActive, y => y.MapFrom(z => z.AppUser.IsActive))
                    .ForMember(x => x.BirthDate, y => y.MapFrom(z => z.AppUser.BirthDate))
                    .ForMember(x => x.FullName, y => y.MapFrom(z => z.AppUser.FirstName + " " + z.AppUser.MiddleName + " " + z.AppUser.LastName))
                    .ForMember(x => x.DateCreated, y => y.MapFrom(z => z.AppUser.DateCreated))
                    .ForMember(x => x.StreetNumber, y => y.MapFrom(z => z.AppUser.Address.StreetNumber))
                    .ForMember(x => x.City, y => y.MapFrom(z => z.AppUser.Address.City))
                    .ForMember(x => x.State, y => y.MapFrom(z => z.AppUser.Address.State))
                    .ForMember(x => x.Country, y => y.MapFrom(z => z.AppUser.Address.Country)); 

            //mapping for course students details
            CreateMap<Student, StudentResponseDto>()
                    .ForMember(x => x.Class, y => y.MapFrom(z => z.Class))
                    .ForMember(x => x.RegistrationNumber, y => y.MapFrom(z => z.RegistrationNumber))
                    .ForMember(x => x.Level, y => y.MapFrom(z => z.Level))
                    .ForMember(x => x.FullName, y => y.MapFrom(z => z.AppUser.FirstName + " " + z.AppUser.MiddleName + " " + z.AppUser.LastName))
                    .ForMember(x => x.ClassAdviserName, y => y.MapFrom(z => z.ClassAdviser.Lecturer.AppUser.FirstName + " " + z.ClassAdviser.Lecturer.AppUser.LastName))
                    .ForMember(x => x.ClassAdviserEmail, y => y.MapFrom(z => z.ClassAdviser.Lecturer.AppUser.Email))
                    .ForMember(x => x.DepartmentName, y => y.MapFrom(z => z.Department.Name))
                    .ForMember(x => x.FacultyName, y => y.MapFrom(z => z.Faculty.Name));

            CreateMap<CourseUpdateDto, Course>()
                    .ForMember(x => x.Name, y => y.MapFrom(z => z.Name))
                    .ForMember(x => x.CourseCode, y => y.MapFrom(z => z.NewCourseCode));

            //mapping to read non-academic staff response
            CreateMap<NonAcademicStaff, NonAcademicStaffResponseDto>()
                .ForMember(x => x.FullName, y => y.MapFrom(z => z.AppUser.FirstName + " " + z.AppUser.MiddleName + " " + z.AppUser.LastName))
                .ForMember(x => x.Position, y => y.MapFrom(z => z.Position.Name))
                .ForMember(x => x.Avatar, y => y.MapFrom(z => z.AppUser.Avatar))
                .ForMember(x => x.IsActive, y => y.MapFrom(z => z.AppUser.IsActive))
                .ForMember(x => x.BirthDate, y => y.MapFrom(z => z.AppUser.BirthDate))
                .ForMember(x => x.DateCreated, y => y.MapFrom(z => z.AppUser.DateCreated))
                .ForMember(x => x.StreetNumber, y => y.MapFrom(z => z.AppUser.Address.StreetNumber))
                .ForMember(x => x.City, y => y.MapFrom(z => z.AppUser.Address.City))
                .ForMember(x => x.State, y => y.MapFrom(z => z.AppUser.Address.State))
                .ForMember(x => x.Country, y => y.MapFrom(z => z.AppUser.Address.Country))
                .ForMember(x => x.Email, y => y.MapFrom(z => z.AppUser.Email))
                .ForMember(x => x.Department, y => y.MapFrom(z => z.Department.Name));

            //mapping for lecturer response
            CreateMap<Lecturer, LecturerResponseDto>()
                .ForMember(x => x.FirstName, y => y.MapFrom(z => z.AppUser.FirstName))
                .ForMember(x => x.MiddleName, y => y.MapFrom(z => z.AppUser.MiddleName))
                .ForMember(x => x.LastName, y => y.MapFrom(z => z.AppUser.LastName))
                .ForMember(x => x.Email, y => y.MapFrom(z => z.AppUser.Email))
                .ForMember(x => x.Avatar, y => y.MapFrom(z => z.AppUser.Avatar))
                .ForMember(x => x.IsActive, y => y.MapFrom(z => z.AppUser.IsActive))
                .ForMember(x => x.BirthDate, y => y.MapFrom(z => z.AppUser.BirthDate))
                .ForMember(x => x.DateCreated, y => y.MapFrom(z => z.AppUser.DateCreated))
                .ForMember(x => x.DateModified, y => y.MapFrom(z => z.AppUser.DateModified))
                .ForMember(x => x.StreetNumber, y => y.MapFrom(z => z.AppUser.Address.StreetNumber))
                .ForMember(x => x.City, y => y.MapFrom(z => z.AppUser.Address.City))
                .ForMember(x => x.State, y => y.MapFrom(z => z.AppUser.Address.State))
                .ForMember(x => x.Country, y => y.MapFrom(z => z.AppUser.Address.Country))
                .ForMember(x => x.Department, y => y.MapFrom(z => z.AppUser.Lecturer.Department.Name))
                .ForMember(x => x.Faculty, y => y.MapFrom(z => z.AppUser.Lecturer.Faculty.Name))
                .ForMember(x => x.Courses, y => y.MapFrom(z => z.AppUser.Lecturer.Courses));

            //mapping for getting details of departments in a faculty
            CreateMap<Department, FacultyDepartmentsResponseDto>()
                .ForMember(x => x.Name, y => y.MapFrom(z => z.Name))
                .ForMember(x => x.Date, y => y.MapFrom(z => z.DateCreated))
                .ForMember(x => x.IsActive, y => y.MapFrom(z => z.IsActive))
                .ForMember(x => x.NoOfLecturer, y => y.MapFrom(z => z.Lecturer.Count()))
                .ForMember(x => x.NoOfNonAcademicStaff, y => y.MapFrom(z => z.NonAcademicStaff.Count()))
                .ForMember(x => x.NoOfCourses, y => y.MapFrom(z => z.Courses.Count()));

            CreateMap<Lecturer, FacultyLecturerResponseDto>()
                .ForMember(x => x.FirstName, y => y.MapFrom(z => z.AppUser.FirstName))
                .ForMember(x => x.MiddleName, y => y.MapFrom(z => z.AppUser.MiddleName))
                .ForMember(x => x.LastName, y => y.MapFrom(z => z.AppUser.LastName))
                .ForMember(x => x.Email, y => y.MapFrom(z => z.AppUser.Email))
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

            //Student register mapping
            CreateMap<RegisterStudentDto, AppUser>();

            //Student Address register mapping
            CreateMap<RegisterStudentDto, Address>();

            //Lecturer register mapping
            CreateMap<RegisterLecturerDto, AppUser>();

            //Lecturer address register mapping
            CreateMap<RegisterLecturerDto, Address>();

            //Non-Academic staff register mapping
            CreateMap<RegisterNonAcademicStaffDto, AppUser>();
            //Non-Academic staff address register mapping
            CreateMap<RegisterNonAcademicStaffDto, Address>();

            //Update address mapping
            CreateMap<UpdateAddressDto,Address>();

            //student mapping
            CreateMap<UpdateAddressDto, Student>();

            //Department mapping
            CreateMap<Department, ReadDepartmentDto>()
                .ForMember(x => x.Name, y => y.MapFrom(z => z.Name))
                .ForMember(x => x.DateCreated, y => y.MapFrom(z => z.DateCreated))
                .ForMember(x => x.IsActive, Y => Y.MapFrom(Z => Z.IsActive))
                .ForMember(x => x.Faculty, y => y.MapFrom(z => z.Faculty.Name));



            //mapping for class-adviser response
            CreateMap<ClassAdviser, ClassAdviserResponseDto>()
                .ForMember(x => x.Level, y => y.MapFrom(z => z.Level))
                .ForMember(x => x.IsCourseAdviser, y => y.MapFrom(z => z.IsCourseAdviser))
                .ForMember(x => x.DateTime, y => y.MapFrom(z => z.DateTime))
                .ForMember(x => x.FirstName, y => y.MapFrom(z => z.Lecturer.AppUser.FirstName))
                .ForMember(x => x.MiddleName, y => y.MapFrom(z => z.Lecturer.AppUser.MiddleName))
                .ForMember(x => x.LastName, y => y.MapFrom(z => z.Lecturer.AppUser.LastName))
                .ForMember(x => x.Email, y => y.MapFrom(z => z.Lecturer.AppUser.Email))
                .ForMember(x => x.Avatar, y => y.MapFrom(z => z.Lecturer.AppUser.Avatar))
                .ForMember(x => x.IsActive, y => y.MapFrom(z => z.Lecturer.AppUser.IsActive))
                .ForMember(x => x.BirthDate, y => y.MapFrom(z => z.Lecturer.AppUser.BirthDate))
                .ForMember(x => x.DateModified, y => y.MapFrom(z => z.Lecturer.AppUser.DateModified))
                .ForMember(x => x.StreetNumber, y => y.MapFrom(z => z.Lecturer.AppUser.Address.StreetNumber))
                .ForMember(x => x.City, y => y.MapFrom(z => z.Lecturer.AppUser.Address.City))
                .ForMember(x => x.State, y => y.MapFrom(z => z.Lecturer.AppUser.Address.State))
                .ForMember(x => x.Country, y => y.MapFrom(z => z.Lecturer.AppUser.Address.Country))
                .ForMember(x => x.Department, y => y.MapFrom(z => z.Lecturer.AppUser.Lecturer.Department.Name))
                .ForMember(x => x.Faculty, y => y.MapFrom(z => z.Lecturer.AppUser.Lecturer.Faculty.Name))
                .ForMember(x => x.Courses, y => y.MapFrom(z => z.Lecturer.AppUser.Lecturer.Courses));
        }
    }
}