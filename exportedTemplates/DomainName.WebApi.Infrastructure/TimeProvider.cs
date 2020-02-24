using DomainName.WebApi.ServiceInterface;
using System;

namespace $safeprojectname$
{
    public class TimeProvider : ITimeProvider
    {
        public DateTime GetUtcTime()
        {
            return DateTime.UtcNow;
        }
    }
}
