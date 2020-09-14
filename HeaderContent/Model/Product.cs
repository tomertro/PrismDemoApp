using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HeaderContent.Model
{
  [DataContract]
  public class Product
  {
    [DataMember]
    public int ProductId { get; set; }
    [DataMember]
    public string ProductName { get; set; }
    [DataMember]
    public string  ProductCode { get; set; }
    [DataMember]
    public DateTime ReleaseDate { get; set; }
    [DataMember]
    public string Description { get; set; }
    [DataMember]
    public decimal Price { get; set; }
    [DataMember]
    public decimal StarRating { get; set; }
   // [DataMember]
   // public string ImageUrl { get; set; }
    

  }
}
