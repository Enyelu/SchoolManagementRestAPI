using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class updateModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PaymentRecords",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    StudentId = table.Column<string>(type: "text", nullable: true),
                    StudentRegistrationNumber = table.Column<string>(type: "text", nullable: true),
                    StudentFirstName = table.Column<string>(type: "text", nullable: true),
                    StudentLastName = table.Column<string>(type: "text", nullable: true),
                    StudentEmail = table.Column<string>(type: "text", nullable: true),
                    StudentDepartment = table.Column<string>(type: "text", nullable: true),
                    StudentLevel = table.Column<int>(type: "integer", nullable: false),
                    PaymentType = table.Column<string>(type: "text", nullable: true),
                    Amount = table.Column<int>(type: "integer", nullable: false),
                    TransactionReference = table.Column<string>(type: "text", nullable: true),
                    IsApproved = table.Column<bool>(type: "boolean", nullable: false),
                    DateCreated = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentRecords", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentRecords");
        }
    }
}
