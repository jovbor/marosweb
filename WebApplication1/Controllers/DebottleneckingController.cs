using Debottlenecking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;


namespace WebApplication1.Controllers
{
    public class DebottleneckingController : Controller
    {
        // GET: Debottlenecking
        public ActionResult Index()
        {
            var factorisedTable = Acummulator.DefferedProduction();
            ViewBag.factorisedTable = factorisedTable;
            ViewBag.nYear = factorisedTable.Count;
            ViewBag.nWells = factorisedTable[0].WellRates.Count;

            var flowTable = GetFlowTable.GetTable();
            ViewBag.flowTable = flowTable;

            var Nodes = GetNodeInfo.NodeInfo();
            ViewBag.NodeInfo = Nodes;
            ViewBag.nNodes = Nodes.Count;


            return View(factorisedTable.ToList());

        }


        // GET: Debottlenecking/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Debottlenecking/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Debottlenecking/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Debottlenecking/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Debottlenecking/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Debottlenecking/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Debottlenecking/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
