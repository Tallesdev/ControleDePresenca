using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControleDePresenca.Migrations
{
    /// <inheritdoc />
    public partial class BDPresenca : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Eventos",
                columns: table => new
                {
                    EventoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventoNome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duracao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eventos", x => x.EventoId);
                });

            migrationBuilder.CreateTable(
                name: "Participantes",
                columns: table => new
                {
                    ParticipanteID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParticipanteNome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Matriculas = table.Column<float>(type: "real", nullable: false),
                    EventosID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participantes", x => x.ParticipanteID);
                    table.ForeignKey(
                        name: "FK_Participantes_Eventos_EventosID",
                        column: x => x.EventosID,
                        principalTable: "Eventos",
                        principalColumn: "EventoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Participantes_EventosID",
                table: "Participantes",
                column: "EventosID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Participantes");

            migrationBuilder.DropTable(
                name: "Eventos");
        }
    }
}
