namespace Berenice.Core.Dtos
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public byte OrderStatus { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }

        public virtual CustomerDTO Customer { get; set; } = null!;
        public virtual ProductDTO Product { get; set; } = null!;
    }
}
