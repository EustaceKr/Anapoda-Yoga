using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class Initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "341743f0-asd2–42de-afbf-59kmkkmk72cf6", "02174cf0–9412–4cfe-afbf-59f706d72cf6" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "341743f0-asd2–42de-afbf-59kmkkmk72cf6");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6bd94697-f54e-43eb-a66d-6eeb99a1e433", "ebf7cc8e-13dc-4a20-ba20-57848c8a8385", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "74737dda-843b-479e-a197-228559f4ffe5", "b286561d-6ea3-413a-9fdf-37cc30db5734", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Adress", "City", "ConcurrencyStamp", "CreatedDate", "DateOfBirth", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "MobileNumber", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "Phone", "PhoneNumber", "PhoneNumberConfirmed", "PostalCode", "SecurityStamp", "Sex", "State", "TwoFactorEnabled", "UpdatedDate", "UserName" },
                values: new object[] { "dd481b71-3896-4889-aa6b-d5502cb8064f", 0, null, null, "0ca8852e-ebf3-4e29-bf3f-abcc2017a563", new DateTime(2022, 6, 27, 17, 10, 39, 682, DateTimeKind.Local).AddTicks(6184), null, "tade@tade.com", false, "St", "Kr", false, null, null, null, "EUSTACE", "AQAAAAEAACcQAAAAEGUYw2Lwnx+AxrasarV+jPFbXQ8BYWs3DkQTOyf94i6VxNWZfpQAHAYxBBO9SVhobQ==", null, null, false, null, "3c73a90a-7144-468b-9caf-c76f10eb993d", null, null, false, null, "eustace" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "6bd94697-f54e-43eb-a66d-6eeb99a1e433", "dd481b71-3896-4889-aa6b-d5502cb8064f" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "74737dda-843b-479e-a197-228559f4ffe5");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "6bd94697-f54e-43eb-a66d-6eeb99a1e433", "dd481b71-3896-4889-aa6b-d5502cb8064f" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6bd94697-f54e-43eb-a66d-6eeb99a1e433");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dd481b71-3896-4889-aa6b-d5502cb8064f");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "341743f0-asd2–42de-afbf-59kmkkmk72cf6", "fd5b3926-cd00-424d-8b14-d67f7e7159bf", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Adress", "City", "ConcurrencyStamp", "CreatedDate", "DateOfBirth", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "MobileNumber", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "Phone", "PhoneNumber", "PhoneNumberConfirmed", "PostalCode", "SecurityStamp", "Sex", "State", "TwoFactorEnabled", "UpdatedDate", "UserName" },
                values: new object[] { "02174cf0–9412–4cfe-afbf-59f706d72cf6", 0, null, null, "5d679900-e1bc-4f6c-9c69-b1c38c0ff323", new DateTime(2022, 6, 27, 16, 17, 33, 851, DateTimeKind.Local).AddTicks(3669), null, "tade@tade.com", false, "St", "Kr", false, null, null, null, "EUSTACE", "AQAAAAEAACcQAAAAELh4q+A47j7mBhuw8h3XhK/y8THgN9dZN3HlsyVHOLgJM7156FtnbG+4ePxJLCLRTA==", null, null, false, null, "e0e72a32-8738-473f-ad1a-571daccf68d4", null, null, false, null, "eustace" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "341743f0-asd2–42de-afbf-59kmkkmk72cf6", "02174cf0–9412–4cfe-afbf-59f706d72cf6" });
        }
    }
}
