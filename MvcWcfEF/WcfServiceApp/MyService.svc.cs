using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfServiceApp
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MyService" in code, svc and config file together.
	// NOTE: In order to launch WCF Test Client for testing this service, please select MyService.svc or MyService.svc.cs at the Solution Explorer and start debugging.
	public class MyService : IMyService
	{
		public List<InfoUser> GetAllUser()
		{
			List<InfoUser> userlst = new List<InfoUser>();
			TestDBEntities tstDb = new TestDBEntities();
			var lstUsr = from k in tstDb.InfoUsers select k;
			foreach (var item in lstUsr)
			{
				InfoUser usr = new InfoUser();
				usr.ID = item.ID;
				usr.Name = item.Name;
				usr.Email = item.Email;
				userlst.Add(usr);
			}

			return userlst;
		}
		public InfoUser GetAllUserById(int id)
		{

			TestDBEntities tstDb = new TestDBEntities();
			var lstUsr = from k in tstDb.InfoUsers where k.ID == id select k;
			InfoUser usr = new InfoUser();
			foreach (var item in lstUsr)
			{
				usr.ID = item.ID;
				usr.Name = item.Name;
				usr.Email = item.Email;
			}

			return usr;
		}
		public int DeleteUserById(int Id)
		{
			TestDBEntities tstDb = new TestDBEntities();
			InfoUser usrdtl = new InfoUser();
			usrdtl.ID = Id;
			tstDb.Entry(usrdtl).State = EntityState.Deleted;
			int Retval = tstDb.SaveChanges();
			return Retval;
		}
		public int AddUser(string Name, string Email)
		{
			TestDBEntities tstDb = new TestDBEntities();
			InfoUser usrdtl = new InfoUser();
			usrdtl.Name = Name;
			usrdtl.Email = Email;
			tstDb.InfoUsers.Add(usrdtl);
			int Retval = tstDb.SaveChanges();
			return Retval;
		}
		public int UpdateUser(int Id, string Name, string Email)
		{
			TestDBEntities tstDb = new TestDBEntities();
			InfoUser usrdtl = new InfoUser();
			usrdtl.ID = Id;
			usrdtl.Name = Name;
			usrdtl.Email = Email;
			tstDb.Entry(usrdtl).State = EntityState.Modified;

			int Retval = tstDb.SaveChanges();
			return Retval;
		}

	}
}  

