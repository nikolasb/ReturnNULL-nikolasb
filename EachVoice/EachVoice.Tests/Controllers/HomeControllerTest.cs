using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

using EachVoice;
using EachVoice.Controllers;
using EachVoice.Abstract;
using Moq;
using NUnit.Framework;
using EachVoice.Models.CustomU;
using EachVoice.Models;
using Microsoft.Owin.Host.SystemWeb;
using EachVoice.ConcreteRepo;
using static EachVoice.Controllers.ManageController;

namespace EachVoice.Tests.Controllers
{
    [TestFixture]
    public class HomeControllerTest
    {
        //start the moq
        private Mock<IUCCRepository> moq;

        public DiscussViewModel x;

        int savedbcount = 0;
        int comments = 4;
        int commentaddedcount = 0;

        int users = 4;
        int useraddedcount = 0;

        int votes = 4;
        int voteaddedcount = 0;
        //Nunit Framework
        [SetUp]
        public void SetupMoq()
        {
            moq = new Mock<IUCCRepository>();

           
            x = new DiscussViewModel
            {
                comment = new UserComment { id = 0, blid = "wa20156", comt = "Very good", netuid = "abc", bltitle = "study while you are still can" },
                list = new List<UserComment> {
                    new UserComment {id = 0, blid="wa20156",comt="Very good",netuid="abc",bltitle="study while you are still can" },
                    new UserComment {id = 1, blid="or20156",comt="well done",netuid="xyz",bltitle="do something while you are still alive" },
                    new UserComment {id = 2, blid="ca20156",comt="space X is cool",netuid="jkl",bltitle="what is up" },
                    new UserComment {id = 3, blid="ca20156",comt="space Z is cool",netuid="zzz",bltitle="what is up" }
                }
            };

            //fake user comment
            moq.Setup(m => m.UserComments).Returns(
                new UserComment[] {
                    new UserComment {id = 0, blid="wa20156",comt="Very good",netuid="abc",bltitle="study while you are still can" },
                    new UserComment {id = 1, blid="or20156",comt="well done",netuid="xyz",bltitle="do something while you are still alive" },
                    new UserComment {id = 2, blid="ca20156",comt="space X is cool",netuid="jkl",bltitle="what is up" },
                    new UserComment {id = 3, blid="ca20156",comt="space Z is cool",netuid="zzz",bltitle="what is up!" }
                });
            //fake user vote
            moq.Setup(m => m.UserVotes).Returns(
               new UserVote[] {
                    new UserVote {id=0,ucblid="or20156",ucnetuid="xyz",votebit=1 },
                    new UserVote {id=1,ucblid="ca20156",ucnetuid="jkl",votebit=0 },
                    new UserVote {id=2,ucblid="wa20156",ucnetuid="abc",votebit=0 },
                    new UserVote {id=3,ucblid="or20156",ucnetuid="zzz",votebit=1 },
               });
            // fake users
            moq.Setup(m => m.Users).Returns(
                new AspNetUser[] {
                    new AspNetUser { Id="abc",Email="abc@gmail.com",EmailConfirmed=false,PhoneNumber="123456",PhoneNumberConfirmed=true,UserName="abc",County="washington"},
                    new AspNetUser { Id="xyz",Email="xyz@gmail.com",EmailConfirmed=true,PhoneNumber="987654",PhoneNumberConfirmed=false,UserName="xyz",County="polk"},
                    new AspNetUser { Id="jkl",Email="jkl@gmail.com",EmailConfirmed=false,PhoneNumber="668855",PhoneNumberConfirmed=true,UserName="jkl",County="clark"},
                    new AspNetUser { Id="zzz",Email="zzz@gmail.com",EmailConfirmed=false,PhoneNumber="668856",PhoneNumberConfirmed=true,UserName="zzz",County="clark"}

                });
            moq.Setup(m => m.DeleteCommentByCommentId(It.IsAny<int?>())).Callback(() => comments = comments - 1);
            moq.Setup(m => m.FindCommentByCommentId(0)).Returns(
                new UserComment { id = 0, blid = "wa20156", comt = "Very good", netuid = "abc", bltitle = "study while you are still can" });
            moq.Setup(m => m.PhoneNumberConfirmed(It.IsAny<string>())).Returns(true);
            moq.Setup(m => m.AddComment(It.IsAny<UserComment>())).Callback(() => { comments = ++comments; commentaddedcount = ++commentaddedcount; });
            //moq.Setup(m => m.FindCommentsByUserId(It.IsAny<string>())).Returns(            
            //new List<UserComment>{
            //    new UserComment {id = 2, blid="ca20156",comt="space X is cool",netuid="jkl",bltitle="what is up" },
            //    new UserComment {id = 3, blid="ca20156",comt="space Z is cool",netuid="zzz",bltitle="what is up!" }
            // });

        }

        [Test]
        public void UserEditWithParmNullShouldReturnNotFound_Get()
        {
            //Arrage Http Get Edit
            HomeController controller = new HomeController(moq.Object);

            //act
            ActionResult r = controller.Edit(null);

            //assert
            Assert.IsInstanceOf<HttpNotFoundResult>(r);
        }

        [Test]
        public void UserEditWithParmShouldRedirectToIndexManage_Get()
        {
            //Arrage 
            HomeController controller = new HomeController(moq.Object);

            //act
            ViewResult r = (ViewResult)controller.Edit(0);
            UserComment c = (UserComment)r.ViewData.Model;
            //assert
            Assert.IsNotNull(r);
            Assert.AreEqual(r.ViewBag.Alert, "comment passed");
            //Assert.AreEqual(r.ViewName, "Edit");
            Assert.AreEqual(c.bltitle, "study while you are still can");
        }

        [Test]
        public void UserDeleteWithParm_Get()
        {
            //arrange
            HomeController controller = new HomeController(moq.Object);
            //act
            ViewResult r = (ViewResult)controller.Delete(0);
            UserComment c = (UserComment)r.ViewData.Model;
            //assert
            Assert.AreEqual(c.blid, "wa20156");
            Assert.IsNotNull(r);
            //Assert.AreEqual(r.View,"Delete");
        }

        [Test]
        public void UserDeleteWithParm_Post()
        {
            int savedbcount = 0;
            int comments = 4;
            moq.Setup(m => m.DeleteCommentByCommentId(It.IsAny<int?>())).Callback(() => comments = comments - 1);

            //arrange
            HomeController controller = new HomeController(moq.Object);
            //act
            RedirectToRouteResult r = (RedirectToRouteResult)controller.Delete(0, null);

            //assert
            Assert.AreEqual(comments, 3);

            Assert.AreEqual(r.RouteValues["Message"], ManageMessageId.SucceedDeletion);
            Assert.AreEqual(r.RouteValues["Action"], "Index");
            Assert.AreEqual(r.RouteValues["Controller"], "Manage");
        }

        [Test]
        public void CreateCommentsTest()
        {
            //arrange
            HomeController controller = new HomeController(moq.Object);
            //act
            RedirectToRouteResult r = (RedirectToRouteResult)controller.CreateComments(x);
            //assert
            Assert.AreEqual(comments, 5);
            Assert.AreEqual(commentaddedcount,1);
            Assert.AreEqual(r.RouteValues["action"], "CreateComments");
            Assert.AreEqual(r.RouteValues["bill_id"], "wa20156");
        }

        //[Test]
        //public void FillViewModelTest()
        //{
        //    //arrange
        //    HomeController controller = new HomeController(moq.Object);
        //    //act
        //    DiscussViewModel dvm = controller.FillViewModel()
        //}

        [Test]
        public void GetCommentsQueryTest()
        {
            HomeController controller = new HomeController(moq.Object);
        }
    }
}