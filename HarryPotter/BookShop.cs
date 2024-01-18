public class BookShop
{
    private const int BookPrice = 8;
    private readonly IReadOnlyDictionary<int, int> Discounts = new Dictionary<int, int>
    {
        {2, 5},
        {3, 10},
        {4, 20},
        {5, 25}
    };

    public double CalculatePrice(Dictionary<int, int> basket)
    {
        var sets = new List<HashSet<int>>();
        foreach (var book in basket)
        {
            AddToSet(sets, book.Key, book.Value);
        }

        double totalPrice = 0;
        foreach (var set in sets)
        {
            var price = set.Count * BookPrice;
            var discount = Discounts.ContainsKey(set.Count) ? Discounts[set.Count] : 0;
            totalPrice += price * (1 - discount / 100.0);
        }

        return totalPrice;
    }

    private void AddToSet(List<HashSet<int>> sets, int book, int quantity)
    {
        for (var i = 0; i < quantity; i++)
        {
            var existingSet = sets.Find(set => !set.Contains(book));
            if (existingSet == null)
            {
                var newSet = new HashSet<int> { book };
                sets.Add(newSet);
            }
            else
            {
                existingSet.Add(book);
            }
        }
    }
}