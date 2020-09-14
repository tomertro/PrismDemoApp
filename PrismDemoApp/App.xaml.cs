using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using HeaderContent;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Unity;
using Unity;

namespace PrismDemoApp
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : PrismApplication
  {
    
    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
      //throw new NotImplementedException();
    }

    protected override Window CreateShell()
    {
      var w = Container.Resolve<MainWindow>();
       return w;
    }

    protected override IModuleCatalog CreateModuleCatalog()
    {
      return new ConfigurationModuleCatalog();
    }
    protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
    {
      moduleCatalog.AddModule<HeaderContentModule>();
    }
  }
}
