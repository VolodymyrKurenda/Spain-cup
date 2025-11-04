using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Spain_cup_03_11_2025.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Matches_Players_delete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Club_Match_MatchID",
                table: "Club");

            migrationBuilder.DropTable(
                name: "ClubClub_Match");

            migrationBuilder.DropTable(
                name: "Club_Match");

            migrationBuilder.DropColumn(
                name: "Club_id",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "Match_id",
                table: "Club");

            migrationBuilder.AlterColumn<int>(
                name: "MatchID",
                table: "Club",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Club_Match_MatchID",
                table: "Club",
                column: "MatchID",
                principalTable: "Match",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Club_Match_MatchID",
                table: "Club");

            migrationBuilder.AddColumn<int>(
                name: "Club_id",
                table: "Player",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "MatchID",
                table: "Club",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Match_id",
                table: "Club",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Club_Match",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MatchID = table.Column<int>(type: "int", nullable: false),
                    Club1_id = table.Column<int>(type: "int", nullable: false),
                    Club2_id = table.Column<int>(type: "int", nullable: false)
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
                });

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
                name: "IX_Club_Match_MatchID",
                table: "Club_Match",
                column: "MatchID");

            migrationBuilder.CreateIndex(
                name: "IX_ClubClub_Match_Club_MatchesID",
                table: "ClubClub_Match",
                column: "Club_MatchesID");

            migrationBuilder.AddForeignKey(
                name: "FK_Club_Match_MatchID",
                table: "Club",
                column: "MatchID",
                principalTable: "Match",
                principalColumn: "ID");
        }
    }
}
