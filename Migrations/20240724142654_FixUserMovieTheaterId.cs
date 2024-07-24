using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cinema_ticket_seller_pdi.Migrations
{
    /// <inheritdoc />
    public partial class FixUserMovieTheaterId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_movie_theaters_movie_theater_id",
                table: "users");

            migrationBuilder.AlterColumn<long>(
                name: "movie_theater_id",
                table: "users",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_users_movie_theaters_movie_theater_id",
                table: "users",
                column: "movie_theater_id",
                principalTable: "movie_theaters",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_movie_theaters_movie_theater_id",
                table: "users");

            migrationBuilder.AlterColumn<long>(
                name: "movie_theater_id",
                table: "users",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_users_movie_theaters_movie_theater_id",
                table: "users",
                column: "movie_theater_id",
                principalTable: "movie_theaters",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
