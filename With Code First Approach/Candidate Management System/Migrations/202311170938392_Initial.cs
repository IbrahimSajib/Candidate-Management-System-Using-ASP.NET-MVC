namespace Candidate_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Candidates",
                c => new
                    {
                        CandidateId = c.Int(nullable: false, identity: true),
                        CandidateName = c.String(nullable: false, maxLength: 50),
                        DateOfBirth = c.DateTime(nullable: false, storeType: "date"),
                        Phone = c.String(nullable: false),
                        Image = c.String(),
                        Fresher = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CandidateId);
            
            CreateTable(
                "dbo.CandidateSkills",
                c => new
                    {
                        CandidateSkillId = c.Int(nullable: false, identity: true),
                        CandidateId = c.Int(nullable: false),
                        SkillId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CandidateSkillId)
                .ForeignKey("dbo.Candidates", t => t.CandidateId, cascadeDelete: true)
                .ForeignKey("dbo.Skills", t => t.SkillId, cascadeDelete: true)
                .Index(t => t.CandidateId)
                .Index(t => t.SkillId);
            
            CreateTable(
                "dbo.Skills",
                c => new
                    {
                        SkillId = c.Int(nullable: false, identity: true),
                        SkillName = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.SkillId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CandidateSkills", "SkillId", "dbo.Skills");
            DropForeignKey("dbo.CandidateSkills", "CandidateId", "dbo.Candidates");
            DropIndex("dbo.CandidateSkills", new[] { "SkillId" });
            DropIndex("dbo.CandidateSkills", new[] { "CandidateId" });
            DropTable("dbo.Skills");
            DropTable("dbo.CandidateSkills");
            DropTable("dbo.Candidates");
        }
    }
}
