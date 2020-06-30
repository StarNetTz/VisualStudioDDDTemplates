using DomainName.PL.Commands;
using DomainName.PL.Events;
using Starnet.Aggregates;
using System;

namespace DomainName.Domain.Organization
{
    public class OrganizationAggregate : Aggregate
    {
        OrganizationAggregateState State;

        public OrganizationAggregate(OrganizationAggregateState state) : base(state)
        {
            State = state;
        }

        internal void RegisterOrganization(RegisterOrganization c)
        {
            if (State.Version > 0)
                if (IsIdempotent(c))
                    return;
                else
                    throw DomainError.Named("OrganizationAlreadyExists", string.Empty);

            var e = new OrganizationRegistered()
            {
                Id = c.Id,
                IssuedBy = c.IssuedBy,
                TimeIssued = c.TimeIssued,
                Name = c.Name,
                Address = c.Address
            };
            Apply(e);
        }
        
            bool IsIdempotent(RegisterOrganization c)
                => (c.Id, c.Name, c.Address) == (State.Id, c.Name, c.Address);

        internal void CorrectOrganizationName(CorrectOrganizationName c)
        {
            if (State.Name == c.Name)
                return;
            var e = new OrganizationNameCorrected()
            {
                Id = c.Id,
                IssuedBy = c.IssuedBy,
                TimeIssued = c.TimeIssued,
                Name = c.Name
            };
            Apply(e);
        }
    }
}
