using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.Core.Entities
{
    public enum Status
    {
        Running = 0,
        Manual,
        Failed,
        Skipped,
        Success,
        Created,
        Canceled
    }
}
