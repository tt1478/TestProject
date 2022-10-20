using Microsoft.EntityFrameworkCore.Migrations;

namespace TestProject.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: false),
                    JobId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "Id", "Description" },
                values: new object[,]
                {
                    { 1, "Project Manager" },
                    { 2, "Program Manager" },
                    { 3, "Data Analyst" },
                    { 4, "Inspector" },
                    { 5, "Operations Manager" },
                    { 6, "Civil Engineering Intern" },
                    { 7, "Assistant Process Engineer" },
                    { 8, "ACCOUNTANT" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "FullName", "JobId", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "Mark Keeling", 1, "965522022" },
                    { 9, "Shaun Burton", 1, "787522552" },
                    { 2, "Destiny Hays", 2, "685255620" },
                    { 3, "Lorcan Harrington", 3, "454751215" },
                    { 4, "Alec Townsend", 4, "582256325" },
                    { 5, "Jethro Cortes", 5, "215525555" },
                    { 6, "Dru Black", 6, "126582825" },
                    { 7, "Claudia Barker", 7, "784446225" },
                    { 8, "Kunal Farrell", 8, "576652555" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_JobId",
                table: "Employees",
                column: "JobId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Jobs");
        }
    }
}
