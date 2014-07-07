using System;
using System.Web.Mvc;
using CQRSlite.Commands;
using Messages.Commands;

namespace CQRSPub.Controllers
{
    public class ArticleController : Controller
    {
        private readonly ICommandSender _commandSender;

        public ArticleController(ICommandSender commandSender)
        {
            _commandSender = commandSender;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Submit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Submit(string title, string body)
        {
            _commandSender.Send(new SubmitArticle(Guid.NewGuid(), title, body));
            return RedirectToAction("Index");
        }

    }
}
