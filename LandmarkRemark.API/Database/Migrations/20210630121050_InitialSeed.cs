using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LandmarkRemark.API.Database.Migrations
{
    public partial class InitialSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Username"},
                values: new object[] { 1, "Alan" }
            );

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Username" },
                values: new object[] { 2, "Benny" }
            );

            migrationBuilder.InsertData(
                table: "Notes",
                columns: new[] { "UserId", "Latitude", "Longitude", "Remark", "PostedOn" },
                values: new object[] { 1, 1.3323154771157073, 103.9387509264694, "Delicious prawn noodles", DateTime.Now }
            );

            migrationBuilder.InsertData(
                table: "Notes",
                columns: new[] { "UserId", "Latitude", "Longitude", "Remark", "PostedOn" },
                values: new object[] { 2, 1.2791060223296298, 103.84497611112889, "Tigerspike Singapore", DateTime.Now }
            );

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData("Users", "UserId", 1);
            migrationBuilder.DeleteData("Users", "UserId", 2);
            migrationBuilder.DeleteData("Notes", "UserId", 1);
            migrationBuilder.DeleteData("Notes", "UserId", 2);
        }
    }
}
