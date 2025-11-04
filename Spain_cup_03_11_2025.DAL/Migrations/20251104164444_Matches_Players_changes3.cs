using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Spain_cup_03_11_2025.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Matches_Players_changes3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Club_Match_Club_Clubid",
                table: "Club_Match");

            migrationBuilder.DropIndex(
                name: "IX_Club_Match_Clubid",
                table: "Club_Match");

            migrationBuilder.RenameColumn(
                name: "Clubid",
                table: "Club_Match",
                newName: "Club2_id");

            migrationBuilder.AddColumn<int>(
                name: "Club1_id",
                table: "Club_Match",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ClubClub_Match",
                columns: table => new
                {
                    ClubID = table.Column<int>(type: "int", nullable: false),
                    Club_MatchesID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClubClub_Match", x => new { x.ClubID, x.Club_MatchesID });
                    table.ForeignKey(
                        name: "FK_ClubClub_Match_Club_ClubID",
                        column: x => x.ClubID,
                        principalTable: "Club",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClubClub_Match_Club_Match_Club_MatchesID",
                        column: x => x.Club_MatchesID,
                        principalTable: "Club_Match",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClubClub_Match_Club_MatchesID",
                table: "ClubClub_Match",
                column: "Club_MatchesID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClubClub_Match");

            migrationBuilder.DropColumn(
                name: "Club1_id",
                table: "Club_Match");

            migrationBuilder.RenameColumn(
                name: "Club2_id",
                table: "Club_Match",
                newName: "Clubid");

            migrationBuilder.CreateIndex(
                name: "IX_Club_Match_Clubid",
                table: "Club_Match",
                column: "Clubid");

            migrationBuilder.AddForeignKey(
                name: "FK_Club_Match_Club_Clubid",
                table: "Club_Match",
                column: "Clubid",
                principalTable: "Club",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
