using $projectname$.Domain.$fileinputname$;
using $projectname$.PL.Commands;
using $projectname$.PL.Events;
using Starnet.Aggregates.Testing;
using System.Linq;
using System.Threading.Tasks;

namespace $rootnamespace$.$fileinputname$Tests
{
    internal class _ServiceSpec : ApplicationServiceSpecification<ICommand, IEvent>
    {
        protected override async Task<ExecuteCommandResult<IEvent>> ExecuteCommand(IEvent[] given, ICommand cmd)
        {
            var repository = new BDDAggregateRepository();
            repository.Preload(cmd.Id, given);
            var svc = new $fileinputname$Interactor(repository);
            await svc.Execute(cmd).ConfigureAwait(false);
            var arr = repository.Appended != null ? repository.Appended.Cast<IEvent>().ToArray() : null;
			var publishedEvents = svc.GetPublishedEvents();
			return new ExecuteCommandResult<IEvent> { ProducedEvents = arr ?? new IEvent[0], PublishedEvents = publishedEvents.Cast<IEvent>().ToArray() };
        }
    }
}