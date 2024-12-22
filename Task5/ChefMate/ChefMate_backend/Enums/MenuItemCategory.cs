namespace ChefMate_backend.Enums
{
    public enum MenuItemCategory
    {
        Appetizers,
        Soups,
        Salads,
        MainCourses,
        Desserts,
        Beverages,
        Starters,
        Sandwiches,
        Burgers,
        Pizzas,
        Pasta,
        Seafood,
        MeatDishes,
        Vegetarian,
        Vegan,
        Grills,
        RiceAndNoodles,
        SideDishes,
        Sauces,
        ExoticDishes,
        SpecialtyDrinks,
        AlcoholicDrinks,
        NonAlcoholicDrinks
    }

    public static class MenuItemCategoryExtensions
    {
        public static string GetCategoryName(this MenuItemCategory category)
        {
            switch (category)
            {
                case MenuItemCategory.Appetizers:
                    return "Закуски";
                case MenuItemCategory.Soups:
                    return "Супи";
                case MenuItemCategory.Salads:
                    return "Салати";
                case MenuItemCategory.MainCourses:
                    return "Основні страви";
                case MenuItemCategory.Desserts:
                    return "Десерти";
                case MenuItemCategory.Beverages:
                    return "Напої";
                case MenuItemCategory.Starters:
                    return "Стартери";
                case MenuItemCategory.Sandwiches:
                    return "Сендвічі";
                case MenuItemCategory.Burgers:
                    return "Бургери";
                case MenuItemCategory.Pizzas:
                    return "Піци";
                case MenuItemCategory.Pasta:
                    return "Паста";
                case MenuItemCategory.Seafood:
                    return "Морепродукти";
                case MenuItemCategory.MeatDishes:
                    return "М'ясні страви";
                case MenuItemCategory.Vegetarian:
                    return "Вегетаріанські страви";
                case MenuItemCategory.Vegan:
                    return "Веганські страви";
                case MenuItemCategory.Grills:
                    return "Страви на грилі";
                case MenuItemCategory.RiceAndNoodles:
                    return "Рис і локшина";
                case MenuItemCategory.SideDishes:
                    return "Гарніри";
                case MenuItemCategory.Sauces:
                    return "Соуси";
                case MenuItemCategory.ExoticDishes:
                    return "Екзотичні страви";
                case MenuItemCategory.SpecialtyDrinks:
                    return "Спеціальні напої";
                case MenuItemCategory.AlcoholicDrinks:
                    return "Алкогольні напої";
                case MenuItemCategory.NonAlcoholicDrinks:
                    return "Безалкогольні напої";
                default:
                    return "Unknown";
            }
        }
    }
}
