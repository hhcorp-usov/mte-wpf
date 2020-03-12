using mteGuides.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace mteGuides
{
    public class mteGuidesModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
 
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<DefaultGuidesPage>();
            //
            containerRegistry.RegisterForNavigation<GuidesEnterprises>();
        }
    }
}