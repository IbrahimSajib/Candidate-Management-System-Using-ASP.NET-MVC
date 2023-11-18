using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace Candidate_Management_System.ViewModels
{
    public class CandidateVM
    {
        public CandidateVM()
        {
            this.SkillList = new List<int>();
        }
        public int CandidateId { get; set; }
        [Required, StringLength(50), DisplayName("Candidate Name")]
        public string CandidateName { get; set; }
        [Required, Column(TypeName = "date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true), DisplayName("Date Of Birth")]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string Phone { get; set; }
        public string Image { get; set; }
        [DisplayName("Image")]
        public HttpPostedFileBase ImageFile { get; set; }
        [Required, DisplayName("Fresher?")]
        public bool Fresher { get; set; }
        public List<int> SkillList { get; set; }
    }
}