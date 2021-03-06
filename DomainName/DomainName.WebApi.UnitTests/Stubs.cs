﻿using DomainName.ReadModel;
using DomainName.WebApi.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainName.WebApi.UnitTests
{
    public class StubMessageBus : IMessageBus
    {
        public Task Send(object message)
            => Task.CompletedTask;
    }

    public class StubTimeProvider : ITimeProvider
    {
        public DateTime GetUtcTime()
            => DateTime.MinValue;
    }

    public class StubQueryById : IQueryById
    {
        public Task<T> GetById<T>(string id)
            => Task.FromResult(default(T));
    }

    public class StubOrganizationSmartSearchQuery : IOrganizationSmartSearchQuery
    {
        public Task<PaginatedResult<Organization>> Execute(ISmartSearchQueryRequest qry)
            => Task.FromResult<PaginatedResult<Organization>>(null);
    }

    public class StubTypeaheadSmartSearchQuery : ITypeaheadSmartSearchQuery
    {
        public Task<PaginatedResult<TypeaheadItem>> Execute(ISmartSearchQueryRequest qry)
            => Task.FromResult(new PaginatedResult<TypeaheadItem>());
    }

    public class LookupQueryById : IQueryById
    {
        public Task<T> GetById<T>(string id)
            => Task.FromResult((T)(object)new Lookup() { Id = id, Data = new List<LookupItem> { new LookupItem { Id = "Items-1", Value = "Item 1" } } });
    }
}
