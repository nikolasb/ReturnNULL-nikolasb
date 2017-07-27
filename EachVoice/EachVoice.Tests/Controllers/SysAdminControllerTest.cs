using System;
using System.Text;
using System.Collections.Generic;
using NUnit;
using NUnit.Framework;
using EachVoice.Abstract;
using Moq;
using EachVoice.Controllers;
using System.Web.Mvc;
using EachVoice.Models.CustomU;

namespace EachVoice.Tests.Controllers
{
/// <summary>
/// Summary description for SysAdminControllerTest
/// </summary>
[TestFixture]
public class SysAdminControllerTest
{
    Mock<IAdminRepo> moq;
        [SetUp]

        public void SetupMoq()
        {
            moq = new Mock<IAdminRepo>();
            moq.Setup(m => m.Users).Returns(
                new AspNetUser[]
            {
            new AspNetUser { Id = "abc", Email = "abc@gmail.com", EmailConfirmed = false, PhoneNumber = "123456", PhoneNumberConfirmed = true, UserName = "abc", County = "washington"},
            new AspNetUser { Id = "xyz", Email = "xyz@gmail.com", EmailConfirmed = true, PhoneNumber = "987654", PhoneNumberConfirmed = false, UserName = "xyz", County = "polk"},
            new AspNetUser { Id = "jkl", Email = "jkl@gmail.com", EmailConfirmed = false, PhoneNumber = "668855", PhoneNumberConfirmed = true, UserName = "jkl", County = "clark"},
            new AspNetUser { Id = "zzz", Email = "zzz@gmail.com", EmailConfirmed = false, PhoneNumber = "668856", PhoneNumberConfirmed = true, UserName = "zzz", County = "clark"}
            });
            moq.Setup(m => m.UserComments).Returns(
                new UserComment[]
            {
            new UserComment {id = 0, blid = "wa20156", comt = "Very good", netuid = "abc", bltitle = "study while you are still can" },
            new UserComment {id = 1, blid = "or20156", comt = "well done", netuid = "xyz", bltitle = "do something while you are still alive" },
            new UserComment {id = 2, blid = "ca20156", comt = "space X is cool", netuid = "jkl", bltitle = "what is up" },
            new UserComment {id = 3, blid = "ca20156", comt = "space Z is cool", netuid = "zzz", bltitle = "what is up!" }
            });
            moq.Setup(m => m.UserVotes).Returns(
                new UserVote[]
            {
            new UserVote {id = 0, ucblid = "or20156", ucnetuid = "xyz", votebit = 1 },
            new UserVote {id = 1, ucblid = "ca20156", ucnetuid = "jkl", votebit = 0 },
            new UserVote {id = 2, ucblid = "wa20156", ucnetuid = "abc", votebit = 0 },
            new UserVote {id = 3, ucblid = "or20156", ucnetuid = "zzz", votebit = 1 }
            });
            //********************************************************************
            moq.Setup(m => m.FindUsersToDelete(It.IsAny<String>(), It.IsAny<string>())).Returns(
                new List<AspNetUser>()
                {
                 new AspNetUser { Id = "abc",Email = "abc@gmail.com",EmailConfirmed = false,PhoneNumber = "123456",PhoneNumberConfirmed = true,UserName = "abc",County = "washington"},
                 new AspNetUser { Id="xyz",Email="xyz@gmail.com",EmailConfirmed=true,PhoneNumber="987654",PhoneNumberConfirmed=false,UserName="xyz",County="polk"}
                });

        }
        [Test]
        public void DeleteVoteWithNullShouldReturnHttpNotFoundTest()
        {
            int dvoteCount = 0;
            int totalVotes = 4;
            moq.Setup(m => m.DeleteUserVote(It.IsAny<UserVote>())).Callback(() => { dvoteCount = ++dvoteCount; totalVotes = --totalVotes; });
            //arrange
            SysAdminController controller = new SysAdminController(moq.Object);
            //act
            RedirectToRouteResult r = (RedirectToRouteResult)controller.DeleteVote(0,null);

            //assert

            Assert.AreEqual(dvoteCount, 1);
            Assert.AreEqual(totalVotes, 3);
            Assert.AreEqual(r.RouteValues["Action"], "SearchToDeleteOrEdit");

        //moq.Setup(m => m.FindVoteById(It.IsAny < int?>())).Returns(new UserVote { id = 0, ucblid = "or20156", ucnetuid = "xyz", votebit = 1 });
    }

    [Test]
    public void SearchToDelOrEditShouldNotReturnNull()
    {
        //Arrange
        SysAdminController controller = new SysAdminController(moq.Object);
        //act
        ViewResult vr = (ViewResult)controller.SearchToDeleteOrEdit();
        //assert
        Assert.IsNotNull(vr);
    }
    [Test]
    public void ListUsersToDeleteOrEdit_Test()
    {
        //arrange
        SysAdminController controller = new SysAdminController(moq.Object);
        //act
        ViewResult vr = (ViewResult)controller.ListUsersToDeleteOrEdit("2452345245", "xyz");
        List<AspNetUser> us = (List<AspNetUser>)vr.ViewData.Model;
        //assert
        Assert.IsNotNull(vr);
        Assert.IsTrue(us.Exists(u => u.Id.Equals("abc")));
    }


    [Test]
    public void DeleteNullVoteReturnsHTTPNotFound()
    {
        // Arrange
        SysAdminController controller = new SysAdminController(moq.Object);
        // Act
        ActionResult vr = (ActionResult)controller.DeleteVote(null, new UserVote());
        // Assert
        Assert.IsInstanceOf(typeof(HttpNotFoundResult), vr);
    }

    [Test]
    public void DeleteVoteWithNonexistentIDRetunsHTTPNotFound()
    {
        // Arrange
        SysAdminController controller = new SysAdminController(moq.Object);
        // Act
        ActionResult vr = (ActionResult)controller.DeleteVote(123456, new UserVote());
        // Assert
        Assert.IsInstanceOf(typeof(HttpNotFoundResult), vr);
    }

    [Test]
    public void DeleteNullVoteGetReturnsHTTPNotFound()
    {
        // Arrange
        SysAdminController controller = new SysAdminController(moq.Object);
        // Act
        var vr = controller.DeleteVote(null);
        // Assert
        Assert.IsInstanceOf(typeof(HttpNotFoundResult), vr);
    }

    [Test]
    public void DeleteVoteGetWithNonexistentIDReturnsHTTPNotFound()
    {
        // Arrange
        SysAdminController controller = new SysAdminController(moq.Object);
        // Act
        var vr = controller.DeleteVote(123456);
        // Assert
        Assert.IsInstanceOf(typeof(HttpNotFoundResult), vr);

    }

    //[Test]
    //public void DeleteVoteWithNullShouldReturnHttpNotFoundTest()
    //{
    //    int dvoteCount = 0;
    //    int totalVotes = 4;
    //    moq.Setup(m => m.DeleteUserVote(It.IsAny<UserVote>())).Callback(() => { dvoteCount = dvoteCount++; totalVotes = totalVotes--; });
    //    //arrange
    //    SysAdminController controller = new SysAdminController(moq.Object);
    //    //act
    //    ActionResult r = controller.DeleteVote(0, null);
    //    //assert
    //    Assert.IsInstanceOf<HttpNotFoundResult>(r);
    //}

    [Test]
    public void ListUsersToDeleteOrEdit_Should_Not_Return_Null()
    {
        //Arrange
        SysAdminController controller = new SysAdminController(moq.Object);
        //Act
        ViewResult vr = (ViewResult)controller.ListUsersToDeleteOrEdit("123456", "abc");
        //Assert
        Assert.IsNotNull(vr);
    }

    [Test]
    public void ListCommentsToDeleteOrEdit_Should_Not_Return_Null()
    {
        //Arrange
        SysAdminController controller = new SysAdminController(moq.Object);
        //Act
        ViewResult vr = (ViewResult)controller.ListCommentsToDeleteOrEdit("or20156");
        //Assert
        Assert.IsNotNull(vr);
    }

    [Test]
    public void DeleteUser_Should_Return_HttpNotFoundResult_When_Actual_Parameter_Is_Null()
    {
        //Arrange
        SysAdminController controller = new SysAdminController(moq.Object);
        //Act
        ActionResult vr = (ActionResult)controller.DeleteUser(null);
        //Assert
        Assert.IsInstanceOf(typeof(HttpNotFoundResult), vr);
    }
}
}
