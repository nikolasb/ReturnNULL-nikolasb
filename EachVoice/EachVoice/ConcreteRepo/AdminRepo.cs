using EachVoice.Abstract;
using EachVoice.Models;
using EachVoice.Models.CustomU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EachVoice.ConcreteRepo
{
    public class AdminRepo : IAdminRepo
    {
        UCContext db;
        //ApplicationDbContext adb;

        public AdminRepo(UCContext db/*, ApplicationDbContext adb*/)
        {
            this.db = db;
            //this.adb = adb;
        }
        public IEnumerable<UserComment> UserComments
        {
            get { return db.UserComments.ToList(); }
        }

        public virtual IEnumerable<UserVote> UserVotes
        {
            get { return db.UserVotes.ToList(); }
        }

        public virtual IEnumerable<AspNetUser> Users
        { get { return db.AspNetUsers.ToList(); } }
        public List<AspNetUser> FindUsersToDelete(string phoneNumber, string userName)
        {
            return db.AspNetUsers.Where(u => u.PhoneNumber.Equals(phoneNumber) || u.UserName.Equals(userName)).ToList();
        }
        public AspNetUser FindUsersById(string id)
        {
            return db.AspNetUsers.FirstOrDefault(u => u.Id.Equals(id));
        }
        public void Delete(AspNetUser u)
        {
            db.AspNetUsers.Remove(u);
            db.SaveChanges();
        }
        public void SaveState(AspNetUser u)
        {
            db.SaveChanges();
        }

        public List<UserComment>FindCommentsToDelete(string billId)
        {
            return db.UserComments.Where(c => c.blid.Equals(billId)).ToList();
        }
        public UserComment FindCommentById(int? id)
        {
            return db.UserComments.Find((int)id);
        }
        public void Delete(UserComment comment)
        {
            db.UserComments.Remove(comment);
            db.SaveChanges();
        }
        public List<UserVote> FindVotesToDelete(string blid)
        {
            return db.UserVotes.Where(v=>v.ucblid.Equals(blid)).ToList();
        }
        public UserVote FindVoteById(int? id)
        {
            return db.UserVotes.FirstOrDefault(v => v.id==id);
        }
        public void DeleteUserVote(UserVote v)
        {
            db.UserVotes.Remove(v);
            db.SaveChanges();
        }
    }
}