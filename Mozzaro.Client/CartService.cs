using Mozzaro.Domain.Entities;

namespace Mozzaro.Client.Services;

public class CartService
{
    private readonly List<CartLine> _items = new();

    public IReadOnlyList<CartLine> Items => _items;

    public int TotalItems =>
        _items.Sum(x => x.Quantity);

    public decimal TotalPrice =>
        _items.Sum(x => x.Pizza.Price * x.Quantity);

    public void Add(Pizza pizza)
    {
        var existingItem = _items
            .FirstOrDefault(x => x.Pizza.Id == pizza.Id);

        if (existingItem != null)
        {
            existingItem.Quantity++;
        }
        else
        {
            _items.Add(new CartLine
            {
                Pizza = pizza,
                Quantity = 1
            });
        }
    }

    public void Increase(int pizzaId)
    {
        var item = _items
            .FirstOrDefault(x => x.Pizza.Id == pizzaId);

        if (item != null)
        {
            item.Quantity++;
        }
    }

    public void Decrease(int pizzaId)
    {
        var item = _items
            .FirstOrDefault(x => x.Pizza.Id == pizzaId);

        if (item == null)
        {
            return;
        }

        item.Quantity--;

        if (item.Quantity <= 0)
        {
            _items.Remove(item);
        }
    }

    public void Remove(int pizzaId)
    {
        var item = _items
            .FirstOrDefault(x => x.Pizza.Id == pizzaId);

        if (item != null)
        {
            _items.Remove(item);
        }
    }

    public void Clear()
    {
        _items.Clear();
    }

    public class CartLine
    {
        public Pizza Pizza { get; set; } = null!;

        public int Quantity { get; set; }
    }
}