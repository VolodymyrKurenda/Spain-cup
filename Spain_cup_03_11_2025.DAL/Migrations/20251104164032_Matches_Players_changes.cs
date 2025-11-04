using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Spain_cup_03_11_2025.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Matches_Players_changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Club_Match_Player_PlayerID",
                table: "Club_Match");

            migrationBuilder.DropForeignKey(
                name: "FK_ClubMatch_Clubs_ClubsID",
                table: "ClubMatch");

            migrationBuilder.DropForeignKey(
                name: "FK_Player_Clubs_ClubID",
                table: "Player");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Clubs",
                table: "Clubs");

            migrationBuilder.RenameTable(
                name: "Clubs",
                newName: "Club");

            migrationBuilder.RenameColumn(
                name: "PlayerID",
                table: "Club_Match",
                newName: "Clubid");

            migrationBuilder.RenameIndex(
                name: "IX_Club_Match_PlayerID",
                table: "Club_Match",
                newName: "IX_Club_Match_Clubid");

            migrationBuilder.AddColumn<int>(
                name: "Match_id",
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
                name: "PK_Club",
                table: "Club",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Club_Match_Club_Clubid",
                table: "Club_Match",
                column: "Clubid",
                principalTable: "Club",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClubMatch_Club_ClubsID",
                table: "ClubMatch",
                column: "ClubsID",
                principalTable: "Club",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Club_Match_Club_Clubid",
                table: "Club_Match");

            migrationBuilder.DropForeignKey(
                name: "FK_ClubMatch_Club_ClubsID",
                table: "ClubMatch");

            migrationBuilder.DropForeignKey(
                name: "FK_Player_Club_ClubID",
                table: "Player");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Club",
                table: "Club");

            migrationBuilder.DropColumn(
                name: "Match_id",
                table: "Club");

            migrationBuilder.DropColumn(
                name: "Player_id",
                table: "Club");

            migrationBuilder.RenameTable(
                name: "Club",
                newName: "Clubs");

            migrationBuilder.RenameColumn(
                name: "Clubid",
                table: "Club_Match",
                newName: "PlayerID");

            migrationBuilder.RenameIndex(
                name: "IX_Club_Match_Clubid",
                table: "Club_Match",
                newName: "IX_Club_Match_PlayerID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clubs",
                table: "Clubs",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Club_Match_Player_PlayerID",
                table: "Club_Match",
                column: "PlayerID",
                principalTable: "Player",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClubMatch_Clubs_ClubsID",
                table: "ClubMatch",
                column: "ClubsID",
                principalTable: "Clubs",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Player_Clubs_ClubID",
                table: "Player",
                column: "ClubID",
                principalTable: "Clubs",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
