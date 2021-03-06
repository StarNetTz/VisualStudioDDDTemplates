﻿using DomainName.Domain.Organization;
using DomainName.PL.Commands;
using DomainName.PL.Events;
using Starnet.Aggregates.Testing;
using System.Linq;
using System.Threading.Tasks;

namespace DomainName.Domain.Tests
{
    internal class _ServiceSpec : ApplicationServiceSpecification<ICommand, IEvent>
    {
        protected override async Task<ExecuteCommandResult<IEvent>> ExecuteCommand(IEvent[] given, ICommand cmd)
        {
            var repository = new BDDAggregateRepository();
            repository.Preload(cmd.Id, given);
            var svc = new OrganizationInteractor(repository);
            await svc.Execute(cmd).ConfigureAwait(false);
            var publishedEvents = svc.GetPublishedEvents();
            var arr = (repository.Appended != null) ? repository.Appended.Cast<IEvent>().ToArray() : null;
            return new ExecuteCommandResult<IEvent> { ProducedEvents = arr ?? new IEvent[0], PublishedEvents = publishedEvents.Cast<IEvent>().ToArray() };
        }
    }
}