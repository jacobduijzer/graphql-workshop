using HotChocolate.Execution;
using HotChocolate.Subscriptions;

namespace ChipsFlicks.Hub.Api;

public class ReservationType : ObjectType
{
    protected override void Configure(IObjectTypeDescriptor descriptor)
    {
        descriptor
            .Field("ReservationAdded")
            //.Type<BookType>()
            .Resolve(context => context.GetEventMessage<Reservation>())
            .Subscribe(async context =>
            {
                var receiver = context.Service<ITopicEventReceiver>();

                ISourceStream stream =
                    await receiver.SubscribeAsync<Reservation>("ReservationAdded");

                return stream;
            });
    }
}