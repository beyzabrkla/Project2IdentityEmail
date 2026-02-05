using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project2IdentityEmail.Migrations
{
    /// <inheritdoc />
    public partial class update_spammessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSpam",
                table: "UserMessages",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSpam",
                table: "UserMessages");
        }
    }
}
