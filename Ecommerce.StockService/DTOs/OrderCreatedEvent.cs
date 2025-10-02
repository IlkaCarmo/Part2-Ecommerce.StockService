namespace Ecommerce.StockService.DTOs
{    
        public class OrderCreatedEvent
        {
            public int OrderId { get; set; }
            public string CustomerId { get; set; }
            public List<OrderItemEvent> Items { get; set; }
        }

        public class OrderItemEvent
        {
            public int ProductId { get; set; }
            public int Quantity { get; set; }
        }
   
}
