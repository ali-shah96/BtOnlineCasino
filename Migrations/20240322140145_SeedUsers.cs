using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineCasino.Migrations
{
    public partial class SeedUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "GameCollections",
                keyColumn: "Id",
                keyValue: 4,
                column: "ParentCollectionId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReleaseDate",
                value: new DateTime(2024, 3, 22, 15, 1, 44, 774, DateTimeKind.Local).AddTicks(1047));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 2,
                column: "ReleaseDate",
                value: new DateTime(2024, 3, 22, 15, 1, 44, 774, DateTimeKind.Local).AddTicks(1121));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 3,
                column: "ReleaseDate",
                value: new DateTime(2024, 3, 22, 15, 1, 44, 774, DateTimeKind.Local).AddTicks(1180));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 4,
                column: "ReleaseDate",
                value: new DateTime(2024, 3, 22, 15, 1, 44, 774, DateTimeKind.Local).AddTicks(1184));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 5,
                column: "ReleaseDate",
                value: new DateTime(2024, 3, 22, 15, 1, 44, 774, DateTimeKind.Local).AddTicks(1188));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Password", "UserName" },
                values: new object[,]
                {
                    { 1, "password1", "user1" },
                    { 2, "password2", "user2" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "GameCollections",
                keyColumn: "Id",
                keyValue: 4,
                column: "ParentCollectionId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReleaseDate",
                value: new DateTime(2024, 3, 20, 17, 3, 42, 863, DateTimeKind.Local).AddTicks(8266));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 2,
                column: "ReleaseDate",
                value: new DateTime(2024, 3, 20, 17, 3, 42, 863, DateTimeKind.Local).AddTicks(8400));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 3,
                column: "ReleaseDate",
                value: new DateTime(2024, 3, 20, 17, 3, 42, 863, DateTimeKind.Local).AddTicks(8410));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 4,
                column: "ReleaseDate",
                value: new DateTime(2024, 3, 20, 17, 3, 42, 863, DateTimeKind.Local).AddTicks(8419));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 5,
                column: "ReleaseDate",
                value: new DateTime(2024, 3, 20, 17, 3, 42, 863, DateTimeKind.Local).AddTicks(8430));
        }
    }
}
