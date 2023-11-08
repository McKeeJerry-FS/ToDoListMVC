using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoListMVC.Data.Migrations
{
    /// <inheritdoc />
    public partial class _002_Updates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accessory_AspNetUsers_AppUserId",
                table: "Accessory");

            migrationBuilder.DropForeignKey(
                name: "FK_AccessoryToDoItem_Accessory_AccessoriesId",
                table: "AccessoryToDoItem");

            migrationBuilder.DropForeignKey(
                name: "FK_AccessoryToDoItem_ToDoItem_ToDoItemsId",
                table: "AccessoryToDoItem");

            migrationBuilder.DropForeignKey(
                name: "FK_ToDoItem_AspNetUsers_AppUserId",
                table: "ToDoItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ToDoItem",
                table: "ToDoItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Accessory",
                table: "Accessory");

            migrationBuilder.RenameTable(
                name: "ToDoItem",
                newName: "ToDoItems");

            migrationBuilder.RenameTable(
                name: "Accessory",
                newName: "Accessories");

            migrationBuilder.RenameIndex(
                name: "IX_ToDoItem_AppUserId",
                table: "ToDoItems",
                newName: "IX_ToDoItems_AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Accessory_AppUserId",
                table: "Accessories",
                newName: "IX_Accessories_AppUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ToDoItems",
                table: "ToDoItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Accessories",
                table: "Accessories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Accessories_AspNetUsers_AppUserId",
                table: "Accessories",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AccessoryToDoItem_Accessories_AccessoriesId",
                table: "AccessoryToDoItem",
                column: "AccessoriesId",
                principalTable: "Accessories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccessoryToDoItem_ToDoItems_ToDoItemsId",
                table: "AccessoryToDoItem",
                column: "ToDoItemsId",
                principalTable: "ToDoItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ToDoItems_AspNetUsers_AppUserId",
                table: "ToDoItems",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accessories_AspNetUsers_AppUserId",
                table: "Accessories");

            migrationBuilder.DropForeignKey(
                name: "FK_AccessoryToDoItem_Accessories_AccessoriesId",
                table: "AccessoryToDoItem");

            migrationBuilder.DropForeignKey(
                name: "FK_AccessoryToDoItem_ToDoItems_ToDoItemsId",
                table: "AccessoryToDoItem");

            migrationBuilder.DropForeignKey(
                name: "FK_ToDoItems_AspNetUsers_AppUserId",
                table: "ToDoItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ToDoItems",
                table: "ToDoItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Accessories",
                table: "Accessories");

            migrationBuilder.RenameTable(
                name: "ToDoItems",
                newName: "ToDoItem");

            migrationBuilder.RenameTable(
                name: "Accessories",
                newName: "Accessory");

            migrationBuilder.RenameIndex(
                name: "IX_ToDoItems_AppUserId",
                table: "ToDoItem",
                newName: "IX_ToDoItem_AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Accessories_AppUserId",
                table: "Accessory",
                newName: "IX_Accessory_AppUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ToDoItem",
                table: "ToDoItem",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Accessory",
                table: "Accessory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Accessory_AspNetUsers_AppUserId",
                table: "Accessory",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AccessoryToDoItem_Accessory_AccessoriesId",
                table: "AccessoryToDoItem",
                column: "AccessoriesId",
                principalTable: "Accessory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccessoryToDoItem_ToDoItem_ToDoItemsId",
                table: "AccessoryToDoItem",
                column: "ToDoItemsId",
                principalTable: "ToDoItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ToDoItem_AspNetUsers_AppUserId",
                table: "ToDoItem",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
