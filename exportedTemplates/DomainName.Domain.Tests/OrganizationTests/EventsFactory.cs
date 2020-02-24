using DomainName.PL.Events;
using System;

namespace $safeprojectname$.OrganizationTests
{
    public class EventsFactory
    {
        public static OrganizationRegistered CreateOrganizationRegisteredEvent(string id)
        {
            return new OrganizationRegistered()
            {
                Id = id,
                IssuedBy = "zeko",
                Name = "Xamics Ltd",
                TimeIssued = DateTime.MinValue,
                Address = new PL.Address
                {
                    Street = "321 Bakers Street b",
                    City = "London",
                    Country = "UK",
                    State = "Essex",
                    PostalCode = "3021"
                }
            };
        }

        public static OrganizationNameCorrected CreateOrganizationNameCorrectedEvent(string id)
        {
            return new OrganizationNameCorrected()
            {
                Id = id,
                IssuedBy = "zeko",
                TimeIssued = DateTime.MinValue,
                Name = "New name"
            };
        }
    }
}