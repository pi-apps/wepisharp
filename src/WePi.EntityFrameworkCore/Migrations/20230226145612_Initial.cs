using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WePi.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WePipipayment",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    domain = table.Column<string>(type: "text", nullable: true),
                    instanceid = table.Column<Guid>(name: "instance_id", type: "uuid", nullable: false),
                    personid = table.Column<Guid>(name: "person_id", type: "uuid", nullable: false),
                    objcat = table.Column<string>(name: "obj_cat", type: "text", nullable: true),
                    objid = table.Column<Guid>(name: "obj_id", type: "uuid", nullable: false),
                    a2u = table.Column<int>(type: "integer", nullable: false),
                    asset = table.Column<string>(type: "text", nullable: true),
                    fee = table.Column<double>(type: "double precision", nullable: false),
                    step = table.Column<int>(type: "integer", nullable: false),
                    testnet = table.Column<bool>(type: "boolean", nullable: false),
                    finished = table.Column<bool>(type: "boolean", nullable: false),
                    published = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    refid = table.Column<Guid>(name: "ref_id", type: "uuid", nullable: false),
                    comment = table.Column<string>(type: "text", nullable: true),
                    stat = table.Column<string>(type: "text", nullable: true),
                    piuid = table.Column<Guid>(name: "pi_uid", type: "uuid", nullable: false),
                    piusername = table.Column<string>(name: "pi_username", type: "text", nullable: true),
                    identifier = table.Column<string>(type: "text", nullable: true),
                    useruid = table.Column<string>(name: "user_uid", type: "text", nullable: true),
                    amount = table.Column<double>(type: "double precision", nullable: false),
                    memo = table.Column<string>(type: "text", nullable: true),
                    fromaddress = table.Column<string>(name: "from_address", type: "text", nullable: true),
                    toaddress = table.Column<string>(name: "to_address", type: "text", nullable: true),
                    direction = table.Column<string>(type: "text", nullable: true),
                    createdat = table.Column<DateTimeOffset>(name: "created_at", type: "timestamp with time zone", nullable: false),
                    network = table.Column<string>(type: "text", nullable: true),
                    approved = table.Column<bool>(type: "boolean", nullable: false),
                    verified = table.Column<bool>(type: "boolean", nullable: false),
                    completed = table.Column<bool>(type: "boolean", nullable: false),
                    cancelled = table.Column<bool>(type: "boolean", nullable: false),
                    usercancelled = table.Column<bool>(name: "user_cancelled", type: "boolean", nullable: false),
                    txverified = table.Column<bool>(name: "tx_verified", type: "boolean", nullable: false),
                    txid = table.Column<string>(name: "tx_id", type: "text", nullable: true),
                    txlink = table.Column<string>(name: "tx_link", type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WePipipayment", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WePipipayment");
        }
    }
}
