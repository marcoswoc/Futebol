using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FutebolApi.Migrations
{
    public partial class UserAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9525b3b5-9e62-4479-9cde-beb97ff22676", "5f75fa5a-306c-4376-af8a-f433c70b1486", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "673ab1b2-b6ce-4c0f-877a-d5faadda1dc3", 0, "a6f9b846-398e-4618-b013-8a49feda0967", "admin@gmail.com", true, false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAEGHb4cXayMiG9oMtpclFDa54benKrU+ULF8ZTaCctpxiTU6VOTL5RRxIusg8Kdr4wQ==", null, false, "", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "9525b3b5-9e62-4479-9cde-beb97ff22676", "673ab1b2-b6ce-4c0f-877a-d5faadda1dc3" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "9525b3b5-9e62-4479-9cde-beb97ff22676", "673ab1b2-b6ce-4c0f-877a-d5faadda1dc3" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9525b3b5-9e62-4479-9cde-beb97ff22676");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "673ab1b2-b6ce-4c0f-877a-d5faadda1dc3");
        }
    }
}
