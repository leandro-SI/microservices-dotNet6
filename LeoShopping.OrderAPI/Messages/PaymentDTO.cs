﻿using LeoShopping.MessageBus;

namespace LeoShopping.OrderAPI.Messages
{
    public class PaymentDTO : BaseMessage
    {
        public long OrderId { get; set; }
        public string Name { get; set; }
        public string CardNumber { get; set; }
        public string CVV { get; set; }
        public string ExpiryMonthYear { get; set; }
        public decimal PurchageAmount { get; set; }
        public string Email { get; set; }
    }
}
