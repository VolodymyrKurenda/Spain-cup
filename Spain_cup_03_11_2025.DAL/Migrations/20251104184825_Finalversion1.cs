using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Spain_cup_03_11_2025.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Finalversion1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Club_Match_MatchID",
                table: "Club");

            migrationBuilder.DropForeignKey(
                name: "FK_Player_Club_ClubID",
                table: "Player");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Player",
                table: "Player");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Match",
                table: "Match");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Club",
                table: "Club");

            migrationBuilder.DropIndex(
                name: "IX_Club_MatchID",
                table: "Club");

            migrationBuilder.DropColumn(
                name: "club1_id",
                table: "Match");

            migrationBuilder.DropColumn(
                name: "club2_id",
                table: "Match");

            migrationBuilder.DropColumn(
                name: "MatchID",
                table: "Club");

            migrationBuilder.DropColumn(
                name: "Player_id",
                table: "Club");

            migrationBuilder.RenameTable(
                name: "Player",
                newName: "Players");

            migrationBuilder.RenameTable(
                name: "Match",
                newName: "Matches");

            migrationBuilder.RenameTable(
                name: "Club",
                newName: "Clubs");

            migrationBuilder.RenameIndex(
                name: "IX_Player_ClubID",
                table: "Players",
                newName: "IX_Players_ClubID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Players",
                table: "Players",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Matches",
                table: "Matches",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clubs",
                table: "Clubs",
                column: "ID");

            migrationBuilder.CreateTable(
                name: "Club_Matches",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Club1_id = table.Column<int>(type: "int", nullable: false),
                    Club2_id = table.Column<int>(type: "int", nullable: false),
                    MatchID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Club_Matches", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Club_Matches_Matches_MatchID",
                        column: x => x.MatchID,
                        principalTable: "Matches",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Club_Matches_MatchID",
                table: "Club_Matches",
                column: "MatchID");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Clubs_ClubID",
                table: "Players",
                column: "ClubID",
                principalTable: "Clubs",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Clubs_ClubID",
                table: "Players");

            migrationBuilder.DropTable(
                name: "Club_Matches");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Players",
                table: "Players");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Matches",
                table: "Matches");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Clubs",
                table: "Clubs");

            migrationBuilder.RenameTable(
                name: "Players",
                newName: "Player");

            migrationBuilder.RenameTable(
                name: "Matches",
                newName: "Match");

            migrationBuilder.RenameTable(
                name: "Clubs",
                newName: "Club");

            migrationBuilder.RenameIndex(
                name: "IX_Players_ClubID",
                table: "Player",
                newName: "IX_Player_ClubID");

            migrationBuilder.AddColumn<int>(
                name: "club1_id",
                table: "Match",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "club2_id",
                table: "Match",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MatchID",
                table: "Club",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Player_id",
                table: "Club",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Player",
                table: "Player",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Match",
                table: "Match",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Club",
                table: "Club",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_Club_MatchID",
                table: "Club",
                column: "MatchID");

            migrationBuilder.AddForeignKey(
                name: "FK_Club_Match_MatchID",
                table: "Club",
                column: "MatchID",
                principalTable: "Match",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Player_Club_ClubID",
                table: "Player",
                column: "ClubID",
                principalTable: "Club",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
