using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendAPI.Migrations
{
    public partial class RelattionShipBetweenDashboardWidget : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Widgets_Dashboards_Dashboardid",
                table: "Widgets");

            migrationBuilder.RenameColumn(
                name: "Dashboardid",
                table: "Widgets",
                newName: "DashboardId");

            migrationBuilder.RenameIndex(
                name: "IX_Widgets_Dashboardid",
                table: "Widgets",
                newName: "IX_Widgets_DashboardId");

            migrationBuilder.AlterColumn<int>(
                name: "DashboardId",
                table: "Widgets",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Widgets_Dashboards_DashboardId",
                table: "Widgets",
                column: "DashboardId",
                principalTable: "Dashboards",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Widgets_Dashboards_DashboardId",
                table: "Widgets");

            migrationBuilder.RenameColumn(
                name: "DashboardId",
                table: "Widgets",
                newName: "Dashboardid");

            migrationBuilder.RenameIndex(
                name: "IX_Widgets_DashboardId",
                table: "Widgets",
                newName: "IX_Widgets_Dashboardid");

            migrationBuilder.AlterColumn<int>(
                name: "Dashboardid",
                table: "Widgets",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Widgets_Dashboards_Dashboardid",
                table: "Widgets",
                column: "Dashboardid",
                principalTable: "Dashboards",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
