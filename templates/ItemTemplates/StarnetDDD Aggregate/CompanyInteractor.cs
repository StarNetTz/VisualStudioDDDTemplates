using $projectname$.PL.Commands;
using Starnet.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace $rootnamespace$.$fileinputname$
{
    public interface I$fileinputname$Interactor : IInteractor { }

    public class $fileinputname$Interactor : Interactor, I$fileinputname$Interactor
    {
        readonly IAggregateRepository AggRepository;

        public $fileinputname$Interactor(IAggregateRepository aggRepository)
        {
            AggRepository = aggRepository;
        }

        async Task IdempotentlyCreateAgg(string id, Action<$fileinputname$Aggregate> usingThisMethod)
        {
            var agg = await AggRepository.GetAsync<$fileinputname$Aggregate>(id);
            if (agg == null)
                agg = new $fileinputname$Aggregate(new $fileinputname$AggregateState());
            var ov = agg.Version;
            usingThisMethod(agg);
            PublishedEvents = agg.PublishedEvents;
            if (ov != agg.Version)
                await AggRepository.StoreAsync(agg);
        }

        async Task IdempotentlyUpdateAgg(string id, Action<OrganizationAggregate> usingThisMethod)
        {
            var agg = await AggRepository.GetAsync<$fileinputname$Aggregate>(id);
            if (agg == null)
                throw DomainError.Named("$fileinputname$DoesNotExist", string.Empty);
            var ov = agg.Version;
            usingThisMethod(agg);
            PublishedEvents = agg.PublishedEvents;
            if (ov != agg.Version)
                await AggRepository.StoreAsync(agg);
        }

        public override async Task Execute(object command)
            => await When((dynamic)command);

        private async Task When(Create$fileinputname$ c)
            => await IdempotentlyCreateAgg(c.Id, agg => agg.Create$fileinputname$(c));
    }
}