using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SodicTask.Interface
{
    public interface IRepositoryManager
    {
          ICategoryRepository Categories { get; }
           IproductRepository Products { get; }
           IImagesRepository ImagesProduct{ get; }

          Task SaveChangesAsyn();
    }
}