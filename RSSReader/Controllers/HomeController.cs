using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ServiceModel.Syndication;
using System.Xml;
using RSSReader.Models;

namespace RSSReader.Controllers
{
    public class HomeController : Controller
    {
        public  ActionResult Feed()
        {
            var items = new List<SyndicationItem>();

            var feed = SyndicationFeed.Load(XmlReader.Create("http://feeds.news24.com/articles/news24/TopStories/rss"));
            ViewBag.feedTitle = "No Title";

            if (feed != null)
            {
                ViewBag.feedTitle = feed.Title.Text;
                items.AddRange(
                    feed.Items.Select(item => item.Title.Text != null && item.Summary.Text != null && item.Links[0] != null ? new SyndicationItem(item.Title.Text, item.Summary.Text, item.Links[0].Uri)
                        { PublishDate = item.PublishDate } : null));
            }

            ViewBag.Feeds = items;
            return View();
        }

        public ActionResult LogViewedFeed(string feedTitle)
        {
            bool read;
            return Feed();
        }

        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
