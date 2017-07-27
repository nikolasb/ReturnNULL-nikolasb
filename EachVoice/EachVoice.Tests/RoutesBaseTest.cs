using Moq;
using NUnit.Framework;
using System;
using System.Web;
using System.Web.Routing;
using System.Reflection;

namespace EachVoice.Tests
{
    [TestFixture]
    public class RoutesBaseTest
    {
        public RouteCollection routes;
        protected HttpContextBase CreateHttpContext(string targetUrl = null, string httpMethod = "GET")
        {
            //-zma create the mock request 
            Mock<HttpRequestBase> mockRequest = new Mock<HttpRequestBase>();
            mockRequest.Setup(m => m.AppRelativeCurrentExecutionFilePath).Returns(targetUrl);
            mockRequest.Setup(m => m.HttpMethod).Returns(httpMethod);

            //-zma create the mock response
            Mock<HttpResponseBase> mockResponse = new Mock<HttpResponseBase>();
            mockResponse.Setup(m => m.ApplyAppPathModifier(It.IsAny<string>())).Returns<string>(s => s);

            //-zma create the mock context, using the request and response
            Mock<HttpContextBase> mockContext = new Mock<HttpContextBase>();
            mockContext.Setup(m => m.Request).Returns(mockRequest.Object);
            mockContext.Setup(m => m.Response).Returns(mockResponse.Object);

            //return the mocked context
            return mockContext.Object;
        }

        protected void TestRouteMatch(string url, string controller, string action,
                                    object routeProperties = null, string httpMethod = "GET")
        {
            //arrange
            //routes = new RouteCollection();
            //RouteConfig.RegisterRoutes(routes);
            //act - process the route
            RouteData result = routes.GetRouteData(CreateHttpContext(url, httpMethod));
            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(TestIncomingRouteResult(result, controller, action, routeProperties));
        }

        protected bool TestIncomingRouteResult(RouteData routeResult, string controller,
                                             string action, object propertySet = null)
        {
            //this one is a little nasty
            Func<object, object, bool> valCompare = (v1, v2) => { return StringComparer.InvariantCultureIgnoreCase.Compare(v1, v2) == 0; };
            bool result = valCompare(routeResult.Values["controller"], controller) && valCompare(routeResult.Values["action"], action);
            if (propertySet != null)
            {
                PropertyInfo[] propInfo = propertySet.GetType().GetProperties();
                foreach(PropertyInfo pi in propInfo)
                {
                    if (!(routeResult.Values.ContainsKey(pi.Name) && valCompare(routeResult.Values[pi.Name], pi.GetValue(propertySet, null))))
                    {
                        result = false;
                        break;
                    }
                }
            }
            return result;
        }

        protected void TestRouteFail(string url)
        {
            // arrange
            //routes = new RouteCollection();
            //RouteConfig.RegisterRoutes(routes);

            //act - process the route
            RouteData result = routes.GetRouteData(CreateHttpContext(url));

            //assert
            Assert.IsTrue(result == null || result.Route == null);
        }

    }
}
