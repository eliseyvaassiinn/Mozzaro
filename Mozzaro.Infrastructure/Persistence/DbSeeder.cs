using Microsoft.EntityFrameworkCore;
using Mozzaro.Domain.Entities;

namespace Mozzaro.Infrastructure.Persistence;

public static class DbSeeder
{
    public static async Task SeedAsync(MozzaroDbContext context)
    {
        await context.Database.MigrateAsync();

        var categories = new Dictionary<string, Category>();

        var categoryNames = new[]
        {
            "Классические",
            "Мясные",
            "Сырные",
            "Острые",
            "Авторские"
        };

        foreach (var name in categoryNames)
        {
            var category = await context.Categories
                .FirstOrDefaultAsync(x => x.Name == name);

            if (category == null)
            {
                category = new Category
                {
                    Name = name
                };

                await context.Categories.AddAsync(category);
                await context.SaveChangesAsync();
            }

            categories[name] = category;
        }

        var ingredientNames = new[]
        {
            "томатный соус",
            "моцарелла",
            "базилик",
            "пепперони",
            "чеддер",
            "пармезан",
            "горгонзола",
            "ветчина",
            "ананас",
            "курица",
            "бекон",
            "грибы",
            "лук",
            "оливки",
            "болгарский перец",
            "халапеньо",
            "кукуруза",
            "помидоры",
            "руккола",
            "чесночный соус",
            "соус барбекю",
            "салями",
            "говядина",
            "фета"
        };

        foreach (var name in ingredientNames)
        {
            var exists = await context.Ingredients
                .AnyAsync(x => x.Name == name);

            if (!exists)
            {
                await context.Ingredients.AddAsync(
                    new Ingredient
                    {
                        Name = name
                    });
            }
        }

        await context.SaveChangesAsync();

        var pizzas = new[]
        {
            new Pizza
            {
                Name = "Маргарита",
                Description = "томатный соус, моцарелла и свежий базилик",
                Price = 8.99m,
                ImageUrl = "/images/pizzas/margarita.jpg",
                IsAvailable = true,
                CategoryId = categories["Классические"].Id
            },

            new Pizza
            {
                Name = "Пепперони",
                Description = "томатный соус, моцарелла и пикантная пепперони",
                Price = 10.49m,
                ImageUrl = "/images/pizzas/pepperoni.jpg",
                IsAvailable = true,
                CategoryId = categories["Мясные"].Id
            },

            new Pizza
            {
                Name = "Четыре сыра",
                Description = "моцарелла, чеддер, пармезан и горгонзола",
                Price = 11.49m,
                ImageUrl = "/images/pizzas/four-cheese.jpg",
                IsAvailable = true,
                CategoryId = categories["Сырные"].Id
            },

            new Pizza
            {
                Name = "Гавайская",
                Description = "томатный соус, моцарелла, ветчина и ананас",
                Price = 10.99m,
                ImageUrl = "/images/pizzas/hawaiian.jpg",
                IsAvailable = true,
                CategoryId = categories["Классические"].Id
            },

            new Pizza
            {
                Name = "Барбекю",
                Description = "соус барбекю, моцарелла, курица и бекон",
                Price = 11.99m,
                ImageUrl = "/images/pizzas/bbq.jpg",
                IsAvailable = true,
                CategoryId = categories["Мясные"].Id
            },

            new Pizza
            {
                Name = "Мясная",
                Description = "томатный соус, моцарелла, ветчина, пепперони и бекон",
                Price = 12.49m,
                ImageUrl = "/images/pizzas/meat.jpg",
                IsAvailable = true,
                CategoryId = categories["Мясные"].Id
            },

            new Pizza
            {
                Name = "Грибная",
                Description = "томатный соус, моцарелла и свежие грибы",
                Price = 9.99m,
                ImageUrl = "/images/pizzas/mushroom.jpg",
                IsAvailable = true,
                CategoryId = categories["Классические"].Id
            },

            new Pizza
            {
                Name = "Ветчина и грибы",
                Description = "моцарелла, ветчина, шампиньоны и томатный соус",
                Price = 10.49m,
                ImageUrl = "/images/pizzas/ham-mushroom.jpg",
                IsAvailable = true,
                CategoryId = categories["Классические"].Id
            },

            new Pizza
            {
                Name = "Салями",
                Description = "томатный соус, моцарелла и итальянская салями",
                Price = 10.99m,
                ImageUrl = "/images/pizzas/salami.jpg",
                IsAvailable = true,
                CategoryId = categories["Мясные"].Id
            },

            new Pizza
            {
                Name = "Карбонара",
                Description = "моцарелла, бекон, пармезан и сливочный соус",
                Price = 11.99m,
                ImageUrl = "/images/pizzas/carbonara.jpg",
                IsAvailable = true,
                CategoryId = categories["Мясные"].Id
            },

            new Pizza
            {
                Name = "Чеддер",
                Description = "моцарелла, чеддер, пармезан и сливочный соус",
                Price = 11.49m,
                ImageUrl = "/images/pizzas/cheddar.jpg",
                IsAvailable = true,
                CategoryId = categories["Сырные"].Id
            },

            new Pizza
            {
                Name = "Сырный микс",
                Description = "моцарелла, чеддер, горгонзола и фета",
                Price = 11.99m,
                ImageUrl = "/images/pizzas/cheese-mix.jpg",
                IsAvailable = true,
                CategoryId = categories["Сырные"].Id
            },

            new Pizza
            {
                Name = "Четыре сезона",
                Description = "моцарелла, ветчина, грибы, оливки и болгарский перец",
                Price = 12.49m,
                ImageUrl = "/images/pizzas/four-seasons.jpg",
                IsAvailable = true,
                CategoryId = categories["Авторские"].Id
            },

            new Pizza
            {
                Name = "Диабло",
                Description = "пепперони, моцарелла, халапеньо и острый соус",
                Price = 11.49m,
                ImageUrl = "/images/pizzas/diablo.jpg",
                IsAvailable = true,
                CategoryId = categories["Острые"].Id
            },

            new Pizza
            {
                Name = "Острая салями",
                Description = "острая салями, моцарелла, халапеньо и томатный соус",
                Price = 11.99m,
                ImageUrl = "/images/pizzas/spicy-salami.jpg",
                IsAvailable = true,
                CategoryId = categories["Острые"].Id
            },

            new Pizza
            {
                Name = "Мексиканская",
                Description = "говядина, моцарелла, кукуруза, перец и халапеньо",
                Price = 12.49m,
                ImageUrl = "/images/pizzas/mexican.jpg",
                IsAvailable = true,
                CategoryId = categories["Острые"].Id
            },

            new Pizza
            {
                Name = "Цыплёнок",
                Description = "курица, моцарелла, помидоры и чесночный соус",
                Price = 10.99m,
                ImageUrl = "/images/pizzas/chicken.jpg",
                IsAvailable = true,
                CategoryId = categories["Мясные"].Id
            },

            new Pizza
            {
                Name = "Цыплёнок ранч",
                Description = "курица, бекон, моцарелла и сливочный соус",
                Price = 11.99m,
                ImageUrl = "/images/pizzas/chicken-ranch.jpg",
                IsAvailable = true,
                CategoryId = categories["Авторские"].Id
            },

            new Pizza
            {
                Name = "Оливковая",
                Description = "моцарелла, оливки, помидоры и свежий базилик",
                Price = 10.49m,
                ImageUrl = "/images/pizzas/olive.jpg",
                IsAvailable = true,
                CategoryId = categories["Классические"].Id
            },

            new Pizza
            {
                Name = "Вегетарианская",
                Description = "моцарелла, грибы, оливки, перец, кукуруза и помидоры",
                Price = 10.99m,
                ImageUrl = "/images/pizzas/vegetarian.jpg",
                IsAvailable = true,
                CategoryId = categories["Авторские"].Id
            },

            new Pizza
            {
                Name = "Руккола",
                Description = "моцарелла, пармезан, помидоры и свежая руккола",
                Price = 11.49m,
                ImageUrl = "/images/pizzas/arugula.jpg",
                IsAvailable = true,
                CategoryId = categories["Авторские"].Id
            },

            new Pizza
            {
                Name = "Пармезан",
                Description = "моцарелла, пармезан, томатный соус и свежий базилик",
                Price = 10.99m,
                ImageUrl = "/images/pizzas/parmesan.jpg",
                IsAvailable = true,
                CategoryId = categories["Сырные"].Id
            },

            new Pizza
            {
                Name = "Итальянская",
                Description = "моцарелла, салями, оливки, помидоры и базилик",
                Price = 12.49m,
                ImageUrl = "/images/pizzas/italian.jpg",
                IsAvailable = true,
                CategoryId = categories["Авторские"].Id
            },

            new Pizza
            {
                Name = "Mozzaro Special",
                Description = "пепперони, курица, бекон, моцарелла и фирменный соус",
                Price = 13.49m,
                ImageUrl = "/images/pizzas/mozzaro-special.jpg",
                IsAvailable = true,
                CategoryId = categories["Авторские"].Id
            }
        };

        foreach (var pizza in pizzas)
        {
            var existingPizza = await context.Pizzas
                .FirstOrDefaultAsync(x => x.Name == pizza.Name);

            if (existingPizza == null)
            {
                await context.Pizzas.AddAsync(pizza);
            }
            else
            {
                existingPizza.Description = pizza.Description;
                existingPizza.Price = pizza.Price;
                existingPizza.ImageUrl = pizza.ImageUrl;
                existingPizza.IsAvailable = pizza.IsAvailable;
                existingPizza.CategoryId = pizza.CategoryId;
            }
        }

        await context.SaveChangesAsync();
    }
}