﻿// <auto-generated />
using System;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Data.Migrations
{
    [DbContext(typeof(SchoolDbContext))]
    partial class SchoolDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("CourseLecturer", b =>
                {
                    b.Property<string>("CoursesId")
                        .HasColumnType("text");

                    b.Property<string>("LecturersAppUserId")
                        .HasColumnType("text");

                    b.HasKey("CoursesId", "LecturersAppUserId");

                    b.HasIndex("LecturersAppUserId");

                    b.ToTable("CourseLecturer");
                });

            modelBuilder.Entity("CourseStudent", b =>
                {
                    b.Property<string>("CoursesId")
                        .HasColumnType("text");

                    b.Property<string>("StudentsAppUserId")
                        .HasColumnType("text");

                    b.HasKey("CoursesId", "StudentsAppUserId");

                    b.HasIndex("StudentsAppUserId");

                    b.ToTable("CourseStudent");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Models.Address", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("City")
                        .HasColumnType("text");

                    b.Property<string>("Country")
                        .HasColumnType("text");

                    b.Property<string>("State")
                        .HasColumnType("text");

                    b.Property<string>("StreetNumber")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("Models.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("AddressesId")
                        .HasColumnType("text");

                    b.Property<string>("Avatar")
                        .HasColumnType("text");

                    b.Property<string>("BirthDate")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("DateCreated")
                        .HasColumnType("text");

                    b.Property<string>("DateModified")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("MiddleName")
                        .HasColumnType("text");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("AddressesId");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Models.ClassAdviser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Class")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("LecturerId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("LecturerId");

                    b.ToTable("ClassAdvisersers");
                });

            modelBuilder.Entity("Models.Course", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("CourseCode")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("DepartmentId")
                        .HasColumnType("text");

                    b.Property<string>("FacultyId")
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("FacultyId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("Models.Department", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("DateCreated")
                        .HasColumnType("text");

                    b.Property<string>("FacultyId")
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("FacultyId");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("Models.Faculty", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("Date")
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Faculties");
                });

            modelBuilder.Entity("Models.Lecturer", b =>
                {
                    b.Property<string>("AppUserId")
                        .HasColumnType("text");

                    b.Property<string>("DepartmentId")
                        .HasColumnType("text");

                    b.Property<string>("FacultyId")
                        .HasColumnType("text");

                    b.HasKey("AppUserId");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("FacultyId");

                    b.ToTable("Lecturers");
                });

            modelBuilder.Entity("Models.NonAcademicStaff", b =>
                {
                    b.Property<string>("AppUserId")
                        .HasColumnType("text");

                    b.Property<string>("DepartmentId")
                        .HasColumnType("text");

                    b.Property<string>("DutyPostId")
                        .HasColumnType("text");

                    b.Property<string>("FacultyId")
                        .HasColumnType("text");

                    b.HasKey("AppUserId");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("DutyPostId");

                    b.HasIndex("FacultyId");

                    b.ToTable("NonAcademicStaff");
                });

            modelBuilder.Entity("Models.NonAcademicStaffPosition", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("NonAcademicStaffDutyPost");
                });

            modelBuilder.Entity("Models.PaymentRecord", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("Amount")
                        .HasColumnType("integer");

                    b.Property<string>("Avatar")
                        .HasColumnType("text");

                    b.Property<string>("DateCreated")
                        .HasColumnType("text");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("boolean");

                    b.Property<string>("PaymentType")
                        .HasColumnType("text");

                    b.Property<string>("StudentDepartment")
                        .HasColumnType("text");

                    b.Property<string>("StudentEmail")
                        .HasColumnType("text");

                    b.Property<string>("StudentFirstName")
                        .HasColumnType("text");

                    b.Property<string>("StudentId")
                        .HasColumnType("text");

                    b.Property<string>("StudentLastName")
                        .HasColumnType("text");

                    b.Property<int>("StudentLevel")
                        .HasColumnType("integer");

                    b.Property<string>("StudentRegistrationNumber")
                        .HasColumnType("text");

                    b.Property<string>("TransactionReference")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("PaymentRecords");
                });

            modelBuilder.Entity("Models.Student", b =>
                {
                    b.Property<string>("AppUserId")
                        .HasColumnType("text");

                    b.Property<int>("Class")
                        .HasColumnType("integer");

                    b.Property<int?>("ClassAdviserId")
                        .HasColumnType("integer");

                    b.Property<string>("DepartmentId")
                        .HasColumnType("text");

                    b.Property<string>("FacultyId")
                        .HasColumnType("text");

                    b.Property<int>("Level")
                        .HasColumnType("integer");

                    b.Property<string>("RegistrationNumber")
                        .HasColumnType("text");

                    b.HasKey("AppUserId");

                    b.HasIndex("ClassAdviserId");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("FacultyId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("CourseLecturer", b =>
                {
                    b.HasOne("Models.Course", null)
                        .WithMany()
                        .HasForeignKey("CoursesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Lecturer", null)
                        .WithMany()
                        .HasForeignKey("LecturersAppUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CourseStudent", b =>
                {
                    b.HasOne("Models.Course", null)
                        .WithMany()
                        .HasForeignKey("CoursesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Student", null)
                        .WithMany()
                        .HasForeignKey("StudentsAppUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Models.AppUser", b =>
                {
                    b.HasOne("Models.Address", "Addresses")
                        .WithMany()
                        .HasForeignKey("AddressesId");

                    b.Navigation("Addresses");
                });

            modelBuilder.Entity("Models.ClassAdviser", b =>
                {
                    b.HasOne("Models.Lecturer", "Lecturer")
                        .WithMany()
                        .HasForeignKey("LecturerId");

                    b.Navigation("Lecturer");
                });

            modelBuilder.Entity("Models.Course", b =>
                {
                    b.HasOne("Models.Department", "Department")
                        .WithMany("Courses")
                        .HasForeignKey("DepartmentId");

                    b.HasOne("Models.Faculty", null)
                        .WithMany("Courses")
                        .HasForeignKey("FacultyId");

                    b.Navigation("Department");
                });

            modelBuilder.Entity("Models.Department", b =>
                {
                    b.HasOne("Models.Faculty", "Faculty")
                        .WithMany("Departments")
                        .HasForeignKey("FacultyId");

                    b.Navigation("Faculty");
                });

            modelBuilder.Entity("Models.Lecturer", b =>
                {
                    b.HasOne("Models.AppUser", "AppUser")
                        .WithOne("Lecturer")
                        .HasForeignKey("Models.Lecturer", "AppUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Department", "Department")
                        .WithMany("Lecturer")
                        .HasForeignKey("DepartmentId");

                    b.HasOne("Models.Faculty", null)
                        .WithMany("Lecturer")
                        .HasForeignKey("FacultyId");

                    b.Navigation("AppUser");

                    b.Navigation("Department");
                });

            modelBuilder.Entity("Models.NonAcademicStaff", b =>
                {
                    b.HasOne("Models.AppUser", "AppUser")
                        .WithOne("NonAcademicStaff")
                        .HasForeignKey("Models.NonAcademicStaff", "AppUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Department", "Department")
                        .WithMany("NonAcademicStaff")
                        .HasForeignKey("DepartmentId");

                    b.HasOne("Models.NonAcademicStaffPosition", "DutyPost")
                        .WithMany("NonAcademicStaff")
                        .HasForeignKey("DutyPostId");

                    b.HasOne("Models.Faculty", null)
                        .WithMany("NonAcademicStaff")
                        .HasForeignKey("FacultyId");

                    b.Navigation("AppUser");

                    b.Navigation("Department");

                    b.Navigation("DutyPost");
                });

            modelBuilder.Entity("Models.Student", b =>
                {
                    b.HasOne("Models.AppUser", "AppUser")
                        .WithOne("Student")
                        .HasForeignKey("Models.Student", "AppUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.ClassAdviser", "ClassAdviser")
                        .WithMany("Students")
                        .HasForeignKey("ClassAdviserId");

                    b.HasOne("Models.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId");

                    b.HasOne("Models.Faculty", "Faculty")
                        .WithMany("Students")
                        .HasForeignKey("FacultyId");

                    b.Navigation("AppUser");

                    b.Navigation("ClassAdviser");

                    b.Navigation("Department");

                    b.Navigation("Faculty");
                });

            modelBuilder.Entity("Models.AppUser", b =>
                {
                    b.Navigation("Lecturer");

                    b.Navigation("NonAcademicStaff");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Models.ClassAdviser", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("Models.Department", b =>
                {
                    b.Navigation("Courses");

                    b.Navigation("Lecturer");

                    b.Navigation("NonAcademicStaff");
                });

            modelBuilder.Entity("Models.Faculty", b =>
                {
                    b.Navigation("Courses");

                    b.Navigation("Departments");

                    b.Navigation("Lecturer");

                    b.Navigation("NonAcademicStaff");

                    b.Navigation("Students");
                });

            modelBuilder.Entity("Models.NonAcademicStaffPosition", b =>
                {
                    b.Navigation("NonAcademicStaff");
                });
#pragma warning restore 612, 618
        }
    }
}
