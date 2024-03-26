using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineCasino.Migrations
{
    public partial class create : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameCollections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisplayIndex = table.Column<int>(type: "int", nullable: false),
                    ParentCollectionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameCollections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameCollections_GameCollections_ParentCollectionId",
                        column: x => x.ParentCollectionId,
                        principalTable: "GameCollections",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisplayIndex = table.Column<int>(type: "int", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GameCategory = table.Column<int>(type: "int", nullable: false),
                    Thumbnail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AvailableDevices = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameCollectionsGames",
                columns: table => new
                {
                    CollectionsId = table.Column<int>(type: "int", nullable: false),
                    GamesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameCollectionsGames", x => new { x.CollectionsId, x.GamesId });
                    table.ForeignKey(
                        name: "FK_GameCollectionsGames_GameCollections_CollectionsId",
                        column: x => x.CollectionsId,
                        principalTable: "GameCollections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameCollectionsGames_Games_GamesId",
                        column: x => x.GamesId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "GameCollections",
                columns: new[] { "Id", "DisplayIndex", "DisplayName", "ParentCollectionId" },
                values: new object[,]
                {
                    { 1, 1, "Featured Games", null },
                    { 2, 2, "Top Rated", null }
                });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "AvailableDevices", "DisplayIndex", "DisplayName", "GameCategory", "ReleaseDate", "Thumbnail" },
                values: new object[,]
                {
                    { 1, "Desktop", 1, "Game A", 0, new DateTime(2024, 3, 20, 16, 40, 45, 324, DateTimeKind.Local).AddTicks(6754), "" },
                    { 2, "Desktop", 2, "Game B", 0, new DateTime(2024, 3, 20, 16, 40, 45, 324, DateTimeKind.Local).AddTicks(6823), "" },
                    { 3, "Desktop", 1, "Game C", 2, new DateTime(2024, 3, 20, 16, 40, 45, 324, DateTimeKind.Local).AddTicks(6826), "" },
                    { 4, "Desktop", 2, "Game D", 2, new DateTime(2024, 3, 20, 16, 40, 45, 324, DateTimeKind.Local).AddTicks(6829), "" },
                    { 5, "Desktop", 1, "Game E", 0, new DateTime(2024, 3, 20, 16, 40, 45, 324, DateTimeKind.Local).AddTicks(6832), "" }
                });

            migrationBuilder.InsertData(
                table: "GameCollections",
                columns: new[] { "Id", "DisplayIndex", "DisplayName", "ParentCollectionId" },
                values: new object[] { 3, 1, "Classic Slots", 1 });

            migrationBuilder.InsertData(
                table: "GameCollections",
                columns: new[] { "Id", "DisplayIndex", "DisplayName", "ParentCollectionId" },
                values: new object[] { 4, 2, "Table Games", 3 });

            migrationBuilder.InsertData(
                table: "GameCollections",
                columns: new[] { "Id", "DisplayIndex", "DisplayName", "ParentCollectionId" },
                values: new object[] { 5, 1, "Roulette", 4 });

            migrationBuilder.CreateIndex(
                name: "IX_GameCollections_ParentCollectionId",
                table: "GameCollections",
                column: "ParentCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_GameCollectionsGames_GamesId",
                table: "GameCollectionsGames",
                column: "GamesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameCollectionsGames");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "GameCollections");

            migrationBuilder.DropTable(
                name: "Games");
        }
    }
}
