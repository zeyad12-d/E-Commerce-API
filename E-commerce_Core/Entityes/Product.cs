using E_commerce_Core.Entityes;

public class Product
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public DateTime UpdatedAtUtc { get; set; }
    public bool IsActive { get; set; }
    public List<string> Images { get; set; } = new List<string>();
    public double Rating { get; set; }
    public int ReviewCount { get; set; }
    public ICollection<Review> Reviews { get; set; }= new List<Review>();
    public int CategoryId { get; set; }
    public Category Category { get; set; }
     
}
