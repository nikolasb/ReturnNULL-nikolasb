using System;
using System.Text;
using System.Collections.Generic;

using Moq;
using System.Web;
using System.Web.Routing;
using NUnit.Framework;
using System.Web.Mvc;

namespace EachVoice.Tests
{
    /// <summary>
    /// -zm testing routes
    /// </summary>
    [TestFixture]
    public class RoutesTest : RoutesBaseTest
    {
        //public TestRoutesBase()
        [SetUp]
        public void SetUp()
        {
            routes = RouteTable.Routes;
            EachVoice.RouteConfig.RegisterRoutes(routes);

            //RouteAssert.UseAssertEngine(new NunitAssertEngine());
        }
        
        [Test]
        public void DefaultURL_ShouldMapTo_Home_Index()
        {
            TestRouteMatch("~/", "Home", "Index");
        }

        [Test,Description("~/Home/About should has controller Home and action method about")]
        public void Home_About()
        {
            TestRouteMatch("~/Home/About","Home","About");
        }

        //[Test, Description("~/Home/Edit with additioanl segment of route value")]
        //public void Edit_GET()
        //{
        //    TestRouteMatch("~/Home/Edit?blid=or1234567&uid=sfga462454", "Home", "Edit",new { blid= "or1234567", uid= "sfga462454" });
        //}
        [Test,Description("the url does not match the place holder which predefined at route config should fail")]
        public void Fail_WrongUrl( )
        {
            TestRouteFail("~/Home/Index/Y/Z");
        }
        

        [Test]
        public void Calendar_RouteURL_Should_Map_To_Calendar()
        {
            TestRouteMatch("~/Home/CalendarOfEvents", "Home", "CalendarOfEvents");
        }

        [Test]
        public void RepHub_Route_Should_Map_To_RepHub()
        {
            TestRouteMatch("~/Home/RepHub", "Home", "RepHub");
        }

        [Test]
        public void BillHub_Route_Should_Map_To_BillHub()
        {
            TestRouteMatch("~/Home/BillHub", "Home", "BillHub");
        }

        //public void Dispose() clean up for future routes testing 
        [TearDown]
        public void TearDown()
        {
            RouteTable.Routes.Clear();
        }
    }
}
