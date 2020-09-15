using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RecipesApi.Models
{
    public class Recipe
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RecipeId { get; set; }

        public string RecipeImageUrl { get; set; }
        [Required]
        public string RecipeName { get; set; }
        [Required]
        public string RecipeDifficulty { get; set; }

        [Required]
        public string RecipeIngredients { get; set; }
        [Required]
        public string RecipeDescription { get; set; }

        public ICollection<Commenting> CommentingData { get; set; }
    }
}
