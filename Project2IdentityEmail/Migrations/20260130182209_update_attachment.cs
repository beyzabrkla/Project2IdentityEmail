using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project2IdentityEmail.Migrations
{
    /// <inheritdoc />
    public partial class update_attachment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AttachmentUrl",
                table: "UserMessages",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttachmentUrl",
                table: "UserMessages");
        }
    }
}
