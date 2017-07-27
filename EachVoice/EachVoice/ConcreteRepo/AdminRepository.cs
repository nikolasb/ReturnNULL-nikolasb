using EachVoice.Abstract;
using EachVoice.Models;
using EachVoice.Models.CustomU;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EachVoice.ConcreteRepo
{
    public class AdminRepository : IAdminRepository
    {
        UCContext db;
        ApplicationDbContext adb;
        public AdminRepository(UCContext db, ApplicationDbContext adb)
        {
            this.db = db;
            this.adb = adb;
        }
        //public DbSet<ApplicationUser> AppUsers { get; }

        public IEnumerable<dynamic> FindUsersToDelete(string phoneNumber, string userName)
        {
            
            return adb.Users.Where(u => u.PhoneNumber.Equals(phoneNumber) || u.UserName.Equals(userName)).ToList();
        }
        public ApplicationUser FindUsersById(string id)
        {
            return adb.Users.FirstOrDefault(u => u.Id.Equals(id));
        }
        public void Delete(ApplicationUser u)
        {
            adb.Users.Remove(u);
            adb.SaveChanges();
        }
    }
}