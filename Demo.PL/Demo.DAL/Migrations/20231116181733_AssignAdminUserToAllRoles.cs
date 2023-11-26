using Microsoft.EntityFrameworkCore.Migrations;
using System.Collections.Generic;

namespace Demo.DAL.Migrations
{
    public partial class AssignAdminUserToAllRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO[dbo].[AspNetUserRoles] (UserId, RoleId) SELECT 'f091fbd4-14e9-46b9-8559-7d7be5a51fb1', Id FROM[dbo].[AspNetRoles]");
            
		}

		protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [dbo].[AspNetUserRoles] WHERE UserId = 'f091fbd4-14e9-46b9-8559-7d7be5a51fb1'");
        }
    }
}
