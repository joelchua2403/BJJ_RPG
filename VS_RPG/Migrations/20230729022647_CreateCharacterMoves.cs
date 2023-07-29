using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VS_RPG.Migrations
{
    /// <inheritdoc />
    public partial class CreateCharacterMoves : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Move_Characters_CharacterId",
                table: "Move");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Move",
                table: "Move");

            migrationBuilder.DropIndex(
                name: "IX_Move_CharacterId",
                table: "Move");

            migrationBuilder.DropColumn(
                name: "CharacterId",
                table: "Move");

            migrationBuilder.RenameTable(
                name: "Move",
                newName: "Moves");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Moves",
                table: "Moves",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CharacterMoves",
                columns: table => new
                {
                    CharacterId = table.Column<int>(type: "integer", nullable: false),
                    MoveId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterMoves", x => new { x.CharacterId, x.MoveId });
                    table.ForeignKey(
                        name: "FK_CharacterMoves_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterMoves_Moves_MoveId",
                        column: x => x.MoveId,
                        principalTable: "Moves",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterMoves_MoveId",
                table: "CharacterMoves",
                column: "MoveId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterMoves");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Moves",
                table: "Moves");

            migrationBuilder.RenameTable(
                name: "Moves",
                newName: "Move");

            migrationBuilder.AddColumn<int>(
                name: "CharacterId",
                table: "Move",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Move",
                table: "Move",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Move_CharacterId",
                table: "Move",
                column: "CharacterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Move_Characters_CharacterId",
                table: "Move",
                column: "CharacterId",
                principalTable: "Characters",
                principalColumn: "Id");
        }
    }
}
