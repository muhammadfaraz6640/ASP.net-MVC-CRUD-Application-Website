using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace firstMVCapp.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        private Models.MovieDb m = new Models.MovieDb();
        public ActionResult Index()
        {
            return View(m.Movies.ToList());
        }

        // GET: Default/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Default/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Default/Create
        [HttpPost]
        public ActionResult Create([Bind(Exclude ="id")] Models.Movie newmovie )
        {
            if (!ModelState.IsValid)
                return View();
            m.Movies.Add(newmovie);
            m.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Default/Edit/5
        public ActionResult Edit(int id)
        {
            var movie_to_edit = (from m in m.Movies where m.Id == id select m).First();
            return View(movie_to_edit);
        }

        // POST: Default/Edit/5
        [HttpPost]
        public ActionResult Edit(Models.Movie movie_to_edit)
        {
            try
            {
                // TODO: Add update logic here
                var new_movie = (from m in m.Movies where m.Id == movie_to_edit.Id select m).First();
                if (!ModelState.IsValid)
                    return View(new_movie);
                m.Entry(new_movie).CurrentValues.SetValues(movie_to_edit);               
                m.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Default/Delete/5
        public ActionResult Delete(int id)
        {
            var del = m.Movies.Where(x => x.Id == id).First();
            return View(del);
        }

        // POST: Default/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            Models.Movie mm = m.Movies.Where(x => x.Id == id).First();
            m.Movies.Remove(mm);
            m.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
