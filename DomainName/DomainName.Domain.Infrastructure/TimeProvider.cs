﻿using System;

namespace $safeprojectname$
{
    public class TimeProvider : ITimeProvider
    {
        public DateTime GetUtcTime()
            => DateTime.UtcNow;
    }
}
