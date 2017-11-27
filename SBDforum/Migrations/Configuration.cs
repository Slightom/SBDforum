namespace SBDforum.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SBDforum.Models.MyDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SBDforum.Models.MyDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Database.ExecuteSqlCommand("Delete from comments");
            context.Database.ExecuteSqlCommand("Delete from answers");
            context.Database.ExecuteSqlCommand("Delete from threads");
            context.Database.ExecuteSqlCommand("Delete from categories");
            context.Database.ExecuteSqlCommand("Delete from messages");
            context.Database.ExecuteSqlCommand("Delete from users");
            context.Database.ExecuteSqlCommand("Delete from rules");
            context.Database.ExecuteSqlCommand("Delete from bannedwordsdictionaries");
            context.Database.ExecuteSqlCommand("Delete from intervalpoints");

            context.SaveChanges();



            User u = new Models.User();
            // u.UserID = 1;
            u.Nick = "Witek15";
            u.Email = "witoldkocur@gmail.com";
            u.Password = "witek15p";
            u.Role = false;
            u.Avatar = "http://i.wp.pl/a/f/jpeg/20901/walesa_wp_04_550.jpeg";
            u.Active = true;
            context.Users.Add(u);
            context.SaveChanges();


            u = new Models.User();
            //  u.UserID = 2;
            u.Nick = "Jola17";
            u.Email = "jolkajolkapamietasz@gmail.com";
            u.Password = "jola17p";
            u.Role = false;
            u.Avatar = "http://images.hellokids.com/_uploads/_tiny_galerie/20131043/vign-pikachu-by5_bb9.jpg";
            u.Active = true;
            context.Users.Add(u);
            context.SaveChanges();

            u = new Models.User();
            //  u.UserID = 3;
            u.Nick = "drobiowy07";
            u.Email = "sosdrobiowy@gmail.com";
            u.Password = "drobiowy07p";
            u.Role = false;
            u.Avatar = "http://megaicons.net/static/img/icons_sizes/84/445/256/blue-user-icon.png";
            u.Active = true;
            context.Users.Add(u);
            context.SaveChanges();

            u = new Models.User();
            // u.UserID = 4;
            u.Nick = "wiktor500";
            u.Email = "wiktorekpotworek@gmail.com";
            u.Password = "wiktor500pp";
            u.Role = false;
            u.Avatar = "http://www.rmf.fm/_files/Short_foto/625/0e4fb6e75a40f3ac328f8fe2e841898e.jpg";
            u.Active = true;
            context.Users.Add(u);
            context.SaveChanges();

            u = new Models.User();
            // u.UserID = 5;
            u.Nick = "marysia94";
            u.Email = "mamarysia@gmail.com";
            u.Password = "marysia94p";
            u.Role = false;
            u.Avatar = "http://www.tapetus.pl/obrazki/n/111055_kotek-apka-klopot.jpg";
            u.Active = true;
            context.Users.Add(u);
            context.SaveChanges();

            u = new Models.User();
            //  u.UserID = 6;
            u.Nick = "admin";
            u.Email = "admin@gmail.com";
            u.Password = "admin";
            u.Role = true;
            u.Avatar = "http://megaicons.net/static/img/icons_sizes/84/445/256/blue-user-icon.png";
            u.Active = true;
            context.Users.Add(u);
            context.SaveChanges();

            u = new Models.User();
            //  u.UserID = 7;
            u.Nick = "u";
            u.Email = "u";
            u.Password = "u";
            u.Role = false;
            u.Avatar = "http://megaicons.net/static/img/icons_sizes/84/445/256/blue-user-icon.png";
            u.Active = true;
            context.Users.Add(u);
            context.SaveChanges();





            BannedWordsDictionary b = new BannedWordsDictionary();
            b.Name = "krucafuks";
            context.BannedWordsDictionaries.AddOrUpdate(b);
            context.SaveChanges();

            b = new BannedWordsDictionary();
            b.Name = "psia koœæ".ToString();
            context.BannedWordsDictionaries.AddOrUpdate(b);
            context.SaveChanges();

            b = new BannedWordsDictionary();
            b.Name = "kurka wodna".ToString();
            context.BannedWordsDictionaries.AddOrUpdate(b);
            context.SaveChanges();

            b = new BannedWordsDictionary();
            b.Name = "pata³ach".ToString();
            context.BannedWordsDictionaries.AddOrUpdate(b);
            context.SaveChanges();

            b = new BannedWordsDictionary();
            b.Name = "motyla noga".ToString();
            context.BannedWordsDictionaries.AddOrUpdate(b);
            context.SaveChanges();

            b = new BannedWordsDictionary();
            b.Name = "cholercia".ToString();
            context.BannedWordsDictionaries.AddOrUpdate(b);
            context.SaveChanges();





            IntervalPoint i = new IntervalPoint();
            i.LowerRange = 0;
            i.UpperRange = 5;
            i.Name = "nowicjusz".ToString();
            context.IntervalPoints.AddOrUpdate(i);
            context.SaveChanges();

            i = new IntervalPoint();
            i.LowerRange = 6;
            i.UpperRange = 500;
            i.Name = "pocz¹tkuj¹cy".ToString();
            context.IntervalPoints.AddOrUpdate(i);
            context.SaveChanges();

            i = new IntervalPoint();
            i.LowerRange = 501;
            i.UpperRange = 10000;
            i.Name = "œredniozaawansowany".ToString();
            context.IntervalPoints.AddOrUpdate(i);
            context.SaveChanges();

            i = new IntervalPoint();
            i.LowerRange = 10001;
            i.UpperRange = 100000;
            i.Name = "zaawansowany";
            context.IntervalPoints.AddOrUpdate(i);
            context.SaveChanges();

            i = new IntervalPoint();
            i.LowerRange = 100001;
            i.UpperRange = 2147483647;
            i.Name = "super nerd";
            context.IntervalPoints.AddOrUpdate(i);
            context.SaveChanges();







            Rule r = new Rule();
            r.Name = "Nie bluzgaj";
            r.Text = "Nie u¿ywaj brzydkich s³ów";
            context.Rules.AddOrUpdate(r);
            context.SaveChanges();

            r = new Rule();
            r.Name = "Pisz poprawnie";
            r.Text = "Nie pope³niaj ortografów";
            context.Rules.AddOrUpdate(r);
            context.SaveChanges();

            r = new Rule();
            r.Name = "Zbieraj punkty";
            r.Text = "Zbieraj¹c punkty zdobywasz wy¿szy level";
            context.Rules.AddOrUpdate(r);
            context.SaveChanges();

            r = new Rule();
            r.Name = "Myj rêce";
            r.Text = "Myj d³onie przed posi³kiem";
            context.Rules.AddOrUpdate(r);
            context.SaveChanges();

            r = new Rule();
            r.Name = "Szanuj rodziców";
            r.Text = "Zadzwoñ do mamy";
            context.Rules.AddOrUpdate(r);
            context.SaveChanges();






            int ids, idr;
            ids = context.Users.Where(n => n.Nick == "Witek15").Select(n => n.UserID).FirstOrDefault();
            idr = context.Users.Where(n => n.Nick == "Jola17").Select(n => n.UserID).FirstOrDefault();
            Message m = new Message();
            m.SenderID = ids;
            m.ReceiverID = idr;
            m.Text = "czeœæ";
            m.Date = new DateTime(2016, 11, 20, 10, 10, 10);
            m.Read = true;
            context.Messages.AddOrUpdate(m);
            context.SaveChanges();


            ids = context.Users.Where(n => n.Nick == "Jola17").Select(n => n.UserID).FirstOrDefault();
            idr = context.Users.Where(n => n.Nick == "Witek15").Select(n => n.UserID).FirstOrDefault();
            m = new Message();
            m.SenderID = ids;
            m.ReceiverID = idr;
            m.Text = "no czeœæ";
            m.Read = true;
            m.Date = new DateTime(2016, 11, 20, 10, 10, 15);
            context.Messages.AddOrUpdate(m);
            context.SaveChanges();


            ids = context.Users.Where(n => n.Nick == "Witek15").Select(n => n.UserID).FirstOrDefault();
            idr = context.Users.Where(n => n.Nick == "Jola17").Select(n => n.UserID).FirstOrDefault();
            m = new Message();
            m.SenderID = ids;
            m.ReceiverID = idr;
            m.Text = "idziemy na piwo?";
            m.Read = true;
            m.Date = new DateTime(2016, 11, 20, 10, 11, 5);
            context.Messages.AddOrUpdate(m);
            context.SaveChanges();

            ids = context.Users.Where(n => n.Nick == "Jola17").Select(n => n.UserID).FirstOrDefault();
            idr = context.Users.Where(n => n.Nick == "Witek15").Select(n => n.UserID).FirstOrDefault();
            m = new Message();
            m.SenderID = ids;
            m.ReceiverID = idr;
            m.Text = "za 15 minut na murkach";
            m.Read = true;
            m.Date = new DateTime(2016, 11, 20, 10, 11, 17);
            context.Messages.AddOrUpdate(m);
            context.SaveChanges();


            ids = context.Users.Where(n => n.Nick == "Witek15").Select(n => n.UserID).FirstOrDefault();
            idr = context.Users.Where(n => n.Nick == "Jola17").Select(n => n.UserID).FirstOrDefault();
            m = new Message();
            m.SenderID = ids;
            m.ReceiverID = idr;
            m.Text = "elo";
            m.Read = true;
            m.Date = new DateTime(2016, 11, 20, 10, 11, 29);
            context.Messages.AddOrUpdate(m);
            context.SaveChanges();






            Category c = new Category();
            // c.CategoryID = 1;
            c.Name = "Linux";
            c.Description = "W¹tki o Linuxie";
            context.Categories.AddOrUpdate(c);
            context.SaveChanges();

            c = new Category();
            // c.CategoryID = 2;
            c.Name = "Monitory";
            c.Description = "W¹tki o Monitorach";
            context.Categories.AddOrUpdate(c);
            context.SaveChanges();

            c = new Category();
            // c.CategoryID = 3;
            c.Name = "Myszki";
            c.Description = "W¹tki o Myszach";
            context.Categories.AddOrUpdate(c);
            context.SaveChanges();

            c = new Category();
            // c.CategoryID = 4;
            c.Name = "Laptopy";
            c.Description = "W¹tki o Laptopach";
            context.Categories.AddOrUpdate(c);
            context.SaveChanges();

            c = new Category();
            //c.CategoryID = 5;
            c.Name = "MVC";
            c.Description = "W¹tki o MVC";
            context.Categories.AddOrUpdate(c);
            context.SaveChanges();

            c = new Category();
            // c.CategoryID = 6;
            c.Name = "Programowanie";
            c.Description = "W¹tki o Programowaniu";
            context.Categories.AddOrUpdate(c);
            context.SaveChanges();






            int ui, ci, ai, ti;
            ui = context.Users.Where(n => n.Nick == "Witek15").Select(n => n.UserID).FirstOrDefault();
            ci = context.Categories.Where(n => n.Name == "Linux").Select(n => n.CategoryID).FirstOrDefault();
            Thread t = new Thread();
            t.CategoryID = ci;
            t.UserID = ui;
            t.Title = "Co to jest grep?";
            t.Date = new DateTime(2016, 11, 20, 07, 50, 37);
            context.Threads.AddOrUpdate(t);
            context.SaveChanges();


            ui = context.Users.Where(n => n.Nick == "wiktor500").Select(n => n.UserID).FirstOrDefault();
            ci = context.Categories.Where(n => n.Name == "Myszki").Select(n => n.CategoryID).FirstOrDefault();
            t = new Thread();
            t.CategoryID = ci;
            t.UserID = ui;
            t.Title = "Jaka najlepsza myszka do grania?";
            t.Date = new DateTime(2016, 11, 20, 07, 52, 37);
            context.Threads.AddOrUpdate(t);
            context.SaveChanges();



            ui = context.Users.Where(n => n.Nick == "wiktor500").Select(n => n.UserID).FirstOrDefault();
            ci = context.Categories.Where(n => n.Name == "Myszki").Select(n => n.CategoryID).FirstOrDefault();
            t = new Thread();
            t.CategoryID = ci;
            t.UserID = ui;
            t.Title = "Myszka przewodowa czy bezprzewodowa?";
            t.Date = new DateTime(2016, 11, 20, 07, 54, 37);
            context.Threads.AddOrUpdate(t);
            context.SaveChanges();


            ui = context.Users.Where(n => n.Nick == "marysia94").Select(n => n.UserID).FirstOrDefault();
            ci = context.Categories.Where(n => n.Name == "MVC").Select(n => n.CategoryID).FirstOrDefault();
            t = new Thread();
            t.CategoryID = ci;
            t.UserID = ui;
            t.Title = "Areas in MVC?";
            t.Date = new DateTime(2016, 11, 20, 07, 56, 37);
            context.Threads.AddOrUpdate(t);
            context.SaveChanges();



            ui = context.Users.Where(n => n.Nick == "wiktor500").Select(n => n.UserID).FirstOrDefault();
            ci = context.Categories.Where(n => n.Name == "Laptopy").Select(n => n.CategoryID).FirstOrDefault();
            t = new Thread();
            t.CategoryID = ci;
            t.UserID = ui;
            t.Title = "Jaki laptop do gier do 3000z³?";
            t.Date = new DateTime(2016, 11, 20, 07, 58, 37);
            context.Threads.AddOrUpdate(t);
            context.SaveChanges();








            ui = context.Users.Where(n => n.Nick == "wiktor500").Select(n => n.UserID).FirstOrDefault();
            ti = context.Threads.Where(n => n.Title == "Co to jest grep?").Select(n => n.ThreadID).FirstOrDefault();
            Answer a = new Models.Answer();
            a.ThreadID = ti;
            a.UserID = ui;
            a.Text = "To wyszukiwanie";
            a.Points = 2;
            a.Date = new DateTime(2016, 11, 20, 08, 50, 37);
            context.Answers.AddOrUpdate(a);
            context.SaveChanges();


            ui = context.Users.Where(n => n.Nick == "Jola17").Select(n => n.UserID).FirstOrDefault();
            ti = context.Threads.Where(n => n.Title == "Myszka przewodowa czy bezprzewodowa?").Select(n => n.ThreadID).FirstOrDefault();
            a = new Models.Answer();
            a.ThreadID = ti;
            a.UserID = ui;
            a.Text = "Zdecydowanie bezprzewodowa";
            a.Points = 3;
            a.Date = new DateTime(2016, 11, 20, 09, 52, 37);
            context.Answers.AddOrUpdate(a);
            context.SaveChanges();


            ui = context.Users.Where(n => n.Nick == "drobiowy07").Select(n => n.UserID).FirstOrDefault();
            ti = context.Threads.Where(n => n.Title == "Myszka przewodowa czy bezprzewodowa?").Select(n => n.ThreadID).FirstOrDefault();
            a = new Models.Answer();
            a.ThreadID = ti;
            a.UserID = ui;
            a.Text = "Polecam przewodow¹";
            a.Points = 1;
            a.Date = new DateTime(2016, 11, 20, 09, 50, 37);
            context.Answers.AddOrUpdate(a);
            context.SaveChanges();


            ui = context.Users.Where(n => n.Nick == "wiktor500").Select(n => n.UserID).FirstOrDefault();
            ti = context.Threads.Where(n => n.Title == "Myszka przewodowa czy bezprzewodowa?").Select(n => n.ThreadID).FirstOrDefault();
            a = new Models.Answer();
            a.ThreadID = ti;
            a.UserID = ui;
            a.Text = "Bezprzewodowe lepsze. W przewodowych kable denerwuj¹ce";
            a.Points = 0;
            a.Date = new DateTime(2016, 11, 20, 09, 54, 37);
            context.Answers.AddOrUpdate(a);
            context.SaveChanges();


            ui = context.Users.Where(n => n.Nick == "Witek15").Select(n => n.UserID).FirstOrDefault();
            ti = context.Threads.Where(n => n.Title == "Myszka przewodowa czy bezprzewodowa?").Select(n => n.ThreadID).FirstOrDefault();
            a = new Models.Answer();
            a.ThreadID = ti;
            a.UserID = ui;
            a.Text = "Je¿eli do stacjonarnego, to przewodowa. Do laptopa bezprzewodowa.";
            a.Points = 2;
            a.Date = new DateTime(2016, 11, 20, 09, 51, 37);
            context.Answers.AddOrUpdate(a);
            context.SaveChanges();





            ui = context.Users.Where(n => n.Nick == "Witek15").Select(n => n.UserID).FirstOrDefault();
            ai = context.Answers.Where(n => n.Text == "Zdecydowanie bezprzewodowa").Select(n => n.AnswerID).FirstOrDefault();
            Comment cm = new Comment();
            cm.AnswerID = ai;
            cm.UserID = ui;
            cm.Text = "Popieram! Jedyne s³uszne rozwi¹zanie!";
            cm.Date = new DateTime(2016, 11, 20, 09, 53, 37);
            context.Comments.AddOrUpdate(cm);
            context.SaveChanges();


            ui = context.Users.Where(n => n.Nick == "drobiowy07").Select(n => n.UserID).FirstOrDefault();
            ai = context.Answers.Where(n => n.Text == "Zdecydowanie bezprzewodowa").Select(n => n.AnswerID).FirstOrDefault();
            cm = new Comment();
            cm.AnswerID = ai;
            cm.UserID = ui;
            cm.Text = "Dok³adnie, nie trzeba sie mêczyc z kabelkiem";
            cm.Date = new DateTime(2016, 11, 20, 09, 54, 37);
            context.Comments.AddOrUpdate(cm);
            context.SaveChanges();


            ui = context.Users.Where(n => n.Nick == "Jola17").Select(n => n.UserID).FirstOrDefault();
            ai = context.Answers.Where(n => n.Text == "Bezprzewodowe lepsze. W przewodowych kable denerwuj¹ce").Select(n => n.AnswerID).FirstOrDefault();
            cm = new Comment();
            cm.AnswerID = ai;
            cm.UserID = ui;
            cm.Text = "W jakim sensie denerwuj¹ce?";
            cm.Date = new DateTime(2016, 11, 20, 11, 50, 37);
            context.Comments.AddOrUpdate(cm);
            context.SaveChanges();


            ui = context.Users.Where(n => n.Nick == "drobiowy07").Select(n => n.UserID).FirstOrDefault();
            ai = context.Answers.Where(n => n.Text == "Bezprzewodowe lepsze. W przewodowych kable denerwuj¹ce").Select(n => n.AnswerID).FirstOrDefault();
            cm = new Comment();
            cm.AnswerID = ai;
            cm.UserID = ui;
            cm.Text = "¯e trzeba je zwijaæ i rozwijaæ i siê pl¹cz¹.";
            cm.Date = new DateTime(2016, 11, 20, 12, 50, 37);
            context.Comments.AddOrUpdate(cm);
            context.SaveChanges();


            ui = context.Users.Where(n => n.Nick == "drobiowy07").Select(n => n.UserID).FirstOrDefault();
            ai = context.Answers.Where(n => n.Text == "Je¿eli do stacjonarnego, to przewodowa. Do laptopa bezprzewodowa.").Select(n => n.AnswerID).FirstOrDefault();
            cm = new Comment();
            cm.AnswerID = ai;
            cm.UserID = ui;
            cm.Text = "Przy stacjonarnym kabel nie przeszkadza. A jak do laptopa to szybciej bez kabla.";
            cm.Date = new DateTime(2016, 11, 20, 15, 50, 37);
            context.Comments.AddOrUpdate(cm);
            context.SaveChanges();

        }
    }
}
