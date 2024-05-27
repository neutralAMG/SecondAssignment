using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecondAssignment.Database.Migrations
{
    /// <inheritdoc />
    public partial class CreateDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    GenreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.GenreId);
                });

            migrationBuilder.CreateTable(
                name: "Producers",
                columns: table => new
                {
                    ProducersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producers", x => x.ProducersId);
                });

            migrationBuilder.CreateTable(
                name: "Series",
                columns: table => new
                {
                    SeriesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImgUrlPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VideoUrlPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrimaryGenreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProducerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SecundaryGenreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GenreId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Series", x => x.SeriesId);
                    table.ForeignKey(
                        name: "FK_Series_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "GenreId");
                    table.ForeignKey(
                        name: "FK_Series_Genres_PrimaryGenreId",
                        column: x => x.PrimaryGenreId,
                        principalTable: "Genres",
                        principalColumn: "GenreId");
                    table.ForeignKey(
                        name: "FK_Series_Genres_SecundaryGenreId",
                        column: x => x.SecundaryGenreId,
                        principalTable: "Genres",
                        principalColumn: "GenreId");
                    table.ForeignKey(
                        name: "FK_Series_Producers_ProducerId",
                        column: x => x.ProducerId,
                        principalTable: "Producers",
                        principalColumn: "ProducersId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Series_GenreId",
                table: "Series",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Series_PrimaryGenreId",
                table: "Series",
                column: "PrimaryGenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Series_ProducerId",
                table: "Series",
                column: "ProducerId");

            migrationBuilder.CreateIndex(
                name: "IX_Series_SecundaryGenreId",
                table: "Series",
                column: "SecundaryGenreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Series");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Producers");
        }
    }
}
