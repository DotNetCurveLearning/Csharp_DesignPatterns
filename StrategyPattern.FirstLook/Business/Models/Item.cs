public partial class Item
{
    public string Id { get; }
    public string Name { get; }
    public decimal Price { get; }
    public ItemType ItemType { get; set; }

    public Item(string id, string name, decimal price, ItemType itemType)
    {
        Id = id;
        Name = name;
        Price = price;
        ItemType = itemType;
    }
}