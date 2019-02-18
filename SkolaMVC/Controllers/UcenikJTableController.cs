using SkolaMVC.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;

namespace SkolaMVC.Controllers
{
    public class UcenikJTableController : Controller
    {
        // GET: UcenikJTable
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
                    var ucenici = context.Uceniks.Select(u => new
                    {
                        u.UcenikId,
                        u.Ime,
                        u.Prezime,
                        u.Pol,
                        u.JMBG,
                        u.Adresa,
                        u.DatumRodjenja,
                        u.ImeRoditelja,
                        u.BrojUDnevniku,
                        u.Drzavljanstvo,
                        u.OdjeljenjeId,
                        u.GradId
                    }).ToList();

                    var count = ucenici.Count();
                    var records = ucenici.OrderBy(jtSorting).Skip(jtStartIndex).Take(jtPageSize).ToList();

                    //Return result to jTable
                    return Json(new { Result = "OK", Records = records, TotalRecordCount = count });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
    }
}