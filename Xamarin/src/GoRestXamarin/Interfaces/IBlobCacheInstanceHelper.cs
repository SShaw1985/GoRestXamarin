using System;
using Akavache;

namespace GoRestXamarin.Interfaces
{
    public interface IBlobCacheInstanceHelper
    {
        void Init();
        IBlobCache LocalMachineCache { get; set; }
    }
}
