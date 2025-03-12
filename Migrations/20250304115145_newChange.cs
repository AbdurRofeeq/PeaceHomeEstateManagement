using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PeaceHomeEstateManagement.Migrations
{
    /// <inheritdoc />
    public partial class newChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AmenitiesProperties",
                table: "AmenitiesProperties");

            migrationBuilder.DropIndex(
                name: "IX_AmenitiesProperties_PropertyId",
                table: "AmenitiesProperties");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("7d547a85-b382-420a-aeaf-ecf8751242a7"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_AmenitiesProperties",
                table: "AmenitiesProperties",
                columns: new[] { "PropertyId", "AmenitiesId" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "IsDeleted", "PasswordHash" },
                values: new object[] { new Guid("0a8ce797-07fa-4e6c-a320-b0260e278e53"), "peacehome@gmail.com", false, "$2a$11$lYBER1rYiw3V3.YuynjFgOSvDUqal.Zm/FElKaewGZaYv/qhdNm7q" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AmenitiesProperties",
                table: "AmenitiesProperties");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0a8ce797-07fa-4e6c-a320-b0260e278e53"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_AmenitiesProperties",
                table: "AmenitiesProperties",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "IsDeleted", "PasswordHash" },
                values: new object[] { new Guid("7d547a85-b382-420a-aeaf-ecf8751242a7"), "peacehome@gmail.com", false, "$2a$11$kELZvTcruUhrWUf.6ss7PO3Why4KWlaidprtOVf4vuRB2GCxhZho6" });

            migrationBuilder.CreateIndex(
                name: "IX_AmenitiesProperties_PropertyId",
                table: "AmenitiesProperties",
                column: "PropertyId");
        }
    }
}
