using System;

namespace shakespeare.core.utilities
{
    public interface INowProvider
    {
        DateTime Now { get; }
    }
}