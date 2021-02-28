using SodicTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SodicTask.Interface
{
    public interface IproductRepository
    {
        IEnumerable<Product> GetAllObject();
        void AddObject(Product Obj);
        Product GetObject(int id);
        void EditProduct(Product Obj);
        void DeleteProduct(Product Obj);
 
         //IEnumerable<Product> GetAllObjectSearchAsync(int parentId);
    }
}