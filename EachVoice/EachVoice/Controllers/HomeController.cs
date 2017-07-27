using Microsoft.AspNet.Identity;
using EachVoice.Models.CustomU;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using static EachVoice.Controllers.ManageController;
using EachVoice.Abstract;
using EachVoice.Models;
using System.IO;
using Newtonsoft.Json;

namespace EachVoice.Controllers
{
    public class HomeController : Controller
    {
        //prepare the interface
        IUCCRepository uc;

        /// <summary>
        /// Set an IUCCRepository varialble to a specific instance of IUCCRepository.
        /// </summary>
        /// <param name="uc">An instance of IUCCRepository.</param>
        public HomeController(IUCCRepository uc)
        {
            this.uc = uc;
        }

        /// <summary>
        /// The method to show the Index (home) page.
        /// </summary>
        /// <returns>The Index view.</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// The method to show the About page.
        /// </summary>
        /// <returns>The About view.</returns>
        public ActionResult About()
        {
            return View();
        }

        /// <summary>
        /// The method to show the BillHub page.
        /// </summary>
        /// <returns>The BillHub view.</returns>
        [HttpGet]
        public ActionResult BillHub()
        {
            return View();
        }

        /// <summary>
        /// Displays which bill has the most comments.
        /// </summary>
        /// <returns>The _MostCommentedBill partial view using a MostCommentViewModel object.</returns>
        public ActionResult MostCommenttedBill()
        {
            var bill = uc.UserComments.GroupBy(z => z.blid).ToList().Select(gp => new { billid = gp.First().blid, mostCount = gp.Count(), title = gp.First().bltitle }).OrderByDescending(gp => gp.mostCount).ToList().First();
            MostCommentViewModel mcvw = new MostCommentViewModel();
            Debug.WriteLine("blid: " + bill.billid + " counts: " + bill.mostCount + " title: " + bill.title);
            mcvw.MostCount = bill.mostCount;
            UserComment c = uc.CreateComment(bill.billid, bill.title, "");
            mcvw.Comment = c;
            return PartialView("_MostCommenttedBill", mcvw);
        }

        /// <summary>
        /// Displays which bill has the most approval votes.
        /// </summary>
        /// <returns>The _MostApprovedView partial view using a MostApprovedBillViewModel object.</returns>
        public ActionResult MostApprovedBill()
        {
            var ap = uc.UserVotes.GroupBy(z => z.ucblid).Select(gp => new { blid = gp.First().ucblid, count = gp.Count(z => z.votebit == 0) }).OrderByDescending(y => y.count).First();
            MostApprovedBillViewModel mavm = new MostApprovedBillViewModel();
            UserVote v = uc.CreateVote1(ap.blid);
            mavm.aVote = ap.count;
            mavm.vote = v;
            return PartialView("_MostApprovedBill", mavm);
        }

        /// <summary>
        /// Sorts the results from searching for a bill.
        /// </summary>
        /// <returns>A JSON object containing a list of sorted bills.</returns>
        public JsonResult GetBillSearchSorted()
        {
            string state = Request.QueryString["stateIn"];
            string category = Request.QueryString["categoryIn"];
            string keyWord = Request.QueryString["keyWord"];
            int currentPage = Int32.Parse(Request.QueryString["currentPage"]);
            string apiKey = "&apikey=" + System.Web.Configuration.WebConfigurationManager.AppSettings["ostate"];

            if (category != "All")
            {
                category = "&subject=" + category;
            }
            else
            {
                category = "";
            }

            string responseText = String.Empty;
            var searchString = "https://openstates.org/api/v1/bills/?state=" + state + "&q=" + keyWord + category + apiKey;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(searchString);
            request.Method = "GET";

            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (StreamReader sr = new StreamReader(response.GetResponseStream())) { responseText = sr.ReadToEnd(); }
            }
            catch (Exception e)
            {
                Debug.Print("Mayday!");
            }


            var list = JsonConvert.DeserializeObject<List<BillModel>>(responseText);

            // This is for local testing
            /*
            BillModel One = new BillModel("ID!", "Title!", 100);
            BillModel Two = new BillModel("ID!", "Title!", 99);
            BillModel Three = new BillModel("ID!", "Title!", 98);
            BillModel Four = new BillModel("ID!", "Title!", 97);
            BillModel Five = new BillModel("ID!", "Title!", 96);
            BillModel Six = new BillModel("ID!", "Title!", 95);
            BillModel Seven = new BillModel("ID!", "Title!", 94);
            BillModel Eight = new BillModel("ID!", "Title!", 93);
            BillModel Nine = new BillModel("ID!", "Title!", 92);
            BillModel Ten = new BillModel("ID!", "Title!", 91);
            BillModel Eleven = new BillModel("ID!", "Title!", 90);
            BillModel Twelve = new BillModel("ID!", "Title!", 89);
            
            BillModel[] listArray = { One, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Eleven, Twelve};
            var list = listArray.ToArray();
            */

            foreach (BillModel item in list)
            {
                string billId = item.id;
                int voteCount = uc.UserVotes.Where(x => x.ucblid == billId).Count();
                int commentsCount = uc.UserComments.Where(x => x.blid == billId).Count();
                item.relevance = commentsCount + voteCount;
            }
            

            List<BillModel> listSorted = list.OrderByDescending(o => o.relevance).ToList();

            int pageSize = 6;
            var billsSorted = new
            {
                list = listSorted.Skip(currentPage * pageSize).Take(pageSize),
                pagesCount = (int)(listSorted.Count / pageSize)
            };

            return Json(billsSorted, JsonRequestBehavior.AllowGet);
        }

        // This is used to display the initial page
        /// <summary>
        /// The method to show the details of a selected bill.
        /// </summary>
        /// <param name="billID">The ID of a bill.</param>
        /// <param name="title">The title of a bill.</param>
        /// <returns>The BillPage view with the details of a selected bill.</returns>
        [HttpGet]
        public ActionResult BillPage(string billID, string title)
        {
            ViewBag.BillID = billID;
            ViewBag.BillTitle = title;

            if (billID == null || title == null) { return HttpNotFound(); }
            var list = new List<UserComment>();
            DiscussViewModel dvw = new DiscussViewModel();
            //as a template
            string uid = User.Identity.GetUserId();
            var comment = uc.CreateComment(billID, title, uid);

            //fill the viewmodel
            dvw.comment = comment;
            list = uc.GetAllCommentsByBillId(billID);
            if (list.Count() == 0) { ViewBag.Message = "Hi there is no comments yet, be the first one to leave a comment!"; }
            dvw.list = list;

            return View(dvw);
        }

        /// <summary>
        /// Registers a vote by the user for or against a bill.
        /// </summary>
        /// <param name="bill_id">The ID of a bill.</param>
        /// <param name="votebit">The vote of a user.</param>
        /// <returns>A JSON object containing the user's vote.</returns>
        [Authorize]
        [HttpPost]
        public ActionResult Vote(string bill_id, int? votebit)
        {
            string uid = User.Identity.GetUserId();
            Debug.WriteLine("billid: " + bill_id + "votebit: " + votebit);
            //var bitcount;
            if (votebit == null)
            {
                var avote = uc.TotalApproveCount(bill_id);
                var dvote = uc.TotalDisapproveCount(bill_id);
                var bitcount = new { approve = avote, disapprove = dvote };
                return Json(bitcount, JsonRequestBehavior.AllowGet);
            }

            var vt = uc.GetUserVote(bill_id, uid);
            if (vt == null)
            {
                try
                {
                    uc.CreateVote(bill_id, uid, vt, (int)votebit);
                    var avote = uc.TotalApproveCount(bill_id);
                    var dvote = uc.TotalDisapproveCount(bill_id);
                    var bitcount = new { approve = avote, disapprove = dvote };
                    return Json(bitcount, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "something went wrong" + ex.Message);
                }
            }
            else if (TryUpdateModel(vt, "", new string[] { "votebit" }))
            {
                try
                {
                    uc.saveState();
                    var avote = uc.TotalApproveCount(bill_id);
                    var dvote = uc.TotalDisapproveCount(bill_id);
                    var bitcount = new { approve = avote, disapprove = dvote };
                    return Json(bitcount, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "" + ex.Message);
                }
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }
        
        /// <summary>
        /// The method to post a comment on the details page of a selected bill.
        /// </summary>
        /// <param name="dvw">The DiscussViewModel object to add a comment to the database.</param>
        /// <returns>The BillPage view with the details and newly posted comment of a selected bill.</returns>
        [Authorize]
        [HttpPost]
        public ActionResult BillPage(DiscussViewModel dvw)
        {
            Debug.WriteLine("bill id: " + dvw.comment.blid + "comment: " + dvw.comment.comt);
            if (ModelState.IsValid)
            {
                try
                {
                    uc.AddComment(dvw.comment);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "unable to save the changes: " + ex.Message);
                }
                // redirect to the action with the right value, for testing
                return RedirectToAction("BillPage", new { billID = dvw.comment.blid, title = dvw.comment.bltitle });
            }

            return View(ViewBag.Message = "view model is not valid");
        }

        /// <summary>
        /// The method to show the RepHub page.
        /// </summary>
        /// <returns>The RepHub view.</returns>
        [HttpGet]
        public ActionResult RepHub()
        {
            return View();
        }

        /// <summary>
        /// Registers a vote by the user for or against a representative.
        /// </summary>
        /// <param name="legID">The ID of a representative.</param>
        /// <param name="RepName">The name of a representative.</param>
        /// <param name="Approval">The vote of a user.</param>
        /// <returns>A call to RepVoteCount which updates the number of votes for a representative.</returns>
        [Authorize]
        [HttpPost]
        public ActionResult VoteEntry(string legID, string RepName, int Approval)
        {
            string uid = User.Identity.GetUserId();
            if (uc.RepVotes.Where(x => x.RepID == legID && x.UserID == uid).Select(x => x.UserID).ToList().Count() > 0)
            //if (uc.RepVotes.Select(x => x.UserID).ToList().Contains(uid))
            {
                var userVote = uc.RepVotes.Where(x => x.UserID == uid && x.RepID == legID).Select(x => x.Approval).First();
                if (userVote == Approval)
                {
                    return RepVoteCount(legID);
                }
                else
                {
                    uc.RemoveRepVote(uid, legID); //will need to change as well
                    uc.AddRepVote(uid, legID, RepName, Approval);
                    return RepVoteCount(legID);
                }
            }
            uc.AddRepVote(uid, legID, RepName, Approval);
            return RepVoteCount(legID);
        }

        /// <summary>
        /// Displays the number of votes for each representative.
        /// </summary>
        /// <param name="legID">The ID of the representative.</param>
        /// <returns>A JSON object containing the number of votes.</returns>
        public ActionResult RepVoteCount(string legID)
        {
            var avote = uc.RepTotalApproveCount(legID);
            var dvote = uc.RepTotalDisapproveCount(legID);
            var voteCount = new { approve = avote, disapprove = dvote };
            return Json(voteCount, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Displays who the most and least popular representatives are.
        /// </summary>
        /// <returns>A JSON object containing the most and least popular representatives.</returns>
        //0 is approve, 1 is disapprove
        public ActionResult MostAndLeastPopularRepsCtlr()
        {
            var ap = uc.RepVotes.GroupBy(z => z.RepID).Select(gp => new { repID = gp.First().RepID, count = gp.Count(z => z.Approval == 0) }).OrderByDescending(y => y.count).First();
            string mostPopularRep = uc.RepVotes.Where(x => x.RepID == ap.repID).Select(x => x.RepName).First();
            var lp = uc.RepVotes.GroupBy(z => z.RepID).Select(gp => new { repID = gp.First().RepID, count = gp.Count(z => z.Approval == 1) }).OrderByDescending(y => y.count).First();
            string leastPopularRep = uc.RepVotes.Where(x => x.RepID == lp.repID).Select(x => x.RepName).First();
            var reps = new { mostPopular = mostPopularRep, leastPopular = leastPopularRep };
            return Json(reps, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Allows one to see his/her comments.
        /// </summary>
        /// <returns>The ViewComments view.</returns>
        [Authorize]
        [HttpGet]
        public ActionResult ViewComments()
        {
            var id = User.Identity.GetUserId();
            if (uc.PhoneNumberConfirmed(id))
            {
                var list = uc.FindCommentsByUserId(id);
                if (list.Count() < 1) ViewBag.Message = "You have not made any comments yet, go ahead join the community and give some of your opinion now!";
                return View(list);
            }
            else
            {
                return RedirectToAction("Index", "Manage", new { Message = ManageMessageId.ConfirmPhoneNumber });
            }

        }

        /// <summary>
        /// Allows one to edit his/her comments.
        /// </summary>
        /// <param name="id">The ID of a bill.</param>
        /// <returns>The Edit view with the details of a selected bill.</returns>
        [HttpGet]
        [Authorize]
        //modified to only allow user to edit their own comments
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return HttpNotFound();
            }

            UserComment com = uc.FindCommentByCommentId(id);
            string uid = com.netuid;
            if (com == null)
            {
                ViewBag.Message = "Could not find the comment to edit, try again";
                RedirectToAction("Index", "Manage");
            }
            ViewBag.Alert = "comment passed";//just for testing
            if (!User.Identity.GetUserId().Equals(uid))
            {
                return RedirectToAction("Error"); 
            }
            return View(com);
        }

        /// <summary>
        /// Allows one to edit his/her comments.
        /// </summary>
        /// <param name="id">The ID of a bill.</param>
        /// <param name="comment">This parameter is present only to override the other Edit function. The actual parameter is not used.</param>
        /// <returns>The Edit view with the details of a selected bill.</returns>
        [HttpPost]
        [Authorize]
        //[ChildActionOnly]
        //may need the id for the model binding but will see soon
        public ActionResult Edit(int? id, string comment)
        {
            //Debug.WriteLine("bill:" + blid + ", user id: " + uid + "comment:" + comment + "bill title: " + bltitle);
            UserComment com;

            com = uc.FindCommentByCommentId(id);
            if (com == null)
            {
                ViewBag.Message = "Something went wrong try again later";

                RedirectToAction("Index", "Manage");
            }

            if (TryUpdateModel(com, "", new string[] { "comt", "id" }))
            {
                try
                {
                    uc.saveState();
                    return RedirectToAction("Index", "Manage");
                }
                catch (Exception ex)
                {

                    ModelState.AddModelError("", "Edit Failed: " + ex.Message);
                }
            }

            ViewBag.Message = "Something went wrong try again later";
            return View(com);
        }

        /// <summary>
        /// Allows one to delete his/her comments.
        /// </summary>
        /// <param name="id">The ID of a bill.</param>
        /// <returns>The Delete view to delete a comment.</returns>
        [HttpGet]
        //modified to only allow user to delet their own comments
        public ActionResult Delete(int? id)
        {
            string uid = User.Identity.GetUserId();

            if (id == null)
            {
                ViewBag.Message = "something went wrong";
                return RedirectToAction("index", "Manage");
            }
            var c = uc.FindCommentByCommentId(id);
            if (c == null)
            {
                ViewBag.Message = "can not delete anything";
                return RedirectToAction("index", "Manage");
            }
            if (!uid.Equals(c.netuid)) { return RedirectToAction("Error"); }
            return View(c);
        }

        /// <summary>
        /// Allows one to delete his/her comments.
        /// </summary>
        /// <param name="id">The ID of a bill.</param>
        /// <param name="ct">This parameter is present only to override the other Edit function. The actual parameter is not used.</param>
        /// <returns>The Delete view to delete a comment.</returns>
        [HttpPost]
        public ActionResult Delete(int? id, UserComment ct)
        {
            UserComment c;

            c = uc.FindCommentByCommentId(id);
            if (c != null)
            {
                try
                {
                    uc.DeleteCommentByCommentId(id);
                    return RedirectToAction("Index", "Manage", new { Message = ManageMessageId.SucceedDeletion });
                }
                catch (Exception ex) { ModelState.AddModelError("", "deletion failed: " + ex.Message); }
            }
            return View(c);
        }

        /// <summary>
        /// Allows one to create a comment.
        /// </summary>
        /// <param name="bill_id">The ID of a bill.</param>
        /// <param name="ttl">The title of a bill.</param>
        /// <returns>The CreateComments view to create a comment.</returns>
        //--ZM pass a viewmodel but create a *model, will return viewmodel but only use the model, list is not needed
        [Authorize]
        [HttpGet]
        //antifogerytoken to protect XSS cross site script
        [ValidateAntiForgeryToken]
        public ActionResult CreateComments(string bill_id, string ttl)
        {
            if (bill_id == null || ttl == null) { return HttpNotFound(); }

            if (uc.Users.FirstOrDefault(z => z.Id.Equals(User.Identity.GetUserId())).PhoneNumberConfirmed)
            {
                return View(FillViewModel(bill_id, ttl, User.Identity.GetUserId()));
            }
            else
            {
                return RedirectToAction("Index", "Manage", new { Message = ManageMessageId.ConfirmPhoneNumber });
            }
        }

        /// <summary>
        /// Passes elements from the DiscussViewModel into the view.
        /// </summary>
        /// <param name="bill_id">The ID of a bill.</param>
        /// <param name="ttl">The title of a bill.</param>
        /// <param name="uid">The ID of the user.</param>
        /// <returns>A DiscuessViewModel with elements passed into it.</returns>
        public DiscussViewModel FillViewModel(string bill_id, string ttl, string uid)
        {
            DiscussViewModel dvw = new DiscussViewModel();
            dvw.comment = uc.CreateComment(bill_id, ttl, uid);
            var list = new List<UserComment>();
            list = uc.GetAllCommentsByBillId(bill_id);
            if (list.Count() == 0) { ViewBag.Message = "Hi there is no comments yet, be the first one to leave a comment!"; }
            dvw.list = list;

            return (dvw);
        }

        /// <summary>
        /// Allows one to create a comment.
        /// </summary>
        /// <param name="dvw">A DiscussViewModel object.</param>
        /// <returns>The CreateComments view to create a comment.</returns>
        [Authorize]
        [HttpPost]
        public ActionResult CreateComments(DiscussViewModel dvw)
        {
            //Debug.WriteLine("bill id: " + dvw.comment.blid + "comment: " + dvw.comment.comt);
            if (ModelState.IsValid)
            {
                try 
                {
                    uc.AddComment(dvw.comment);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "unable to save the changes: " + ex.Message);
                }
                // redirect to the action with the right value, for testing
                return RedirectToAction("CreateComments", new { bill_id = dvw.comment.blid, ttl = dvw.comment.bltitle });
            }

            return View(ViewBag.Message = "view model is not valid");
        }


        /// <summary>
        /// Determins if a user is logged in.
        /// </summary>
        /// <returns>A JSON object.</returns>
        public JsonResult isUserLoggedIn()
        {
            string uid = User.Identity.GetUserId();
            if(uid==null)
            {
                var loggedIn = new {isLoggedIn=0};
                return Json(loggedIn, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var loggedIn = new {isLoggedIn=1};
                return Json(loggedIn, JsonRequestBehavior.AllowGet);
            }
        }

    }
}