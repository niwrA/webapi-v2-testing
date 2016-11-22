using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using TestWebApi;
using TestWebApi.Controllers;
using System.Threading;
using TestWebApi.Models;
using NUnit.Framework;
using System.Threading.Tasks;
using System.Web.Http;

namespace TestWebApi.Controllers.Tests
{
    [TestFixture()]
    public class RequestTestsControllerTest
    {
        [Test()]
        public async Task PostRequestTestTest_RejectsInvalidModel()
        {
            var sut = new RequestTestsController();
            var requestTest = new RequestTest();
            sut.Configuration = new System.Web.Http.HttpConfiguration();
            sut.Request = new System.Net.Http.HttpRequestMessage();
            sut.Validate(requestTest);
            var sutResult = sut.PostRequestTest(requestTest);
            var result = await sutResult.ExecuteAsync(CancellationToken.None);
            Assert.AreEqual(false, result.IsSuccessStatusCode);
        }

        [Test()]
        public async Task PostRequestTestTest_WithNull_ReturnsBadRequest()
        {
            var sut = new RequestTestsController();
            RequestTest requestTest = null;
            sut.Configuration = new System.Web.Http.HttpConfiguration();
            sut.Request = new System.Net.Http.HttpRequestMessage();
            sut.Validate(requestTest);
            var sutResult = sut.PostRequestTest(requestTest);
            var result = await sutResult.ExecuteAsync(CancellationToken.None);
            var statusCode = result.StatusCode;
            var content = await result.Content.ReadAsStringAsync();
            Assert.AreEqual(System.Net.HttpStatusCode.BadRequest, statusCode);
        }

        [Test()]
        public async Task PostRequestTestTest_RoutingTest()
        {
            var config = new System.Web.Http.HttpConfiguration();
            WebApiConfig.Register(config);
            string url = "http://localhost/api/requesttests/";
            var request = new System.Net.Http.HttpRequestMessage(System.Net.Http.HttpMethod.Post, new Uri(url));

            using (var server = new HttpServer(config))
            {
                var client = new System.Net.Http.HttpClient(server);

                using (var response = await client.SendAsync(request))
                {
                    Assert.IsTrue(System.Net.HttpStatusCode.NotFound != response.StatusCode);
                }
            }
        }

        [Test()]        
        public async Task PostRequestTestTest_Json_IntegrationTest()
        {
            var config = new System.Web.Http.HttpConfiguration();
            WebApiConfig.Register(config);
            string url = "http://localhost/api/requesttests/";
            var request = new System.Net.Http.HttpRequestMessage(System.Net.Http.HttpMethod.Post, new Uri(url));
            request.Content = new System.Net.Http.StringContent(@"{'Id':1, 'Name':'A Name'}");

            request.Content.Headers.ContentType.MediaType = "application/json";
            using (var server = new HttpServer(config))
            {
                var client = new System.Net.Http.HttpClient(server);

                using (var response = await client.SendAsync(request))
                {
                    Assert.AreEqual(System.Net.HttpStatusCode.Created, response.StatusCode);
                }
            }
        }

        [Test()]
        public async Task PostRequestTestTest_InvalidJson_IntegrationTest()
        {
            var config = new System.Web.Http.HttpConfiguration();
            WebApiConfig.Register(config);
            string url = "http://localhost/api/requesttests/";
            var request = new System.Net.Http.HttpRequestMessage(System.Net.Http.HttpMethod.Post, new Uri(url));
            request.Content = new System.Net.Http.StringContent(@"{'Id':1, 'Name':''}");

            request.Content.Headers.ContentType.MediaType = "application/json";
            using (var server = new HttpServer(config))
            {
                var client = new System.Net.Http.HttpClient(server);

                using (var response = await client.SendAsync(request))
                {
                    Assert.AreEqual(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
                }
            }
        }
    }
}