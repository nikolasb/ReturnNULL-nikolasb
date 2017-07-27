using EachVoice.Models;
using EachVoice.Models.CustomU;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EachVoice.Abstract
{
    public interface IAdminRepo
    {
        IEnumerable<UserComment> UserComments { get; }
        IEnumerable<UserVote> UserVotes { get; }
        IEnumerable<AspNetUser> Users { get; }

        List<AspNetUser> FindUsersToDelete(string phoneNumber, string userName);
        AspNetUser FindUsersById(string id);
        void Delete(AspNetUser u);
        void SaveState(AspNetUser u);
        List<UserComment>FindCommentsToDelete(string billId);
        UserComment FindCommentById(int? id);
        void Delete(UserComment comment);

        List<UserVote> FindVotesToDelete(string blid);
        UserVote FindVoteById(int? id);
        void DeleteUserVote(UserVote v);
    }
}
