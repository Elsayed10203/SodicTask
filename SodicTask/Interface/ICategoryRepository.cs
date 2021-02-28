using SodicTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SodicTask.Interface
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAllObject();
        void AddObject(Category Catag);
        Category GetObject(int id);
        void EditCatag(Category Catag);
        void DeleteCatag(Category Catag);
       IEnumerable<Category> GetAllObjectSearchAsync(int parentId);

    }
}