using SodicTask.Interface;
using SodicTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SodicTask.Repository
{
    public class ImagesRepositorys : RepositoryBase<ProductImage>, IImagesRepository
    {
        public ImagesRepositorys(TaskDbContext RepositoryContext_) : base(RepositoryContext_)
        {

        }
        public void AddObject(ProductImage Obj) => Create(Obj);

        public IEnumerable<ProductImage> GetAllObject(int prodId) => FindByCondition(x => x.ProdId == prodId, false);
         
    }
}
