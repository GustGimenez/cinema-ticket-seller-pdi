using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace cinema_ticket_seller_pdi.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "customers",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    document = table.Column<string>(type: "varchar(11)", nullable: false),
                    birth_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    name = table.Column<string>(type: "varchar(100)", nullable: false),
                    password = table.Column<string>(type: "varchar(20)", nullable: false),
                    active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "movie_theaters",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_movie_theaters", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    total = table.Column<decimal>(type: "numeric", nullable: false),
                    ticket_quantity = table.Column<int>(type: "integer", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    movie_session_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.id);
                    table.ForeignKey(
                        name: "FK_orders_customers_movie_session_id",
                        column: x => x.movie_session_id,
                        principalTable: "customers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "employees",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    movie_theater_id = table.Column<long>(type: "bigint", nullable: false),
                    MovieTheaterId = table.Column<long>(type: "bigint", nullable: false),
                    name = table.Column<string>(type: "varchar(100)", nullable: false),
                    password = table.Column<string>(type: "varchar(20)", nullable: false),
                    active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employees", x => x.id);
                    table.ForeignKey(
                        name: "FK_employees_movie_theaters_MovieTheaterId",
                        column: x => x.MovieTheaterId,
                        principalTable: "movie_theaters",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "movies",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "varchar(100)", nullable: false),
                    parental_rating = table.Column<string>(type: "text", nullable: false),
                    movie_theater_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_movies", x => x.id);
                    table.ForeignKey(
                        name: "FK_movies_movie_theaters_movie_theater_id",
                        column: x => x.movie_theater_id,
                        principalTable: "movie_theaters",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "movie_sessions",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    from = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    to = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    seats = table.Column<int>(type: "integer", nullable: false),
                    active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    price = table.Column<decimal>(type: "numeric", nullable: false),
                    movie_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_movie_sessions", x => x.id);
                    table.ForeignKey(
                        name: "FK_movie_sessions_movies_movie_id",
                        column: x => x.movie_id,
                        principalTable: "movies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tickets",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    order_id = table.Column<long>(type: "bigint", nullable: false),
                    movie_session_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tickets", x => x.id);
                    table.ForeignKey(
                        name: "FK_tickets_movie_sessions_movie_session_id",
                        column: x => x.movie_session_id,
                        principalTable: "movie_sessions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tickets_orders_order_id",
                        column: x => x.order_id,
                        principalTable: "orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_customers_document",
                table: "customers",
                column: "document",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_employees_MovieTheaterId",
                table: "employees",
                column: "MovieTheaterId");

            migrationBuilder.CreateIndex(
                name: "IX_movie_sessions_movie_id",
                table: "movie_sessions",
                column: "movie_id");

            migrationBuilder.CreateIndex(
                name: "IX_movies_movie_theater_id",
                table: "movies",
                column: "movie_theater_id");

            migrationBuilder.CreateIndex(
                name: "IX_movies_name",
                table: "movies",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_orders_movie_session_id",
                table: "orders",
                column: "movie_session_id");

            migrationBuilder.CreateIndex(
                name: "IX_tickets_movie_session_id",
                table: "tickets",
                column: "movie_session_id");

            migrationBuilder.CreateIndex(
                name: "IX_tickets_order_id",
                table: "tickets",
                column: "order_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "employees");

            migrationBuilder.DropTable(
                name: "tickets");

            migrationBuilder.DropTable(
                name: "movie_sessions");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "movies");

            migrationBuilder.DropTable(
                name: "customers");

            migrationBuilder.DropTable(
                name: "movie_theaters");
        }
    }
}
