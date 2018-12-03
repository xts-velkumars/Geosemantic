using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ste.Data.Migrations
{
    public partial class Mig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CommandAudit",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LoggedOnUserId = table.Column<long>(nullable: false),
                    UtcTimeStamp = table.Column<DateTime>(nullable: false),
                    MessageId = table.Column<string>(nullable: true),
                    IsSuccess = table.Column<bool>(nullable: false),
                    ExceptionMessage = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Data = table.Column<string>(nullable: true),
                    Milliseconds = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommandAudit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTs = table.Column<DateTime>(nullable: false),
                    CreationUserId = table.Column<string>(maxLength: 50, nullable: false),
                    LastChangeTs = table.Column<DateTime>(nullable: true),
                    LastChangeUserId = table.Column<string>(maxLength: 50, nullable: true),
                    StatusType = table.Column<byte>(nullable: false),
                    EmailAddress = table.Column<string>(maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    Password = table.Column<string>(maxLength: 50, nullable: false),
                    MobileNumber = table.Column<string>(maxLength: 10, nullable: false),
                    GenderType = table.Column<byte>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommandAudit");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
