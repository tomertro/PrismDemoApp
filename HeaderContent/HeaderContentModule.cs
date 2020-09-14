using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeaderContent.Service;
using HeaderContent.View;
using HeaderContent.ViewModel;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Unity;

namespace HeaderContent
{
  public class HeaderContentModule : IModule
  {
    private IViewModel vm;
    private IUnityContainer _container;

    public HeaderContentModule(IUnityContainer container)
    {
        _container = container;
    }
    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
       vm = new HeaderViewModel(_container);
      containerRegistry.RegisterInstance(vm);
      containerRegistry.Register<ProductService>();
    }

    public void OnInitialized(IContainerProvider containerProvider)
    {
      vm.LoadView();
    }
  }
}

