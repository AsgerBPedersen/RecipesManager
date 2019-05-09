Create Database RecipeManager;

use RecipeManager;

create table Recipes(
Id int primary key identity(1,1),
[Name] varchar(50) not null,
[Description] varchar(max) null
);

create table Ingredients(
Id int primary key identity(1,1),
[Name] varchar(50) not null,
[Type] int not null
);

create table IngredientsInRecipe(
IngredientId int not null,
RecipeId int not null,
Amount int null,
Unit int null,
constraint FK_IngredientsInRecipe_IngredientId foreign key (IngredientId) references Ingredients(Id) on delete cascade,
constraint FK_IngredientsInRecipe_RecipeId foreign key (RecipeId) references Recipes(Id) on delete cascade,
constraint PK_IngredientsInRecipe primary key (IngredientId, RecipeId)
);