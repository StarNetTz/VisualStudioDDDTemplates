﻿using $ext_projectname$.ReadModel;
using System.Threading.Tasks;

namespace $safeprojectname$.WebApi.Tests
{
    public class StubQueryById<T> : IQueryById<T>
    {
        public Task<T> GetById(string id)
        {
            return Task.FromResult<T>(default(T));
        }
    }
}