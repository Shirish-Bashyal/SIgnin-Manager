using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIgnin_Manager.Migrations
{
    /// <inheritdoc />
    public partial class friends : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_FriendRequest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SentById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ReceivedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsAccepted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_FriendRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_FriendRequest_AspNetUsers_ReceivedById",
                        column: x => x.ReceivedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_tbl_FriendRequest_AspNetUsers_SentById",
                        column: x => x.SentById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "tbl_Messages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MsgSentById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    MsgReceivedBYId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_Messages_AspNetUsers_MsgReceivedBYId",
                        column: x => x.MsgReceivedBYId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_tbl_Messages_AspNetUsers_MsgSentById",
                        column: x => x.MsgSentById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_FriendRequest_ReceivedById",
                table: "tbl_FriendRequest",
                column: "ReceivedById");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_FriendRequest_SentById",
                table: "tbl_FriendRequest",
                column: "SentById");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Messages_MsgReceivedBYId",
                table: "tbl_Messages",
                column: "MsgReceivedBYId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Messages_MsgSentById",
                table: "tbl_Messages",
                column: "MsgSentById");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_FriendRequest");

            migrationBuilder.DropTable(
                name: "tbl_Messages");
        }
    }
}
