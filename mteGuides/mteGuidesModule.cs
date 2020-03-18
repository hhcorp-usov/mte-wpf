using mteGuides.Views;
using mteModels.Dialogs;
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
            containerRegistry.RegisterDialog<MessageBox>();
            //
            containerRegistry.RegisterDialog<GuidesEnterprises>();
            containerRegistry.RegisterDialog<GuidesPosts>();
            containerRegistry.RegisterDialog<GuidesWorkers>();
            containerRegistry.RegisterDialog<GuidesCarTypes>();
            containerRegistry.RegisterDialog<GuidesCars>();
            containerRegistry.RegisterDialog<GuidesPointTypes>();
            containerRegistry.RegisterDialog<GuidesPoints>();
        }
    }
}