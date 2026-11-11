namespace Shop;

public static class RepairLogic
{
    public static TUser? Authenticate<TUser>(IEnumerable<TUser> users, string login)
        where TUser : class, IRegisteredUser
    {
        if (string.IsNullOrWhiteSpace(login))
            return null;

        return users.FirstOrDefault(u =>
            string.Equals(u.Login, login.Trim(), StringComparison.OrdinalIgnoreCase));
    }

    public static void Back<TLine>(ICollection<TLine> draft)
    {
        // При возврате неподтверждённая заявка сохраняется.
    }

    public static List<TItem> Catalog<TItem>(IEnumerable<TItem> items, string query,
        string category, bool descending) where TItem : ICatalogItem
    {
        query = query.Trim();

        var filtered = items.Where(p =>
            (category == "Все категории" || p.Category == category) &&
            (string.IsNullOrWhiteSpace(query) ||
             p.Name.Contains(query, StringComparison.OrdinalIgnoreCase) ||
             p.Description.Contains(query, StringComparison.OrdinalIgnoreCase)));

        return descending
            ? filtered.OrderByDescending(p => p.DiscountPrice).ThenBy(p => p.Id).ToList()
            : filtered.OrderBy(p => p.DiscountPrice).ThenBy(p => p.Id).ToList();
    }
}