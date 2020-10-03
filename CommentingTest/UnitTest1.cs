using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using RecipesApi.Controllers;
using RecipesApi.Data;
using RecipesApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommentingTest
{
   class UntiTesting
    {
        [Test]
        public async Task TestGetRecipe()
        {
            DbContextOptions<RecipeContext> options = new DbContextOptionsBuilder<RecipeContext>().UseInMemoryDatabase(databaseName: "GetComment").Options;
            using (var context = new RecipeContext(options))
            {
                context.Commenting.Add(new Commenting()
                {
                    UserName = "User1",
                    CommentText = "Comment1"
                });
                context.SaveChanges();
                Commenting commentItem1 = context.Commenting.First();
                CommentingsController commentingsController = new CommentingsController(context);
                IActionResult result = await commentingsController.GetCommenting(commentItem1.CommentingId) as ActionResult;

                var okObjectResult = result as OkObjectResult;
                Assert.IsNotNull(okObjectResult);

                var model = okObjectResult.Value as Commenting;
                Assert.IsNotNull(model);
                Assert.AreEqual(commentItem1.UserName, model.UserName);
            }
        }
        [Test]
        public async Task TestPutRecipeItemUpdate()
        {
            DbContextOptions<RecipeContext> options = new DbContextOptionsBuilder<RecipeContext>().UseInMemoryDatabase(databaseName: "PutComment").Options;
            using (var context = new RecipeContext(options))
            {
                context.Commenting.Add(new Commenting()
                {
                    UserName = "User1",
                    CommentText = "Comment1"
                });
                context.SaveChanges();
                //Given
                string name = "putRecipe";
                Commenting commentingItem1 = context.Commenting.Where(x => x.UserName == "User1").Single();
                commentingItem1.UserName = name;

                //When
                CommentingsController commentingsController = new CommentingsController(context);
                IActionResult result = await commentingsController.PutCommenting(commentingItem1.CommentingId, commentingItem1) as IActionResult;

                //Then
                commentingItem1 = context.Commenting.Where(x => x.UserName == name).Single();
            }
        }

        [Test]
        public async Task TestDeleteRecipe()
        {
            DbContextOptions<RecipeContext> options = new DbContextOptionsBuilder<RecipeContext>().UseInMemoryDatabase(databaseName: "DeleteRecipe").Options;
            using (var context = new RecipeContext(options))
            {
                context.Commenting.Add(new Commenting()
                {
                    UserName = "User1",
                    CommentText = "Comment1"
                });
                context.SaveChanges();
                //Given
                Commenting commentingItem1 = context.Commenting.First();

                //When
                CommentingsController commentingsController = new CommentingsController(context);
                IActionResult result = await commentingsController.DeleteCommenting(commentingItem1.CommentingId) as IActionResult;

                //Then
                var okObjectResult = result as OkObjectResult;
                Assert.IsNotNull(okObjectResult);

                var model = okObjectResult.Value as Commenting;
                Assert.IsNotNull(model);
                Assert.AreNotEqual(0, model.CommentingId);
                Assert.AreEqual(commentingItem1.UserName, model.UserName);
                Assert.AreEqual(commentingItem1.CommentText, model.CommentText);

                Assert.AreEqual(false, context.Commenting.Any(x => x.CommentingId == commentingItem1.CommentingId));
            }
        }
    }
}