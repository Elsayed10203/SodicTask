using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
 using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SodicTask.Interface;
using SodicTask.Models;
using System.Threading.Tasks;

namespace SodicTask.Controllers
{
    public class ProductController : Controller
    {
 
         private readonly IRepositoryManager Repostitory;

        public ProductController(IRepositoryManager Repostitory_)
        {
            Repostitory = Repostitory_;
        }
        public IActionResult Index()
        {
            try
            {
                 IEnumerable<Product> LstProd = Repostitory.Products.GetAllObject();
                 return View(LstProd);
            }
            catch (System.Exception)
            {

                 
            }
            return NotFound();
        }
        int FlagNam = 0;
        public IActionResult Order_Name()
        {
            if (FlagNam == 0)
            {
                return View(nameof(Index), Repostitory.Products.GetAllObject().OrderBy(x => x.ProdName));
                FlagNam = 1;
            }
            else
            {
                return View(nameof(Index), Repostitory.Products.GetAllObject().OrderByDescending(x => x.ProdName));
                FlagNam = 0;

            }
        }
         int  FlagQuant = 0;

        public IActionResult OrderQuantity()
        {
             if (FlagQuant == 0)
            {
                return View(nameof(Index), Repostitory.Products.GetAllObject().OrderBy(x => x.Quantity));
                FlagQuant = 1;
            }
            else
            {
                return View(nameof(Index), Repostitory.Products.GetAllObject().OrderByDescending(x => x.Quantity));
                FlagQuant = 0;

            }
        }
        
         public IActionResult Details(int ? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return View(Repostitory.Products.GetObject( int.Parse(id.ToString())));
        }

          void FillViewBage()
        {
             ViewBag.Catageory = new SelectList(Repostitory.Categories.GetAllObject(), "CatagNo", "CatagName") as SelectList;
         }

         [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product Prod, List<IFormFile> files)
        {
 
            if (ModelState.IsValid)
            {
                  Repostitory.Products.AddObject(Prod);
                await  Repostitory.SaveChangesAsyn();
                saveFile(Prod, files);

                return RedirectToAction(nameof(Index),Repostitory.Products.GetAllObject());
            }
            return View(Prod);
        }

        public IActionResult Create( )
        {
            FillViewBage();
           return View( ); 
         }

       async  void saveFile(Product prod, List<IFormFile> FormFile)
        {

            try
            {
                 string Extenten="";
                if (FormFile != null)
                {
                    prod.ProdMainImg = $"{0}{prod.ProdId}{Path.GetExtension(FormFile[0].FileName)}";
                    await Repostitory.SaveChangesAsyn();

                    Extenten = Path.GetExtension(FormFile[0].FileName);
                    prod.ProdMainImg = $"{0}{prod.ProdId}{Extenten}";

                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", $"{0}{prod.ProdId}{Extenten}");

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await FormFile[0].CopyToAsync(stream);
                    }

                }
            }
            catch (System.Exception excep)
            {

                var mess = excep.Message;
            }
            finally
            {
 
            }

        }

      /* void DeletProductPic(Product Prod)
        {
           
            if (Prod.Photo != string.Empty)
            {
                try
                {
                    string Root_Path = Directory.GetCurrentDirectory();
                    string FullPath = Path.Combine(Root_Path, "Resources", Prod);

                    System.IO.File.Delete(FullPath);
                }
                catch (Exception ex)
                {

                    Log.LogError($"Function is : Delete Picture  Error is  {ex.Message}");
                }

            }
        }*/

        // GET: Students/Edit/5
        public IActionResult Edit(int? id)
        {
             if (id == null)
            {
                return NotFound();
            }
            FillViewBage();
         Product Prod= Repostitory.Products.GetObject(int.Parse(id.ToString()));
            return View(Prod);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Product prod, IFormFile file)
        {

            if (ModelState.IsValid)
            {
                Repostitory.Products.EditProduct(prod);
              await  Repostitory.SaveChangesAsyn();
                return RedirectToAction(nameof(Index),Repostitory.Products.GetAllObject());
            }
            FillViewBage();
            return View(prod);
         }

        // GET: Students/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
             return View(Repostitory.Products.GetObject(int.Parse(id.ToString())));
        }

         [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> DeleteConfirmed(int id)
        {
            Product prod = Repostitory.Products.GetObject(int.Parse(id.ToString()));
            Repostitory.Products.DeleteProduct(prod);
            await Repostitory.SaveChangesAsyn();
            return RedirectToAction(nameof(Index), Repostitory.Products.GetAllObject());
        }
        
        
    }
}
