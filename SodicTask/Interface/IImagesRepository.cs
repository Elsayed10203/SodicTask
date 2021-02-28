using SodicTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SodicTask.Interface
{
    public interface IImagesRepository
    {
        IEnumerable<ProductImage> GetAllObject(int prodId);
        void AddObject(ProductImage Obj);
        
     }
}
