# Recipe-Api

This website is hosted at: http://recipe-api-nu.azurewebsites.net/index.html

The structure of the Recipe response:
```json
{
  "recipeId": 0,
  "recipeImageUrl": "string",
  "recipeName": "string",
  "recipeDifficulty": "string",
  "recipeIngredients": "string",
  "recipeDescription": "string",
  "commentingData": [
    {
      "commentingId": 0,
      "userName": "string",
      "commentText": "string",
      "recipeId": 0
    }
  ]
}
```
The strucutre of the Commenting section response:
```json 
{
  "commentingId": 0,
  "userName": "string",
  "commentText": "string",
  "recipeId": 0
}
```
