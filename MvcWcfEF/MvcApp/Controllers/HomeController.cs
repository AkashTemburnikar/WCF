using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApp.Models;

namespace MvcApp.Controllers
{
	public class HomeController : Controller
	{
		private readonly ServiceReference1.MyServiceClient ur = new ServiceReference1.MyServiceClient();

		public ActionResult Index()
		{
			List<User> lstRecord = new List<User>();
			var lst = ur.GetAllUser();
			foreach (var item in lst)
			{
				User usr = new User();
				usr.Id = item.ID;
				usr.Name = item.Name;
				usr.Email = item.Email;
				lstRecord.Add(usr);
			}
			return View(lstRecord);
		}

		public ActionResult Add()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Add(User mdl)
		{
			User usr = new User();
			usr.Name = mdl.Name;
			usr.Email = mdl.Email;
			ur.AddUser(usr.Name, usr.Email);
			return RedirectToAction("Index", "Home");
		}

		public ActionResult Delete(int id)
		{
			int retval = ur.DeleteUserById(id);
			if (retval > 0)
			{
				return RedirectToAction("Index", "Home");
			}
			return View();
		}

		public ActionResult Edit(int id)
		{
			var lst = ur.GetAllUserById(id);
			User usr = new User();
			usr.Id = lst.ID;
			usr.Name = lst.Name;
			usr.Email = lst.Email;
			return View(usr);

		}

		[HttpPost]
		public ActionResult Edit(User mdl)
		{
			User usr = new User();
			usr.Id = mdl.Id;
			usr.Name = mdl.Name;
			usr.Email = mdl.Email;
			int Retval = ur.UpdateUser(usr.Id, usr.Name, usr.Email);
			if (Retval > 0)
			{
				return RedirectToAction("Index", "Home");
			}
			return View();
		}
	}
}