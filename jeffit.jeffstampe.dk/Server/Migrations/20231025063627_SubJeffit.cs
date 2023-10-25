using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jeffit.jeffstampe.dk.Server.Migrations
{
    /// <inheritdoc />
    public partial class SubJeffit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SubJeffit",
                table: "Threads",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubJeffit",
                table: "Threads");
        }
    }
}
