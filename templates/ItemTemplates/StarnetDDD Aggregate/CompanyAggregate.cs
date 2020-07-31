using $projectname$.PL.Commands;
using $projectname$.PL.Events;
using Starnet.Aggregates;
using System.Runtime.CompilerServices;

namespace $rootnamespace$.$fileinputname$
{
    public class $fileinputname$Aggregate : Aggregate
    {
        $fileinputname$AggregateState State;

        public $fileinputname$Aggregate($fileinputname$AggregateState state) : base(state)
            => State = state;

        internal void Create$fileinputname$(Create$fileinputname$ c)
        {
            if (State.Version > 0)
                if (IsIdempotent(c))
                    return;
                else
                    throw DomainError.Named("$fileinputname$AlreadyExists", string.Empty); 
    
            var e = new $fileinputname$Created() 
			{ 
				Id = c.Id, 
				IssuedBy = c.IssuedBy, 
				TimeIssued = c.TimeIssued,
                Name = c.Name
            };
            Apply(e);
        }

            bool IsIdempotent(Create$fileinputname$ c)
                => c.Name == State.Name;
    }
}
