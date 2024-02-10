using MassTransit;
using Shared.Events;

namespace Payment.API.Consumers
{
    public class StockReservedEventConsumer : IConsumer<StockReservedEvent>
    {
        readonly IPublishEndpoint _publishEndpoint;

        public StockReservedEventConsumer(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<StockReservedEvent> context)
        {
            if(true)
            {
                PaymentCompletedEvent paymentCompletedEvent = new PaymentCompletedEvent()
                {
                    OrderId = context.Message.OrderId,
                };

                _publishEndpoint.Publish(paymentCompletedEvent);

                Console.WriteLine("Ödeme başarılı...");
            }
            else
            {
                PaymentFailedEvent paymentFailedEvent = new PaymentFailedEvent()
                {
                    OrderId = context.Message.OrderId,
                    Message = "Bakiye yetersiz"
                };
                _publishEndpoint.Publish(paymentFailedEvent);

                Console.WriteLine("Ödeme başarı...");


            }

            await Task.CompletedTask ;
        }
    }
}
