using System;
using System.Collections.Generic;
using System.Linq;
using GoRestXamarin.Interfaces;
using Prism;
using Prism.Ioc;

namespace GoRestXamarin.iOS
{
    public class iOSInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

            containerRegistry.Register<IBlobCacheInstanceHelper, BlobCacheInstanceHelper>();
        }
    }
}
