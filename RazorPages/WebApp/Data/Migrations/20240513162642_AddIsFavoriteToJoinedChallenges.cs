using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIsFavoriteToJoinedChallenges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFavorite",
                table: "JoinedChallenges",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFavorite",
                table: "JoinedChallenges");
        }
    }
}
