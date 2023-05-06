using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebTruyenChu_Backend.Migrations
{
    public partial class readdchapterindex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChapterIndex",
                table: "Chapters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "12d7a001-5636-48c5-96c0-2971c48216e1", "ADMINISTRATOR" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "f9d3ef4d-e7af-4a47-b53d-f28712a53c4a", "EDITOR" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "3cf3578d-a4a2-4a74-82ef-fa08732781f8", "MEMBER" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "NormalizedEmail", "NormalizedUserName", "PasswordHash" },
                values: new object[] { "9f900489-7510-4df7-a915-06265081e8f7", true, "PP311@gmail.com", "PP311", "AQAAAAEAACcQAAAAEJYwT8wuvMhitmfYj0kKSntYHeZu8bfMKabMQLj/6QOe17N1Op37lL63mm84jWisvw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "NormalizedEmail", "NormalizedUserName", "PasswordHash" },
                values: new object[] { "1f651938-e317-4d39-abae-abb1376de04d", true, "JACKIE007@GMAIL.COM", "JACKIE007", "AQAAAAEAACcQAAAAEELLFE7/UYeFxVg0JKQbjp+WEGdJRHtEHt5h/UTzGGrj+aV++q2LGH5wrKIMsUcLcA==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChapterIndex",
                table: "Chapters");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "56b11d8f-d31d-4ef4-9b44-4df97c575785", null });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "4b44ef28-b7b3-44a9-9f5d-341b6f24e3f7", null });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "319af318-23be-4ca6-89da-b59f4eba35e5", null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "NormalizedEmail", "NormalizedUserName", "PasswordHash" },
                values: new object[] { "4e7d556c-8a43-4a86-8226-f8ac53058878", false, null, null, "AQAAAAEAACcQAAAAENFCqgDo/YGrwW289/kO0gpyYdL/Ff26NRrl4WS+bTT45OaryCKxjhmiyr9dewCQ/A==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "NormalizedEmail", "NormalizedUserName", "PasswordHash" },
                values: new object[] { "e562e271-69b4-4fc3-9660-57d11d4d008d", false, null, null, "AQAAAAEAACcQAAAAEIfS6vDGWzecMs8HLYkkJWYC2tA+gysdNk8y/m/0mhCkfkiTWru9M5KyyZdvjNlyaQ==" });
        }
    }
}
