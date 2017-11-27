namespace SBDforum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class w1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        AnswerID = c.Int(nullable: false, identity: true),
                        ThreadID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Points = c.Int(nullable: false),
                        Text = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.AnswerID)
                .ForeignKey("dbo.Users", t => t.UserID)
                .ForeignKey("dbo.Threads", t => t.ThreadID)
                .Index(t => t.ThreadID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentID = c.Int(nullable: false, identity: true),
                        AnswerID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Text = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CommentID)
                .ForeignKey("dbo.Answers", t => t.AnswerID)
                .ForeignKey("dbo.Users", t => t.UserID)
                .Index(t => t.AnswerID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        Nick = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Role = c.Boolean(nullable: false),
                        Avatar = c.String(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserID);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        MessageID = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        SenderID = c.Int(nullable: false),
                        ReceiverID = c.Int(nullable: false),
                        Text = c.String(nullable: false),
                        Read = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.MessageID)
                .ForeignKey("dbo.Users", t => t.ReceiverID)
                .ForeignKey("dbo.Users", t => t.SenderID)
                .Index(t => t.SenderID)
                .Index(t => t.ReceiverID);
            
            CreateTable(
                "dbo.Threads",
                c => new
                    {
                        ThreadID = c.Int(nullable: false, identity: true),
                        CategoryID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        Title = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ThreadID)
                .ForeignKey("dbo.Categories", t => t.CategoryID)
                .ForeignKey("dbo.Users", t => t.UserID)
                .Index(t => t.CategoryID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryID);
            
            CreateTable(
                "dbo.BannedWordsDictionaries",
                c => new
                    {
                        BannedWordsDictionaryID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.BannedWordsDictionaryID);
            
            CreateTable(
                "dbo.IntervalPoints",
                c => new
                    {
                        IntervalPointID = c.Int(nullable: false, identity: true),
                        LowerRange = c.Int(nullable: false),
                        UpperRange = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.IntervalPointID);
            
            CreateTable(
                "dbo.Rules",
                c => new
                    {
                        RuleID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Text = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.RuleID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Threads", "UserID", "dbo.Users");
            DropForeignKey("dbo.Threads", "CategoryID", "dbo.Categories");
            DropForeignKey("dbo.Answers", "ThreadID", "dbo.Threads");
            DropForeignKey("dbo.Messages", "SenderID", "dbo.Users");
            DropForeignKey("dbo.Messages", "ReceiverID", "dbo.Users");
            DropForeignKey("dbo.Comments", "UserID", "dbo.Users");
            DropForeignKey("dbo.Answers", "UserID", "dbo.Users");
            DropForeignKey("dbo.Comments", "AnswerID", "dbo.Answers");
            DropIndex("dbo.Threads", new[] { "UserID" });
            DropIndex("dbo.Threads", new[] { "CategoryID" });
            DropIndex("dbo.Messages", new[] { "ReceiverID" });
            DropIndex("dbo.Messages", new[] { "SenderID" });
            DropIndex("dbo.Comments", new[] { "UserID" });
            DropIndex("dbo.Comments", new[] { "AnswerID" });
            DropIndex("dbo.Answers", new[] { "UserID" });
            DropIndex("dbo.Answers", new[] { "ThreadID" });
            DropTable("dbo.Rules");
            DropTable("dbo.IntervalPoints");
            DropTable("dbo.BannedWordsDictionaries");
            DropTable("dbo.Categories");
            DropTable("dbo.Threads");
            DropTable("dbo.Messages");
            DropTable("dbo.Users");
            DropTable("dbo.Comments");
            DropTable("dbo.Answers");
        }
    }
}
