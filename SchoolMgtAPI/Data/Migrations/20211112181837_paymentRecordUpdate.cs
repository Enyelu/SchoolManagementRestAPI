using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class paymentRecordUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Departments",
                newName: "DateCreated");

            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                table: "PaymentRecords",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "PaymentRecords");

            migrationBuilder.RenameColumn(
                name: "DateCreated",
                table: "Departments",
                newName: "Date");
        }
    }
}
