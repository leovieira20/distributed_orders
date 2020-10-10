using System;

namespace ProductInventory.Domain.Exceptions
{
    public class NotEnoughStockForItemException : Exception
    {
        public NotEnoughStockForItemException(string productId)
        {
            ProductId = productId;
        }
        
        public string ProductId { get; }
    }
}