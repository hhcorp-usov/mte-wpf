using mteWayBills.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace mteWayBills
{
    public class mteWayBillsModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
 
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<DefaultDocumentsPage>();
        }
    }
}