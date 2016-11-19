using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Demo6.Classes;
using Demos6.DataModel;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class NinjasController : Controller
    {
        private MvcRepository db = new MvcRepository("NinjaMvc");

        // GET: Ninjas
        public ActionResult Index()
        {
            var ninjas = db.GetNinjasWithClan();
            return View(ninjas.ToList());
        }

        // GET: Ninjas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ninja ninja = db.GetNinjaWithEquipmentAndClan(id.Value);
            if (ninja == null)
            {
                return HttpNotFound();
            }
            return View(ninja);
        }

        //// GET: Ninjas/Create
        //public ActionResult Create()
        //{
        //    ViewBag.ClanId = new SelectList(db.Clans, "Id", "ClanName");
        //    return View();
        //}

        //// POST: Ninjas/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,Name,ServedInOniwaban,ClanId,DateOfBirth,DateModified,DateCreated")] Ninja ninja)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Ninjas.Add(ninja);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.ClanId = new SelectList(db.Clans, "Id", "ClanName", ninja.ClanId);
        //    return View(ninja);
        //}

        // GET: Ninjas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ninja ninja = db.GetNinjaWithEquipment(id.Value);
            if (ninja == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClanId = new SelectList(db.GetClans(), "Id", "ClanName", ninja.ClanId);
            return View(ninja);
        }

        [HttpPost]
        public ActionResult Edit(Ninja ninja)
        {
            if (ModelState.IsValid == true)
            {
                db.EditNinja(ninja);
                //return RedirectToAction("Details", "Ninjas", new { id = ninja.Id });

                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ClanId = new SelectList(db.GetClans(), "Id", "ClanName", ninja.ClanId);
                return View(ninja);
            }
        }

        public ActionResult ViewEquipmentList(int id)
        {
            var list = db.GetNinjaEquipmentList(id);

            return PartialView(list.ToList());
        }






    }
}
