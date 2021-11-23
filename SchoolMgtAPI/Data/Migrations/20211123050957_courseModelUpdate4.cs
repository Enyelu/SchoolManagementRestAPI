using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Data.Migrations
{
    public partial class courseModelUpdate4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassAdvisersers_Lecturers_LecturerId",
                table: "ClassAdvisersers");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_ClassAdvisersers_ClassAdviserId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_ClassAdviserId",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClassAdvisersers",
                table: "ClassAdvisersers");

            migrationBuilder.DropIndex(
                name: "IX_ClassAdvisersers_LecturerId",
                table: "ClassAdvisersers");

            migrationBuilder.DropColumn(
                name: "ClassAdviserId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Class",
                table: "ClassAdvisersers");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ClassAdvisersers",
                newName: "Level");

            migrationBuilder.AddColumn<string>(
                name: "ClassAdviserLecturerId",
                table: "Students",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LecturerId",
                table: "ClassAdvisersers",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DateTime",
                table: "ClassAdvisersers",
                type: "text",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<int>(
                name: "Level",
                table: "ClassAdvisersers",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<bool>(
                name: "IsCourseAdviser",
                table: "ClassAdvisersers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClassAdvisersers",
                table: "ClassAdvisersers",
                column: "LecturerId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_ClassAdviserLecturerId",
                table: "Students",
                column: "ClassAdviserLecturerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassAdvisersers_Lecturers_LecturerId",
                table: "ClassAdvisersers",
                column: "LecturerId",
                principalTable: "Lecturers",
                principalColumn: "AppUserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_ClassAdvisersers_ClassAdviserLecturerId",
                table: "Students",
                column: "ClassAdviserLecturerId",
                principalTable: "ClassAdvisersers",
                principalColumn: "LecturerId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassAdvisersers_Lecturers_LecturerId",
                table: "ClassAdvisersers");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_ClassAdvisersers_ClassAdviserLecturerId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_ClassAdviserLecturerId",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClassAdvisersers",
                table: "ClassAdvisersers");

            migrationBuilder.DropColumn(
                name: "ClassAdviserLecturerId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "IsCourseAdviser",
                table: "ClassAdvisersers");

            migrationBuilder.RenameColumn(
                name: "Level",
                table: "ClassAdvisersers",
                newName: "Id");

            migrationBuilder.AddColumn<int>(
                name: "ClassAdviserId",
                table: "Students",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTime",
                table: "ClassAdvisersers",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LecturerId",
                table: "ClassAdvisersers",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ClassAdvisersers",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<string>(
                name: "Class",
                table: "ClassAdvisersers",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClassAdvisersers",
                table: "ClassAdvisersers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Students_ClassAdviserId",
                table: "Students",
                column: "ClassAdviserId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassAdvisersers_LecturerId",
                table: "ClassAdvisersers",
                column: "LecturerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassAdvisersers_Lecturers_LecturerId",
                table: "ClassAdvisersers",
                column: "LecturerId",
                principalTable: "Lecturers",
                principalColumn: "AppUserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_ClassAdvisersers_ClassAdviserId",
                table: "Students",
                column: "ClassAdviserId",
                principalTable: "ClassAdvisersers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
