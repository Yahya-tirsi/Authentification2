using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CorsesAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoriesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(nullable: false)
                .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    ImageUrl = table.Column<string>(maxLength: 255, nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    Instructor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    ThumbnailUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.CourseId);
                    table.ForeignKey(
                        name: "FK_Courses_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CategoryId",
                table: "Courses",
                column: "CategoryId");


            // Seed data
            migrationBuilder.InsertData(
    table: "Categories",
    columns: new[] { "CategoryId", "Name", "Description", "ImageUrl", "CreatedAt", "UpdatedAt" },
    values: new object[,]
    {
        {
            1,
            "Web Development",
            "Learn modern web technologies",
            "https://www.abc-coursparticuliers.com/wp-content/uploads/2023/07/cours-particuliers-abc-hp.jpg",
            DateTime.UtcNow,
            DateTime.UtcNow
        },
        {
            2,
            "Data Science",
            "Master data analysis and ML",
            "https://img.le-dictionnaire.com/cours-lycee.jpg",
            DateTime.UtcNow,
            DateTime.UtcNow
        }
    });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "CourseId", "CategoryId", "Title", "Description", "Duration", "Instructor", "Price", "ThumbnailUrl", "CreatedAt", "UpdatedAt" },
                values: new object[,]
                {
        {
            1, 1, "Angular Fundamentals", "Learn Angular from scratch", 15, "John Doe", 49.99m,
            "https://www.itrainu.in/wp-content/uploads/2023/08/angular.2-jpg.webp",
            DateTime.UtcNow, DateTime.UtcNow
        },
        {
            2, 1, "ASP.NET Core API", "Build robust web APIs", 20, "Jane Smith", 59.99m,
            "https://www.classcentral.com/report/wp-content/uploads/2024/04/BCG_ASP.NET_banner-1.png",
            DateTime.UtcNow, DateTime.UtcNow
        },
        {
            3, 2, "Machine Learning Basics", "Introduction to ML concepts", 25, "Dr. Alan Turing", 79.99m,
            "https://www.classcentral.com/report/wp-content/uploads/2022/05/ML-BCG-Featured-image.png",
            DateTime.UtcNow, DateTime.UtcNow
        }
                });
        }


/// <inheritdoc />
protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
