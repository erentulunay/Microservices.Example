using MassTransit;
using Microsoft.EntityFrameworkCore;
using Shared.Events;

namespace Order.API.Consumers
{
    public class PaymentCompletedEventConsumer : IConsumer<PaymentCompletedEvent>
    {
        readonly OrderAPIDbContext _orderAPIDbContext;

        public PaymentCompletedEventConsumer(OrderAPIDbContext orderAPIDbContext)
        {
            _orderAPIDbContext = orderAPIDbContext;
        }

        public async Task Consume(ConsumeContext<PaymentCompletedEvent> context)
        {
            Order.API.Models.Entities.Order order = await _orderAPIDbContext.Orders.FirstOrDefaultAsync(o => o.OrderId == context.Message.OrderId);
            order.OrderStatu = Models.Enums.OrderStatus.Completed;
            await _orderAPIDbContext.SaveChangesAsync();
        }
    }
}
