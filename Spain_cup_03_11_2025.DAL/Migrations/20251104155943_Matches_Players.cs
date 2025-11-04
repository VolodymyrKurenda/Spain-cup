using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Spain_cup_03_11_2025.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Matches_Players : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Match",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    club1_id = table.Column<int>(type: "int", nullable: false),
                    club2_id = table.Column<int>(type: "int", nullable: false),
                    club1_scored = table.Column<int>(type: "int", nullable: false),
                    club2_scored = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Match", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Player",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClubID = table.Column<int>(type: "int", nullable: false),
                    Club_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Player_Clubs_ClubID",
                        column: x => x.ClubID,
                        principalTable: "Clubs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClubMatch",
                columns: table => new
                {
                    ClubsID = table.Column<int>(type: "int", nullable: false),
                    MatchesID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClubMatch", x => new { x.ClubsID, x.MatchesID });
                    table.ForeignKey(
                        name: "FK_ClubMatch_Clubs_ClubsID",
                        column: x => x.ClubsID,
                        principalTable: "Clubs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClubMatch_Match_MatchesID",
                        column: x => x.MatchesID,
                        principalTable: "Match",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Club_Match",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerID = table.Column<int>(type: "int", nullable: false),
                    MatchID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Club_Match", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Club_Match_Match_MatchID",
                        column: x => x.MatchID,
                        principalTable: "Match",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Club_Match_Player_PlayerID",
                        column: x => x.PlayerID,
                        principalTable: "Player",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Club_Match_MatchID",
                table: "Club_Match",
                column: "MatchID");

            migrationBuilder.CreateIndex(
                name: "IX_Club_Match_PlayerID",
                table: "Club_Match",
                column: "PlayerID");

            migrationBuilder.CreateIndex(
                name: "IX_ClubMatch_MatchesID",
                table: "ClubMatch",
                column: "MatchesID");

            migrationBuilder.CreateIndex(
                name: "IX_Player_ClubID",
                table: "Player",
                column: "ClubID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Club_Match");

            migrationBuilder.DropTable(
                name: "ClubMatch");

            migrationBuilder.DropTable(
                name: "Player");

            migrationBuilder.DropTable(
                name: "Match");
        }
    }
}
