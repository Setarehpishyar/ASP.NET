using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "Budget", "Company", "Description", "EndDate", "ImagePath", "Members", "StartDate", "Status", "Title" },
                values: new object[,]
                {
                    { 1, 10000.00m, "GitLab Inc.", "It is necessary to develop a website redesign in a corporate style.", new DateTime(2025, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/img1.svg", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Website Redesign" },
                    { 2, 8000.00m, "Bitbucket Inc.", "It is necessary to create a landing together with the development of design.", new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/img2.svg", null, new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Landing Page" }
                });
        }
    }
}
