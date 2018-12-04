using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Geosemantic.Data.Migrations
{
    public partial class mig33 : Migration
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
                name: "Page",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTs = table.Column<DateTime>(nullable: false),
                    CreationUserId = table.Column<string>(maxLength: 50, nullable: false),
                    LastChangeTs = table.Column<DateTime>(nullable: true),
                    LastChangeUserId = table.Column<string>(maxLength: 50, nullable: true),
                    StatusType = table.Column<byte>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ShortName = table.Column<string>(nullable: true),
                    Sequence = table.Column<long>(nullable: false),
                    Url = table.Column<string>(nullable: true),
                    ParentId = table.Column<long>(nullable: false),
                    Icon = table.Column<string>(nullable: true),
                    Label = table.Column<string>(nullable: true),
                    HasBadge = table.Column<string>(nullable: true),
                    BadgeText = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Page", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTs = table.Column<DateTime>(nullable: false),
                    CreationUserId = table.Column<string>(maxLength: 50, nullable: false),
                    LastChangeTs = table.Column<DateTime>(nullable: true),
                    LastChangeUserId = table.Column<string>(maxLength: 50, nullable: true),
                    StatusType = table.Column<byte>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    RoleSystemType = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RolePageMapping",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTs = table.Column<DateTime>(nullable: false),
                    CreationUserId = table.Column<string>(maxLength: 50, nullable: false),
                    LastChangeTs = table.Column<DateTime>(nullable: true),
                    LastChangeUserId = table.Column<string>(maxLength: 50, nullable: true),
                    StatusType = table.Column<byte>(nullable: false),
                    PageId = table.Column<long>(nullable: false),
                    RoleId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePageMapping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RolePageMapping_Page_PageId",
                        column: x => x.PageId,
                        principalTable: "Page",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePageMapping_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    Password = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    MobileNumber = table.Column<string>(maxLength: 10, nullable: false),
                    GenderType = table.Column<byte>(nullable: true),
                    UserSystemType = table.Column<byte>(nullable: false),
                    UserStatusType = table.Column<byte>(nullable: false),
                    RoleId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RolePageMapping_PageId",
                table: "RolePageMapping",
                column: "PageId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePageMapping_RoleId",
                table: "RolePageMapping",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId",
                table: "User",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommandAudit");

            migrationBuilder.DropTable(
                name: "RolePageMapping");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Page");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
