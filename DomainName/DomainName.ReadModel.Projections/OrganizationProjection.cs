using DomainName.PL.Events;
using Starnet.Projections;
using System.Threading.Tasks;

namespace DomainName.ReadModel.Projections
{
    [SubscribesToStream(StreamName)]
    public class OrganizationProjection : Projection, IHandledBy<OrganizationProjectionHandler>
    {
        public const string StreamName = "$ce-Organizations";
    }

    public class OrganizationProjectionHandler : IHandler
    {
        readonly INoSqlStore Store;

        public OrganizationProjectionHandler(INoSqlStore store)
        {
            Store = store;
        }

        public async Task Handle(dynamic @event, long checkpoint)
            => await When(@event, checkpoint);

            async Task When(OrganizationRegistered e, long checkpoint)
            {
                var doc = await Store.LoadAsync<Organization>(e.Id);
                if (doc == null)
                    doc = new Organization();
                doc.Id = e.Id;
                doc.Name = e.Name;
                doc.Address = e.Address;
                await Store.StoreAsync(doc);
            }
    }
}
