﻿using DomainName.ReadModel;
using ServiceStack;

namespace $safeprojectname$
{
    [Route("/lookups")]
    public class GetLookup : IReturn<Lookup>
    {
        public string Id { get; set; }
    }
}
