//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Candidate_Management_System.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CandidateSkill
    {
        public int CandidateSkillId { get; set; }
        public int CandidateId { get; set; }
        public int SkillId { get; set; }
    
        public virtual Candidate Candidate { get; set; }
        public virtual Skill Skill { get; set; }
    }
}
