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

namespace RecipeTests
{
    class UntiTesting
    {
        [Test]
        public async Task TestGetRecipe()
        {
            DbContextOptions<RecipeContext> options = new DbContextOptionsBuilder<RecipeContext>().UseInMemoryDatabase(databaseName: "GetRecipe").Options;
            using (var context = new RecipeContext(options))
            {
                context.Recipe.Add(new Recipe()
                {
                    RecipeDescription = "Method1",
                    RecipeDifficulty = "Medium1",
                    RecipeName = "Name1",
                    RecipeIngredients = "Ingredients1"
                });
                context.SaveChanges();
                Recipe recipeItem1 = context.Recipe.First();
                RecipesController recipesController = new RecipesController(context);
                IActionResult result = await recipesController.GetRecipe(recipeItem1.RecipeId) as IActionResult;

                var okObjectResult = result as OkObjectResult;
                Assert.IsNotNull(okObjectResult);

                var model = okObjectResult.Value as Recipe;
                Assert.IsNotNull(model);
                Assert.AreEqual(recipeItem1.RecipeName, model.RecipeName);
            }
        }
        [Test]
        public async Task TestPutRecipeItemUpdate()
        {
            DbContextOptions<RecipeContext> options = new DbContextOptionsBuilder<RecipeContext>().UseInMemoryDatabase(databaseName: "PutRecipe").Options;
            using (var context = new RecipeContext(options))
            {
                context.Recipe.Add(new Recipe()
                {
                    RecipeDescription = "Method1",
                    RecipeDifficulty = "Medium1",
                    RecipeName = "Name1",
                    RecipeIngredients = "Ingredients1"
                });
                context.SaveChanges();
                //Given
                string name = "putRecipe";
                Recipe recipeItem1 = context.Recipe.Where(x => x.RecipeName == "Name1").Single();
                recipeItem1.RecipeName = name;

                //When
                RecipesController recipesController = new RecipesController(context);
                IActionResult result = await recipesController.PutRecipe(recipeItem1.RecipeId, recipeItem1) as IActionResult;

                //Then
                recipeItem1 = context.Recipe.Where(x => x.RecipeName == name).Single();
            }
        }

        [Test]
        public async Task TestGetRecipes()
        {
            DbContextOptions<RecipeContext> options = new DbContextOptionsBuilder<RecipeContext>().UseInMemoryDatabase(databaseName: "GetRecipes").Options;
            using (var context = new RecipeContext(options))
            {
                context.Recipe.Add(new Recipe()
                {
                    RecipeDescription = "Method1",
                    RecipeDifficulty = "Medium1",
                    RecipeName = "Name1",
                    RecipeIngredients = "Ingredients1"
                });
                context.Recipe.Add(new Recipe()
                {
                    RecipeDescription = "Method2",
                    RecipeDifficulty = "Medium2",
                    RecipeName = "Name2",
                    RecipeIngredients = "Ingredients2"
                });
                context.SaveChanges();

                //When
                RecipesController recipeController = new RecipesController(context);
                IActionResult result = await recipeController.GetRecipes() as IActionResult;

                //Then
                var okObjectResult = result as OkObjectResult;
                Assert.IsNotNull(okObjectResult);

                var model = okObjectResult.Value as IEnumerable<Recipe>;
                Assert.IsNotNull(model);
                Assert.AreEqual(2, model.Count());
            }
        }

        [Test]
        public async Task TestPostRecipe()
        {
            DbContextOptions<RecipeContext> options = new DbContextOptionsBuilder<RecipeContext>().UseInMemoryDatabase(databaseName: "PostRecipe").Options;
            using (var contex = new RecipeContext(options))
            {
                //Given
                Recipe recipeItem1 = new Recipe { RecipeName = "russian salad", RecipeDescription = "Description of russian salad", RecipeDifficulty = "Medium", RecipeIngredients = "Ingredients of russian salad", RecipeImageUrl = "https://honestcooking.com/wp-content/uploads/2013/12/Screen-Shot-2013-12-10-at-12.41.37-PM.png" };


                //When
                RecipesController recipesController = new RecipesController(contex);
                IActionResult result = await recipesController.PostRecipe(recipeItem1) as IActionResult;

                //Then 
                var okObjectResult = result as OkObjectResult;
                Assert.IsNotNull(okObjectResult);

                var model = okObjectResult.Value as Recipe;
                Assert.IsNotNull(model);
                Assert.AreNotEqual(0, model.RecipeId);
                Assert.AreEqual(recipeItem1.RecipeDescription, model.RecipeDescription);
                Assert.AreEqual(recipeItem1.RecipeDifficulty, model.RecipeDifficulty);
                Assert.AreEqual(recipeItem1.RecipeImageUrl, model.RecipeImageUrl);
                Assert.AreEqual(recipeItem1.RecipeIngredients, model.RecipeIngredients);
                Assert.AreEqual(recipeItem1.RecipeName, model.RecipeName);

                Assert.AreEqual(true, contex.Recipe.Any(x => x.RecipeId == model.RecipeId));
            }
        }

        [Test]
        public async Task TestDeleteRecipe()
        {
            DbContextOptions<RecipeContext> options = new DbContextOptionsBuilder<RecipeContext>().UseInMemoryDatabase(databaseName: "DeleteRecipe").Options;
            using (var context = new RecipeContext(options))
            {
                context.Recipe.Add(new Recipe()
                {
                    RecipeDescription = "Method1",
                    RecipeDifficulty = "Medium1",
                    RecipeName = "Name1",
                    RecipeIngredients = "Ingredients1"
                });
                context.SaveChanges();
                //Given
                Recipe recipeItem1 = context.Recipe.First();

                //When
                RecipesController recipesController = new RecipesController(context);
                IActionResult result = await recipesController.DeleteRecipe(recipeItem1.RecipeId) as IActionResult;

                //Then
                var okObjectResult = result as OkObjectResult;
                Assert.IsNotNull(okObjectResult);

                var model = okObjectResult.Value as Recipe;
                Assert.IsNotNull(model);
                Assert.AreNotEqual(0, model.RecipeId);
                Assert.AreEqual(recipeItem1.RecipeDescription, model.RecipeDescription);
                Assert.AreEqual(recipeItem1.RecipeDifficulty, model.RecipeDifficulty);
                Assert.AreEqual(recipeItem1.RecipeImageUrl, model.RecipeImageUrl);
                Assert.AreEqual(recipeItem1.RecipeIngredients, model.RecipeIngredients);
                Assert.AreEqual(recipeItem1.RecipeName, model.RecipeName);

                Assert.AreEqual(false, context.Recipe.Any(x => x.RecipeId == recipeItem1.RecipeId));
            }
        }
    }
}
