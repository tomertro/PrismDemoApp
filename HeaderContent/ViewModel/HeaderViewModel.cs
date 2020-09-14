using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using HeaderContent.Model;
using HeaderContent.Service;
using HeaderContent.View;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Unity;

namespace HeaderContent.ViewModel
{
  public class HeaderViewModel : BindableBase, IViewModel
  {
    private HeaderControl _view;
    private IRegionManager _regionManager;
    private IUnityContainer _container;
    private ProductService _productService;
    private ContentView _contentView;

    public DelegateCommand LoadDataCommand { get; private set; }
    public DelegateCommand AttachFileCommand { get; private set; }

    public DelegateCommand SelectFileCommand { get; private set; }

    public DelegateCommand SendEmailCommand { get; private set; }
    public ObservableCollection<Product> Products { get; private set; }

    private Visibility _emailVisibility;

    public Visibility EmailVisibility
    {
      get { return _emailVisibility; }
      set
      {
        _emailVisibility = value;
        RaisePropertyChanged("EmailVisibility");
      }
    }

    
    private Visibility _productsTabVisibility;
    public Visibility ProductsTabVisibility
    {
      get { return _productsTabVisibility; }
      set
      {
        _productsTabVisibility = value;
        RaisePropertyChanged("ProductsTabVisibility");
      }
    }

    private bool _emailTabSelected;
    public bool EmailTabSelected
    {
      get { return _emailTabSelected; }
      set { SetProperty(ref _emailTabSelected, value); }
    }

    private bool _productsTabSelected;
    public bool ProductsTabSelected
    {
      get { return _productsTabSelected; }
      set { SetProperty(ref _productsTabSelected, value); }
    }


    private string _selectedFile;
    public string SelectedFile
    {
      get { return _selectedFile; }
      set { SetProperty(ref _selectedFile, value); }
    }
    public HeaderViewModel(IUnityContainer container)
    {
      _container = container;
      EmailVisibility = Visibility.Collapsed;
      ProductsTabVisibility = Visibility.Collapsed;
      Products = new ObservableCollection<Product>();
      _regionManager = _container.Resolve<IRegionManager>();
      _productService = _container.Resolve<ProductService>();
      LoadDataCommand = new DelegateCommand(LoadData, CanLoadData);
      AttachFileCommand = new DelegateCommand(AttachFile);
      SelectFileCommand = new DelegateCommand(SelectFile);

      SendEmailCommand = new DelegateCommand(SendEmail);
    }

    private void SendEmail()
    {
      MessageBox.Show("Mail is sent");
    }

    private void SelectFile()
    {
      OpenFileDialog openFileDialog1 = new OpenFileDialog();
      openFileDialog1.ShowDialog();
      SelectedFile = openFileDialog1.FileName;

    }

    private void AttachFile()
    {
      EmailVisibility = Visibility.Visible;
      EmailTabSelected = true;
      ProductsTabSelected = false;
      //IsSelected = true;
      NavigateToContentView();
    }

    private bool CanLoadData()
    {
      return true;
    }

    private void LoadData()
    {
      var result = _productService.GetProducts();
      Products.Clear();
      foreach (var p in result)
      {
        Products.Add(p);
      }

      EmailTabSelected = false;
      ProductsTabSelected = true;
      ProductsTabVisibility = Visibility.Visible;
      NavigateToContentView();
      
    }

    private void NavigateToContentView()
    {
      if (_contentView == null)
      {
        _contentView = _container.Resolve<ContentView>();
        _regionManager.AddToRegion("Content", _contentView);
      }
    }

    public void LoadView()
    {
      _view = _container.Resolve<HeaderControl>();
      _regionManager.AddToRegion("MainRegion", _view);
    }

  }
}
