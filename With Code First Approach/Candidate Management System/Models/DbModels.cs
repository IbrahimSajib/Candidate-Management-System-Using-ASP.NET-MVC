using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel;

namespace Candidate_Management_System.Models
{
    
    public class Candidate
    {
        public Candidate()
        {
            this.CandidateSkills = new List<CandidateSkill>();
        }
        public int CandidateId { get; set; }
        [Required, StringLength(50), DisplayName("Candidate Name")]
        public string CandidateName { get; set; }
        [Required, Column(TypeName = "date"), DisplayFormat(DataFormatString = "{0:yyyy-MMM-dd}", ApplyFormatInEditMode = true), DisplayName("Date Of Birth")]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string Phone { get; set; }
        public string Image { get; set; }
        [Required,DisplayName("Fresher?")]
        public bool Fresher { get; set; }
        public virtual ICollection<CandidateSkill> CandidateSkills { get; set; }
    }

    public class Skill
    {
        public Skill()
        {
            this.CandidateSkills = new List<CandidateSkill>();
        }
        public int SkillId { get; set; }
        [Required,DisplayName("Skill Name"),StringLength(30)]
        public string SkillName { get; set; }
        //nev
        public virtual ICollection<CandidateSkill> CandidateSkills { get; set; }
    }
    public class CandidateSkill
    {
        public int CandidateSkillId { get; set; }
        [ForeignKey("Candidate")]
        public int CandidateId { get; set; }
        [ForeignKey("Skill")]
        public int SkillId { get; set; }
        //nev
        public virtual Candidate Candidate { get; set; }
        public virtual Skill Skill { get; set; }       
    }
    public class MyDbContext : DbContext
    {
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<CandidateSkill> CandidateSkills { get; set; }
    }
}