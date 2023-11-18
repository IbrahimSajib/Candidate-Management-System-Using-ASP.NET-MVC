using Candidate_Management_System.Models;
using Candidate_Management_System.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Candidate_Management_System.Controllers
{
    public class CandidatesController : Controller
    {
        private readonly MyDbContext db = new MyDbContext();

        // GET: Candidates
        public ActionResult Index(int page = 1)
        {
            var candidates = db.Candidates.Include(c => c.CandidateSkills.Select(cs => cs.Skill)).OrderBy(c => c.CandidateId).ToList();
            ViewBag.totalPages = (int)Math.Ceiling((double)db.Candidates.Count() / 5);
            ViewBag.currentPage = page;
            return View(db.Candidates.Include(c => c.CandidateSkills.Select(cs => cs.Skill)).OrderBy(x => x.CandidateId).Skip((page - 1) * 5).Take(5).ToList());
        }

        // GET: Candidates/Details/{id}
        public ActionResult Details(int? id)
        {
            Candidate candidate = db.Candidates.Single(x => x.CandidateId == id);
            return View(candidate);
        }


        public ActionResult AddNewSkill(int? id)
        {
            ViewBag.Skills = new SelectList(db.Skills.ToList(), "SkillId", "SkillName", (id != null) ? id.ToString() : "");
            return PartialView("_addNewSkill");
        }

        // GET: Candidates/Create
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CandidateVM candidateVM, int[] skillId)
        {
            if (ModelState.IsValid)
            {
                Candidate candidate = new Candidate()
                {
                    CandidateName = candidateVM.CandidateName,
                    DateOfBirth = candidateVM.DateOfBirth,
                    Phone = candidateVM.Phone,
                    Fresher = candidateVM.Fresher
                };

                HttpPostedFileBase file = candidateVM.ImageFile;
                if (file != null)
                {
                    string filePath = Path.Combine("/Images/", DateTime.Now.Ticks + Path.GetExtension(file.FileName));
                    file.SaveAs(Server.MapPath(filePath));
                    candidate.Image = filePath;
                }

                //Save all skill from skillId

                foreach (var item in skillId)
                {
                    CandidateSkill candidateSkill = new CandidateSkill()
                    {
                        Candidate = candidate,
                        CandidateId = candidate.CandidateId,
                        SkillId = item
                    };
                    db.CandidateSkills.Add(candidateSkill);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: Candidates/Edit/{id}
        public ActionResult Edit(int? id)
        {
            Candidate candidate = db.Candidates.First(x => x.CandidateId == id);
            var candidateSkill = db.CandidateSkills.Where(x => x.CandidateId == id).ToList();
            CandidateVM candidateVM = new CandidateVM()
            {
                CandidateId = candidate.CandidateId,
                CandidateName = candidate.CandidateName,
                DateOfBirth = candidate.DateOfBirth,
                Phone = candidate.Phone,
                Image = candidate.Image,
                Fresher = candidate.Fresher,
            };
            if (candidateSkill.Count() > 0)
            {
                foreach (var item in candidateSkill)
                {
                    candidateVM.SkillList.Add(item.SkillId);
                }
            }
            return View(candidateVM);
        }

        // POST: Candidates/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CandidateVM candidateVM, int[] skillId)
        {
            if (ModelState.IsValid)
            {
                Candidate candidate = new Candidate()
                {
                    CandidateId = candidateVM.CandidateId,
                    CandidateName = candidateVM.CandidateName,
                    DateOfBirth = candidateVM.DateOfBirth,
                    Phone = candidateVM.Phone,
                    Image = candidateVM.Image,
                    Fresher = candidateVM.Fresher,
                };

                HttpPostedFileBase file = candidateVM.ImageFile;
                if (file != null)
                {
                    string filePath = Path.Combine("/Images/", DateTime.Now.Ticks + Path.GetExtension(file.FileName));
                    file.SaveAs(Server.MapPath(filePath));
                    candidate.Image = filePath;
                }

                var existsSkillEntry = db.CandidateSkills.Where(x => x.CandidateId == candidate.CandidateId).ToList();
                //For delete spot
                foreach (var item in existsSkillEntry)
                {
                    db.CandidateSkills.Remove(item);
                }
                //Add Spot
                foreach (var item in skillId)
                {
                    CandidateSkill candidateSkill = new CandidateSkill()
                    {
                        CandidateId = candidate.CandidateId,
                        SkillId = item
                    };
                    db.CandidateSkills.Add(candidateSkill);
                }
                db.Entry(candidate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: Candidates/Delete/{id}
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Candidate candidate = db.Candidates.Find(id);
            if (candidate == null)
            {
                return HttpNotFound();
            }
            return View(candidate);
        }

        // POST: Candidates/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Candidate candidate = db.Candidates.Find(id);
            db.Candidates.Remove(candidate);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}