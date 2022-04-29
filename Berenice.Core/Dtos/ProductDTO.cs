namespace Berenice.Core.Dtos
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public string Brand { get; set; } = null!;
        public string Category { get; set; } = null!;
        public short ModelYear { get; set; }
        public decimal Price { get; set; }
        public int OrdersCount { get; set; }
    }
}