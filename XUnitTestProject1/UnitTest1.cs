using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Controllers;
using WebApplication1.DatabaseModels;
using Xunit;

namespace XUnitTestProject1
{
    public class UnitTest1
    {
        //https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/testing?view=aspnetcore-3.1
        [Fact]
        public async Task Test1()
        {
            //Arrange
            var mock = new Mock<ToDoContext>();
            var options = new DbContextOptionsBuilder<ToDoContext>()
            .UseInMemoryDatabase(databaseName: "MovieListDatabase")
            .Options;

            // Insert seed data into the database using one instance of the context
            using var context = new ToDoContext(options);
            context.TodoItems.Add(new ToDoItem() { Id = 1, Task = "Monday" });
            context.TodoItems.Add(new ToDoItem() { Id = 2, Task = "Tuesday" });
            context.SaveChanges();


            var controller = new ToDoItemsController(context);
            var items = await controller.GetTodoItems();

            Assert.IsType<ActionResult<IEnumerable<ToDoItem>>>(items);
        }
    }
}
