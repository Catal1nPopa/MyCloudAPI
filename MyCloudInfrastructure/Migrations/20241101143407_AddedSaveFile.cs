using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyCloudInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedSaveFile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "FileLength",
                table: "files",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileLength",
                table: "files");
        }
    }
}
