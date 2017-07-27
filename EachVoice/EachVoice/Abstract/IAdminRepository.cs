using EachVoice.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EachVoice.Abstract
{
    public interface IAdminRepository
    {
        //DbSet<ApplicationUser> AppUsers { get; }
       IEnumerable<dynamic> FindUsersToDelete(string phoneNumber, string userName);
       ApplicationUser FindUsersById(string id);
        void Delete(ApplicationUser u);
    }
}
