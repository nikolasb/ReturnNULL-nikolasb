using EachVoice.Models;
using EachVoice.Models.CustomU;
using System.Collections.Generic;
namespace EachVoice.Abstract
{
    public interface IUCCRepository
    {
        IEnumerable<UserComment> UserComments { get; }
        IEnumerable<UserVote> UserVotes { get; }

        IEnumerable<AspNetUser> Users { get; }

        IEnumerable<RepVote> RepVotes { get; }
        //*********************************************
        void saveState();
        bool PhoneNumberConfirmed(string id);
        List<UserComment> FindCommentsByUserId(string id);
        UserComment FindCommentByCommentId(int? id);

        void DeleteCommentByCommentId(int? id);
        UserComment CreateComment(string bill_id, string ttl, string uid);

        List<UserComment> GetAllCommentsByBillId(string bill_id);

        void AddComment(UserComment c);
        //*******************************************************************

        int TotalApproveCount(string bill_id);

        int TotalDisapproveCount(string bill_id);

        UserVote GetUserVote(string bill_id, string uid);

        void CreateVote(string bill_id, string uid, UserVote vt, int votebit);

        void AddRepVote(string UserID, string legID, string RepName, int Approval);
        UserVote CreateVote1(string blid);
        int RepTotalApproveCount(string legID);
        int RepTotalDisapproveCount(string legID);

        void RemoveRepVote(string userID, string legID);
    }
}
