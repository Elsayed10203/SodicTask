using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SodicTask.Interface;
using SodicTask.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SodicTask.Controllers
{
    public class ProductImagesController : Controller
    {
        private readonly IRepositoryManager Repostitory;
        public IActionResult Index()
        {
            return Content("Work");
        }
         public ProductImagesController(IRepositoryManager Repostitory_)
        {
            Repostitory = Repostitory_;
        }

        [HttpPost]
        public IActionResult Details(Product Prod )
        {
            if(Prod == null)
            {
                return NotFound();
            }
            ViewBag.productId = Prod.ProdId;
            ViewBag.ProdName = Prod.ProdName;
            return View(Repostitory.ImagesProduct.GetAllObject(Prod.ProdId));
        }
        [HttpPost]
        public async Task<RedirectResult> Create(List<IFormFile> files, int ProdId)
        {
            try
            {
                string Extenten = " ";
                ProductImage Obj;
                for (int i = 0; i < files.Count(); i++)
                {

                    Obj = new ProductImage();
                    if (files[i].Length > 0)
                    {
                        Extenten = Path.GetExtension(files[i].FileName);

                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", $"{i}{ProdId}{Extenten}");

                        using (var stream = System.IO.File.Create(filePath))
                        {
                            await files[i].CopyToAsync(stream);
                        }
                    }
                    Obj.ProdId = ProdId;
                    Obj.ProdImages = $"{i}{ProdId}{Extenten}";
                    Repostitory.ImagesProduct.AddObject(Obj);
                    await Repostitory.SaveChangesAsyn();
                }
            }
            catch (Exception ex)
            {

                var Mess= ex.Message;
            }
 
         return  Redirect("/product");
        }
    }
}
