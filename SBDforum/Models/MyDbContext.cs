using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace SBDforum.Models
{
    public class MyDbContext : DbContext
    {
        public MyDbContext() : base("SBDforumDB") { }


        public DbSet<Category> Categories { get; set; }
        public DbSet<Thread>   Threads { get; set; }
        public DbSet<Answer>   Answers { get; set; }
        public DbSet<Comment>  Comments { get; set; }

        public DbSet<User>     Users { get; set; }
        public DbSet<Message>  Messages { get; set; }

        public DbSet<Rule> Rules { get; set; }
        public DbSet<IntervalPoint> IntervalPoints { get; set; }
        public DbSet<BannedWordsDictionary> BannedWordsDictionaries { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();


            modelBuilder.Entity<Message>()
                        .HasRequired(b => b.Sender)
                        .WithMany(a => a.MessagesSent)
                        .HasForeignKey(b => b.SenderID);

            modelBuilder.Entity<Message>()
                        .HasRequired(b => b.Receiver)
                        .WithMany(a => a.MessagesReceived)
                        .HasForeignKey(b => b.ReceiverID);
        }
    }
}