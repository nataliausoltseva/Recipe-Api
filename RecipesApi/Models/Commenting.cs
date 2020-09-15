using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RecipesApi.Models
{
    public class Commenting
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int CommentingId { get; set; }

        [Required]
        public string UserName { get; set; }
        [Required]
        public string CommentText { get; set; }

        [Required]
        public int RecipeId { get; set; }
    }
}
