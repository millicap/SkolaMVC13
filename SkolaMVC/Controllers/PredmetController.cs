using SkolaMVC.DBModels;
using SkolaMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SkolaMVC.Controllers
{
    public class PredmetController : Controller
    {
        // GET: Predmet
        public ActionResult Index()
        {
            using (var context = new SkolaContext())
            {
                var predmetList = context.Predmets.Select(p => new PredmetViewModel()
                {
                    PredmetId = p.PredmetId,
                    Naziv = p.Naziv

                }).ToList();

                return View(predmetList);
            }              
        }

        public ActionResult Delete(string id)
        {
            int predmetId = Convert.ToInt32(id);

            using (var context = new SkolaContext())
            {
                Predmet predmet = context.Predmets.Where(p => p.PredmetId == predmetId).FirstOrDefault();
                PredmetViewModel predmetVM = new PredmetViewModel()
                {
                    PredmetId= predmet.PredmetId,
                    Naziv = predmet.Naziv

                };
                return View(predmetVM);
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (var context = new SkolaContext())
            {
                if (context.Predmets.Find(id).Ocjenas.Count == 0)
                {
                    context.Predmets.Remove(context.Predmets.Find(id));
                    context.SaveChanges();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Greska");
                    Predmet predm = context.Predmets.Find(id);
                    PredmetViewModel predmetVM = new PredmetViewModel()
                    {
                        PredmetId = predm.PredmetId,        
                        Naziv = predm.Naziv                     
                    };
                    return View(predmetVM);
                }              
            }
            return RedirectToAction("Index");          
        }
    }
}