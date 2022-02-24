using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendAPI.Migrations
{
    public partial class updatenaming : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "widgetType",
                table: "Widgets",
                newName: "WidgetType");

            migrationBuilder.RenameColumn(
                name: "minWidth",
                table: "Widgets",
                newName: "MinWidth");

            migrationBuilder.RenameColumn(
                name: "minHeight",
                table: "Widgets",
                newName: "MinHeight");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Widgets",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "username",
                table: "Users",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "Users",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "fullName",
                table: "Users",
                newName: "FullName");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Users",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "TaskModels",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "task",
                table: "TaskModels",
                newName: "Task");

            migrationBuilder.RenameColumn(
                name: "isComplete",
                table: "TaskModels",
                newName: "IsComplete");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "TaskModels",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Dashboards",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "title",
                table: "Dashboards",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "layoutType",
                table: "Dashboards",
                newName: "LayoutType");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Dashboards",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "title",
                table: "Contacts",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "project",
                table: "Contacts",
                newName: "Project");

            migrationBuilder.RenameColumn(
                name: "lastName",
                table: "Contacts",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "firstName",
                table: "Contacts",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "employeeId",
                table: "Contacts",
                newName: "EmployeeId");

            migrationBuilder.RenameColumn(
                name: "department",
                table: "Contacts",
                newName: "Department");

            migrationBuilder.RenameColumn(
                name: "avatar",
                table: "Contacts",
                newName: "Avatar");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Contacts",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WidgetType",
                table: "Widgets",
                newName: "widgetType");

            migrationBuilder.RenameColumn(
                name: "MinWidth",
                table: "Widgets",
                newName: "minWidth");

            migrationBuilder.RenameColumn(
                name: "MinHeight",
                table: "Widgets",
                newName: "minHeight");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Widgets",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Users",
                newName: "username");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Users",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Users",
                newName: "fullName");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Users",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Users",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "TaskModels",
                newName: "userId");

            migrationBuilder.RenameColumn(
                name: "Task",
                table: "TaskModels",
                newName: "task");

            migrationBuilder.RenameColumn(
                name: "IsComplete",
                table: "TaskModels",
                newName: "isComplete");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "TaskModels",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Dashboards",
                newName: "userId");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Dashboards",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "LayoutType",
                table: "Dashboards",
                newName: "layoutType");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Dashboards",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Contacts",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "Project",
                table: "Contacts",
                newName: "project");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Contacts",
                newName: "lastName");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Contacts",
                newName: "firstName");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "Contacts",
                newName: "employeeId");

            migrationBuilder.RenameColumn(
                name: "Department",
                table: "Contacts",
                newName: "department");

            migrationBuilder.RenameColumn(
                name: "Avatar",
                table: "Contacts",
                newName: "avatar");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Contacts",
                newName: "id");
        }
    }
}
