using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Spain_cup_03_11_2025.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Matches_Players_changes2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClubMatch");

            migrationBuilder.AddColumn<int>(
                name: "MatchID",
                table: "Club",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Club_MatchID",
                table: "Club",
                column: "MatchID");

            migrationBuilder.AddForeignKey(
                name: "FK_Club_Match_MatchID",
                table: "Club",
                column: "MatchID",
                principalTable: "Match",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Club_Match_MatchID",
                table: "Club");

            migrationBuilder.DropIndex(
                name: "IX_Club_MatchID",
                table: "Club");

            migrationBuilder.DropColumn(
                name: "MatchID",
                table: "Club");

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
                        name: "FK_ClubMatch_Club_ClubsID",
                        column: x => x.ClubsID,
                        principalTable: "Club",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClubMatch_Match_MatchesID",
                        column: x => x.MatchesID,
                        principalTable: "Match",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClubMatch_MatchesID",
                table: "ClubMatch",
                column: "MatchesID");
        }
    }
}
