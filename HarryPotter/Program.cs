BookShop shop = new BookShop();
var basket = new Dictionary<int, int> { { 1, 2 }, { 2, 2 }, { 3, 2 }, { 4, 1 }, { 5, 1 } };
Console.WriteLine(shop.CalculatePrice(basket));