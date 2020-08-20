using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApplication1.ActionResults;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ForStudent]

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;

        }
        public IActionResult Index()
        {
            //return new BeautyResult {InnerHtml = "Hello"};

            var claim = User.Claims.FirstOrDefault(c => c.Type == "studentId");
            if (claim == null)
            {
                return View();
            }

            return RedirectToAction("select", new { id = Convert.ToInt16(claim.Value) });

        }

        public IActionResult Select(int? id)
        {
            var stud = _context.Students.Include("StudDiscs").SingleOrDefault(s => s.Id == id);
            var selectedDiscsIds = stud.StudDiscs.Select(p => p.DisciplineId);
            var discs = _context.Disciplines;
            var model = new SelectModel {Student = stud};
            model.SelectedDiscs = discs.Where(d => selectedDiscsIds.Contains(d.Id)).OrderBy(d => d.Title);
            model.NotSelectedDiscs = discs.Except(model.SelectedDiscs).OrderBy(d => d.Title);

            return View(model);
        }

        [HttpPost]
        public IActionResult Select(int studId, int[] selectedDiscsIds)
        {
            var student = _context.Students.Include("StudDiscs").SingleOrDefault(s => s.Id == studId);
            student.StudDiscs = new List<StudDisc>();
            foreach (var id in selectedDiscsIds)
            {
                student.StudDiscs.Add(new StudDisc { StudentId = student.Id, DisciplineId = id });
            }
            _context.SaveChanges();

            return RedirectToAction("Select");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
