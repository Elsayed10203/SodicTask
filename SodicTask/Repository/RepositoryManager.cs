using SodicTask.Interface;
using SodicTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SodicTask.Repository
{
    public class RepositoryManager : IRepositoryManager 
    {
        private IproductRepository _ProductRepsitory;
        private ICategoryRepository _CategoryRepository;
        private IImagesRepository _ImagesRepository;


        public readonly TaskDbContext _RepositoryContext;
        public RepositoryManager(TaskDbContext RepositoryContext) =>
        _RepositoryContext = RepositoryContext;
        public IproductRepository Products
        {
            get
            {
                if (_ProductRepsitory == null)
                {
                    _ProductRepsitory = new productRepository(_RepositoryContext);
                }
                return _ProductRepsitory;
            }
        }
        public ICategoryRepository Categories
        {
            get
            {
                if (_CategoryRepository == null)
                {
                    _CategoryRepository = new CategoryRepository(_RepositoryContext);
                }
                return _CategoryRepository;
            }
        }
        public IImagesRepository ImagesProduct
        {
            get
            {
                if (_ImagesRepository == null)
                {
                    _ImagesRepository = new ImagesRepositorys(_RepositoryContext);
                }
                return _ImagesRepository;
            }
        }

        public async Task SaveChangesAsyn() => await _RepositoryContext.SaveChangesAsync();

      
    }
}