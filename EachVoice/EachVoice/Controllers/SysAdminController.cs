using EachVoice.Abstract;
using EachVoice.ConcreteRepo;
using EachVoice.Models;
using EachVoice.Models.CustomU;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EachVoice.Controllers
{
 [Authorize(Roles ="admin")]
    public class SysAdminController : Controller
    {
        IAdminRepo Arepo;
        public SysAdminController(IAdminRepo Arepo)
        {
            this.Arepo = Arepo;
        }
        [HttpGet]
        public ActionResult SearchToDeleteOrEdit()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ListUsersToDeleteOrEdit(string phoneNumber, string userName)
        {

            //Debug.WriteLine("phone# " + phoneNumber + " userName: " + userName);
            return View(Arepo.FindUsersToDelete(phoneNumber, userName));
        }

        [HttpGet]
        public ActionResult DeleteUser(string id)
        {
            Debug.WriteLine("user id: "+ id);
            if (id == null) return HttpNotFound();
            AspNetUser au = Arepo.FindUsersById(id);
            
            return View(au);
        }

        [HttpPost]
        public ActionResult DeleteUser( string UserName, string id, AspNetUser u)
        {
            if (id == null) return HttpNotFound();
            AspNetUser au = Arepo.FindUsersById(id);
            if (au == null) return HttpNotFound();
            Arepo.Delete(au);
            ViewBag.Message = "User has been deleted successfully";
            return RedirectToAction("SearchToDeleteOrEdit");
        }
        [HttpGet]
        public ActionResult EditUser(string id)
        {
            if (id == null) return HttpNotFound();
            AspNetUser u = Arepo.FindUsersById(id);
            if (u == null) return HttpNotFound();

            return View(u);
        }
        [HttpPost]
        public ActionResult EditUser(string County, string Email, string Id, string PhoneNumber, bool TwoFactorEnabled, string UserName)
        {
            if (Id == null) return HttpNotFound();
            AspNetUser u = Arepo.FindUsersById(Id);
            if (u == null) return HttpNotFound();
            if(TryUpdateModel(u,"",new string[] { "County","Email", "Id","PhoneNumber", "TwoFactorEnabled","UserName" }))
            {
                try
                {
                    Arepo.SaveState(u);
                    ViewBag.Message = "changes have successfully been saved.";
                    return RedirectToAction("SearchToDeleteOrEdit","SysAdmin");
                }

                catch (Exception ex)
                {
                    ModelState.AddModelError("","not able to save the changes: "+ex.Message);                    
                }
            }
            ViewBag.Message = "Something went wrong! not able to save the changes.";
            return View("SearchToDeleteOrEdit");
        }
        //[HttpGet]
        //public ActionResult CreateUser(string id)
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult CreateUser(AspNetUser u)
        //{
        //    return View();
        //}
      
        //public ActionResult SearchComemntsToDeleteOrEdit()
        //{
        //    return View();
        //}
        [HttpGet]
        public ActionResult ListCommentsToDeleteOrEdit(string blid)
        {

            Debug.WriteLine(" billId: " + blid);
            return View(Arepo.FindCommentsToDelete(blid));
        }

        [HttpGet]
        public ActionResult DeleteComment(int? id)
        {
            Debug.WriteLine("comment id: " + id);
            if (id == null) return HttpNotFound();
            UserComment comment = Arepo.FindCommentById(id);

            return View(comment);
        }

        [HttpPost]
        public ActionResult DeleteComment(int? id, UserComment comment)
        {
            if (id == null) return HttpNotFound();
            UserComment com = Arepo.FindCommentById(id);
            if (com == null) return HttpNotFound();
            Arepo.Delete(com);
            ViewBag.Message = "Comment has been deleted successfully";
            return RedirectToAction("SearchToDeleteOrEdit");
        }

        //public ActionResult SearchVotesToDeleteOrEdit()
        //{
        //    return View();
        //}
        [HttpGet]
        public ActionResult ListVotesToDeleteOrEdit(string blid, string uid)
        {

            // Debug.WriteLine(" billId: " + blid);
            List<UserVote> vs = Arepo.FindVotesToDelete(blid);
            return View(vs);
        }
        [HttpGet]
        public ActionResult DeleteVote(int? id)
        {
            if (id == null) return HttpNotFound();
            UserVote v = Arepo.FindVoteById(id);
            if (v == null) return HttpNotFound();
            return View(v);
        }

        [HttpPost]
        public ActionResult DeleteVote(int? id, UserVote vote)
        {
            
            if (id == null) return HttpNotFound();
            UserVote v = Arepo.FindVoteById(id);
            if (v == null) return HttpNotFound();
            Arepo.DeleteUserVote(v);
            ViewBag.Message = "Comment has been deleted successfully";
            return RedirectToAction("SearchToDeleteOrEdit");
        }
    }
}   
