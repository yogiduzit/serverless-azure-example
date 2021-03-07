using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Data.Migrations
{
    public partial class M1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentId = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    School = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentId);
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "StudentId", "FirstName", "LastName", "School" },
                values: new object[,]
                {
                    { "0e0bdef2-1a54-495e-aeb1-8f82a38a7a08", "Jane", "Smith", "Medicine" },
                    { "56327f98-43b5-4f5e-a0b7-d7114e629daa", "John", "Fisher", "Engineering" },
                    { "96536e8a-7066-4f57-a40d-21d5986dd7a2", "Pamela", "Baker", "Food Science" },
                    { "27c3e3e3-7e0a-4526-a9d9-d765f8943244", "Peter", "Taylor", "Mining" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}
