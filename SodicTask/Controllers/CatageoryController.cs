using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SodicTask.Interface;
using SodicTask.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace SodicTask.Controllers
{
    public class CatageoryController : Controller
    {
        // GET: CatageoryController1
         private static string MSG= "Catageory ";
        private readonly IRepositoryManager Repostitory;

        public CatageoryController(IRepositoryManager _Repostitory)
        {
            Repostitory = _Repostitory;
        }
 
        public ActionResult Index()
        {
            ViewBag.alert = MSG;
            FillViewBag();
            return View(Repostitory.Categories.GetAllObject());
        }
         
        void FillViewBag()
        {
          IDictionary<int, string> Colors = new Dictionary<int, string>();
            Colors.Add(1, "Red");
            Colors.Add(2, "Yellow");
            Colors.Add(3, "Teal");
            Colors.Add(4, "Green");
            SelectList ColorsList = new SelectList(Colors, "Key", "Value");
            ViewBag.Parent = "MainCatageory";
            ViewBag.Colors = ColorsList;
        }
        //Ajax
        [HttpPost]
        public ActionResult FormCatagPartial(int id)
        {
            FillViewBag();
            ViewBag.Parent = "MainCatageory";
            //Show Main
            if (id == 0)
            {
                return View(new Category() { ParentNo = 0, LevelNo = 0});
 
            }

            Category Obj = Repostitory.Categories.GetObject(id);
            Category Parent = Repostitory.Categories.GetObject( int.Parse(Obj.ParentNo.ToString()));
            if (Parent == null)
            {
                 Obj.ParentNo = 0;
                Obj.LevelNo =0;

            }
            else 
            { 
                ViewBag.Parent = Parent.CatagName;
                 Obj.LevelNo = 1;
            }
            return View(Obj);
        }

        [HttpPost]
        public ActionResult FormCatagPartialNew(int id)
        {
            try
            {
                FillViewBag();

                Category Parnt = Repostitory.Categories.GetObject(id);
                Category NewChld = new Category()
                {
                    ParentNo = id,
                    LevelNo = (short)(Parnt.LevelNo + 1)
                };
                ViewBag.Parent = Parnt.CatagName;
                return View("FormCatagPartial", NewChld);

            }
            catch (Exception ex)
            {
             }
            return View("FormCatagPartial", new Category());


        }

        // GET: CatageoryController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateUpdate(Category Obj)
        {
            //Add New
            try
            {
                if (Obj.CatagNo == 0)
                {
                    if (Obj.ParentNo != 0)
                    {
                        Category Par = Repostitory.Categories.GetObject(int.Parse(Obj.ParentNo.ToString()));
                        Obj.LevelNo = (short)(Par.LevelNo + 1);
                    }
                    Repostitory.Categories.AddObject(Obj);
                }

                //Update 
                else
                {
                    Repostitory.Categories.EditCatag(Obj);

                }
              await  Repostitory.SaveChangesAsyn();
            }
            catch (Exception ex)
            {

             }
            return RedirectToAction(nameof(Index),Repostitory.Categories.GetAllObject());
        }
 
         [HttpPost]
         public async Task< ActionResult> Delete(int id)
        {
            try
            {
                Category Obj = Repostitory.Categories.GetObject(id);
                IEnumerable< Category > lstChild= Repostitory.Categories.GetAllObjectSearchAsync(id);
                if(lstChild.Count()==0)
                {
                    Repostitory.Categories.DeleteCatag(Obj);
                   await Repostitory.SaveChangesAsyn();
                     return Content("True");
                }
            }
            catch (Exception except)
            {
                 
            }
            return Content("false");
         }

  
    }
}
