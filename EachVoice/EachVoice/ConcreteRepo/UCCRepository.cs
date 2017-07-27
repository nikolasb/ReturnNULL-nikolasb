using EachVoice.Abstract;
using EachVoice.Models;
using EachVoice.Models.CustomU;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EachVoice.ConcreteRepo
{
    public class UCCRepository : IUCCRepository
    {
        private UCContext db;
        private ApplicationDbContext adb;
        
        public UCCRepository(UCContext db, ApplicationDbContext adb)
        {
            this.db = db;
            this.adb = adb;
        }
        //nevagation property
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

        public IEnumerable<RepVote> RepVotes
        {
            get
            {
                return db.RepVotes.ToList();
            }
        }

        public void saveState()
        {
            db.SaveChanges();

        }
        public bool PhoneNumberConfirmed(string id)
        {

            return adb.Users.Find(id).PhoneNumberConfirmed;
        }
        public List<UserComment> FindCommentsByUserId(string id)
        {
            return db.UserComments.Where(u => u.netuid.Equals(id)).ToList();
        }
        public List<UserComment> GetAllCommentsByBillId(string bill_id)
        {
            return db.UserComments.Where(z => z.blid == bill_id).ToList().OrderByDescending(x => x.bltitle).ToList();
        }
        public UserComment FindCommentByCommentId(int? id)
        {
            UserComment a = db.UserComments.FirstOrDefault(z=>z.id==id);
            return a;

        }

        public void AddComment(UserComment c)
        {
            db.UserComments.Add(c);
            db.SaveChanges();
        }
        //void UpdateModel(string comment, int? id)
        //{


        //}
        public void DeleteCommentByCommentId(int? id)
        {
            UserComment c = db.UserComments.Find(id);
            db.UserComments.Remove(c);
            db.SaveChanges();
        }

        public UserComment CreateComment(string bill_id, string ttl, string uid)
        {
            UserComment comment = db.UserComments.Create();
            comment.blid = bill_id;
            comment.bltitle = ttl;
            comment.netuid = uid;
            return comment;
        }

        public void CreateVote(string bill_id, string uid, UserVote vt, int votebit)
        {
            vt = db.UserVotes.Create();
            
            vt.votebit = votebit;
            vt.ucblid = bill_id;
            vt.ucnetuid = uid;
            db.UserVotes.Add(vt);
            db.SaveChanges();
        }
        public UserVote CreateVote1(string blid)
        {
            UserVote vote = db.UserVotes.Create();
            vote.ucblid = blid;
            vote.votebit = 2;
            vote.ucnetuid = "";
            return vote;
        }

        public void AddRepVote(string UserID, string legID, string RepName, int Approval)
        {
            RepVote rv=db.RepVotes.Create();
            rv.Approval = Approval;
            rv.RepID = legID;
            rv.RepName = RepName;
            rv.UserID = UserID;
            db.RepVotes.Add(rv);
            db.SaveChanges();
        }

        //public int TotalApproveCount(string bill_id, int? votebit)
        //{
        //    return db.UserVotes.Where(z => z.ucblid.Equals(bill_id)).ToList().Count(a => a.votebit == 0);
        //}
        public int TotalApproveCount(string bill_id)
        {
            return db.UserVotes.Where(z => z.ucblid.Equals(bill_id)).ToList().Count(a => a.votebit == 0);
        }

        public int TotalDisapproveCount(string bill_id)
        {
            return db.UserVotes.Where(z => z.ucblid.Equals(bill_id)).Count(d => d.votebit == 1);
        }

        public UserVote GetUserVote(string bill_id, string uid)
        {
            UserVote uv = db.UserVotes.FirstOrDefault(z => z.ucblid.Equals(bill_id) & z.ucnetuid.Equals(uid));
            return uv;
        }

        public int RepTotalApproveCount(string legID)
        {
            return db.RepVotes.Where(z => z.RepID.Equals(legID)).Count(d => d.Approval == 0);
        }

        public int RepTotalDisapproveCount(string legID)
        {
            return db.RepVotes.Where(z => z.RepID.Equals(legID)).Count(d => d.Approval == 1);
        }

        public void dispose()
        {
            db.Dispose();
        }

        public void RemoveRepVote(string userID, string legID)
        {
            RepVote rv = db.RepVotes.Where(x => x.UserID == userID && x.RepID == legID).First();
            db.RepVotes.Remove(rv);
            db.SaveChanges();
        }
    }
}