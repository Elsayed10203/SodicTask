using SodicTask.Interface;
using SodicTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SodicTask.Repository
{
    public class productRepository: RepositoryBase<Product>, IproductRepository
    {
 
        public productRepository(TaskDbContext RepositoryContext_):base(RepositoryContext_)
        {

        }

        public void AddObject(Product Obj) => Create(Obj);


        public void DeleteProduct(Product Obj) => Delete(Obj);

        public void EditProduct(Product Obj) => Update(Obj);


        public IEnumerable<Product> GetAllObject() => FindAll(false);


        public Product GetObject(int id) => FindByCondition(x=>x.ProdId==id,false).FirstOrDefault();
     }
}