using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BetHive.Wallet.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBatchExternalIdMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ExternalId",
                table: "BatchMovements",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_BatchMovements_ExternalId",
                table: "BatchMovements",
                column: "ExternalId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BatchMovements_ExternalId",
                table: "BatchMovements");

            migrationBuilder.DropColumn(
                name: "ExternalId",
                table: "BatchMovements");
        }
    }
}
