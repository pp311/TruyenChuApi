using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebTruyenChu_Backend.Migrations
{
    public partial class adduserdata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, "56b11d8f-d31d-4ef4-9b44-4df97c575785", "Administrator", null },
                    { 2, "4b44ef28-b7b3-44a9-9f5d-341b6f24e3f7", "Editor", null },
                    { 3, "319af318-23be-4ca6-89da-b59f4eba35e5", "Member", null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Avatar", "ConcurrencyStamp", "DateOfBirth", "Email", "EmailConfirmed", "Gender", "Introduction", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { 1, 0, null, "4e7d556c-8a43-4a86-8226-f8ac53058878", new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "pp311@gmail.com", false, "male", "Hello, I am Phuc Phan", false, null, "Phuc Phan", null, null, "AQAAAAEAACcQAAAAENFCqgDo/YGrwW289/kO0gpyYdL/Ff26NRrl4WS+bTT45OaryCKxjhmiyr9dewCQ/A==", null, false, null, false, "pp311" },
                    { 2, 0, null, "e562e271-69b4-4fc3-9660-57d11d4d008d", new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "jackie007@gmail.com", false, "male", "Hello, I am Jack", false, null, "Jack", null, null, "AQAAAAEAACcQAAAAEIfS6vDGWzecMs8HLYkkJWYC2tA+gysdNk8y/m/0mhCkfkiTWru9M5KyyZdvjNlyaQ==", null, false, null, false, "jackie007" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { 2, 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");
        }
    }
}
