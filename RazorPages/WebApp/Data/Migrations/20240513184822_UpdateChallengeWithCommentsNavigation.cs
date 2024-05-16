using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Data.Migrations
{
    public partial class UpdateChallengeWithCommentsNavigation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Challenges_Comments_ChallengeId",
                table: "Challenges");
        }
    }
}
