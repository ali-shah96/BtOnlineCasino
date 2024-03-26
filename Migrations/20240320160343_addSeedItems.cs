using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineCasino.Migrations
{
    public partial class addSeedItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameCollectionsGames_GameCollections_CollectionsId",
                table: "GameCollectionsGames");

            migrationBuilder.DropForeignKey(
                name: "FK_GameCollectionsGames_Games_GamesId",
                table: "GameCollectionsGames");

            migrationBuilder.RenameColumn(
                name: "GamesId",
                table: "GameCollectionsGames",
                newName: "GameCollectionId");

            migrationBuilder.RenameColumn(
                name: "CollectionsId",
                table: "GameCollectionsGames",
                newName: "GameId");

            migrationBuilder.RenameIndex(
                name: "IX_GameCollectionsGames_GamesId",
                table: "GameCollectionsGames",
                newName: "IX_GameCollectionsGames_GameCollectionId");

            migrationBuilder.InsertData(
                table: "GameCollectionsGames",
                columns: new[] { "GameCollectionId", "GameId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 2, 3 },
                    { 2, 4 },
                    { 3, 5 }
                });

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

            migrationBuilder.AddForeignKey(
                name: "FK_GameCollectionsGames_GameCollections_GameCollectionId",
                table: "GameCollectionsGames",
                column: "GameCollectionId",
                principalTable: "GameCollections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameCollectionsGames_Games_GameId",
                table: "GameCollectionsGames",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameCollectionsGames_GameCollections_GameCollectionId",
                table: "GameCollectionsGames");

            migrationBuilder.DropForeignKey(
                name: "FK_GameCollectionsGames_Games_GameId",
                table: "GameCollectionsGames");

            migrationBuilder.DeleteData(
                table: "GameCollectionsGames",
                keyColumns: new[] { "GameCollectionId", "GameId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "GameCollectionsGames",
                keyColumns: new[] { "GameCollectionId", "GameId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "GameCollectionsGames",
                keyColumns: new[] { "GameCollectionId", "GameId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "GameCollectionsGames",
                keyColumns: new[] { "GameCollectionId", "GameId" },
                keyValues: new object[] { 2, 4 });

            migrationBuilder.DeleteData(
                table: "GameCollectionsGames",
                keyColumns: new[] { "GameCollectionId", "GameId" },
                keyValues: new object[] { 3, 5 });

            migrationBuilder.RenameColumn(
                name: "GameCollectionId",
                table: "GameCollectionsGames",
                newName: "GamesId");

            migrationBuilder.RenameColumn(
                name: "GameId",
                table: "GameCollectionsGames",
                newName: "CollectionsId");

            migrationBuilder.RenameIndex(
                name: "IX_GameCollectionsGames_GameCollectionId",
                table: "GameCollectionsGames",
                newName: "IX_GameCollectionsGames_GamesId");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReleaseDate",
                value: new DateTime(2024, 3, 20, 16, 40, 45, 324, DateTimeKind.Local).AddTicks(6754));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 2,
                column: "ReleaseDate",
                value: new DateTime(2024, 3, 20, 16, 40, 45, 324, DateTimeKind.Local).AddTicks(6823));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 3,
                column: "ReleaseDate",
                value: new DateTime(2024, 3, 20, 16, 40, 45, 324, DateTimeKind.Local).AddTicks(6826));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 4,
                column: "ReleaseDate",
                value: new DateTime(2024, 3, 20, 16, 40, 45, 324, DateTimeKind.Local).AddTicks(6829));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 5,
                column: "ReleaseDate",
                value: new DateTime(2024, 3, 20, 16, 40, 45, 324, DateTimeKind.Local).AddTicks(6832));

            migrationBuilder.AddForeignKey(
                name: "FK_GameCollectionsGames_GameCollections_CollectionsId",
                table: "GameCollectionsGames",
                column: "CollectionsId",
                principalTable: "GameCollections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameCollectionsGames_Games_GamesId",
                table: "GameCollectionsGames",
                column: "GamesId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
