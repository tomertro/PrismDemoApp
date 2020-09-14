using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using HeaderContent.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Unity;

namespace HeaderContent.Service
{
  public class ProductService
  {
    private JsonSerializerSettings _jsonSettings;
    private IUnityContainer _container;

    public ProductService(IUnityContainer container)
    {
      _container = container;
      _jsonSettings = new JsonSerializerSettings
      {
        NullValueHandling = NullValueHandling.Ignore,
        DefaultValueHandling = DefaultValueHandling.Ignore,
        MissingMemberHandling = MissingMemberHandling.Ignore,

      };
    }

    public List<Product> GetProducts()
    {
      Task<List<Product>> task = Task<List<Product>>.Factory.StartNew(() => {
        var jasonStr = File.ReadAllText(@"..\debug\lib\products.json");
        var result = GetObjectFromJson<List<Product>>(jasonStr);
         return result;
        });
      var products = task.Result;
      return products;
     
    }
    private T GetObjectFromJson<T>(string input)
    {
      try
      {
       
        return JsonConvert.DeserializeObject<T>(input, _jsonSettings);
      }
      catch (Exception ex)
      {
        throw new ApplicationException("GetObjectFromJson", ex);
      }
    }
    
  }
}
