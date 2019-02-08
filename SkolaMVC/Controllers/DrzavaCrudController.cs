using SkolaMVC.DBModels;
using SkolaMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;

namespace SkolaMVC.Controllers
{
    public class DrzavaCrudController : Controller
    {
        // GET: DrzavaCrud
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult List(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                using (var context = new SkolaContext())
                {
                    var drzave = context.Drzavas.Select(d => new
                    {
                       d.DrzavaId,
                         d.Naziv
                    }).ToList();

                    var count = drzave.Count();
                    var records = drzave.OrderBy(jtSorting).Skip(jtStartIndex).Take(jtPageSize).ToList();



                    //Return result to jTable
                    return Json(new { Result = "OK", Records = records, TotalRecordCount = count });
                }

                 
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }



        [HttpPost]
        public JsonResult Create(Drzava drzava)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { Result = "ERROR", Message = "Form is not valid! Please correct it and try again." });
                }

                using (var context = new SkolaContext())
                {
                    context.Drzavas.Add(drzava);
                    context.SaveChanges();
                    return Json(new { Result = "OK", Record = drzava });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult Update(Drzava drzava)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { Result = "ERROR", Message = "Form is not valid! Please correct it and try again." });
                }

                using (var context = new SkolaContext())
                {
                    Drzava drzavaUpdate = context.Drzavas.Find(drzava.DrzavaId);

                    drzavaUpdate.DrzavaId = drzava.DrzavaId;
                    drzavaUpdate.Naziv = drzava.Naziv;

                    context.SaveChanges();
                }
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult Delete(int drzavaId)
        {
            try
            {
                using (var context = new SkolaContext())
                {

                    context.Drzavas.Remove(context.Drzavas.Find(drzavaId));
                    context.SaveChanges();
                }
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
    }
}