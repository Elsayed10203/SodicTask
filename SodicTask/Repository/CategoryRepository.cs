using SodicTask.Interface;
using SodicTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SodicTask.Repository
{
 
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {

        public CategoryRepository(TaskDbContext RepositoryContext_):base(RepositoryContext_)
          {

           }

        public void AddObject(Category Catag) => Create(Catag);

       
        public void EditCatag(Category Catag) => Update(Catag);
        

        public IEnumerable<Category> GetAllObject() => FindAll(trackChanges: false);

        public Category GetObject(int id) => FindByCondition(x => x.CatagNo == id,false).SingleOrDefault();
        public void DeleteCatag(Category Catag) => Delete(Catag);

 
        public  IEnumerable<Category> GetAllObjectSearchAsync(int parentId)
        {
            return  FindByCondition(x=>x.ParentNo== parentId, false);

        }
    }
}