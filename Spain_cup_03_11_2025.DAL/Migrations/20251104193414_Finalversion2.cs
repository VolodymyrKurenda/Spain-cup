using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Spain_cup_03_11_2025.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Finalversion2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MatchID",
                table: "Players",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Goals",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerID = table.Column<int>(type: "int", nullable: false),
                    MatchID = table.Column<int>(type: "int", nullable: false),
                    Minute = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goals", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Goals_Matches_MatchID",
                        column: x => x.MatchID,
                        principalTable: "Matches",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Goals_Players_PlayerID",
                        column: x => x.PlayerID,
                        principalTable: "Players",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Players_MatchID",
                table: "Players",
                column: "MatchID");

            migrationBuilder.CreateIndex(
                name: "IX_Goals_MatchID",
                table: "Goals",
                column: "MatchID");

            migrationBuilder.CreateIndex(
                name: "IX_Goals_PlayerID",
                table: "Goals",
                column: "PlayerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Matches_MatchID",
                table: "Players",
                column: "MatchID",
                principalTable: "Matches",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Matches_MatchID",
                table: "Players");

            migrationBuilder.DropTable(
                name: "Goals");

            migrationBuilder.DropIndex(
                name: "IX_Players_MatchID",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "MatchID",
                table: "Players");
        }
    }
}
