using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using NUnit.Framework.Legacy;

namespace EscoIntegrationTests
{
    public class EscoIntegrationTests
    {
        private HttpClient _httpClient;
        private WebApplicationFactory<Program> _application;

        public EscoIntegrationTests()
        {
            _application = new WebApplicationFactory<Program>();
            _httpClient = _application.CreateClient();
        }

        [SetUp]
        public void Setup()
        {
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            _application.Dispose();
            _httpClient.Dispose();
        }
        
        
        

        [Test]
        public async Task WhenQueringForAllEscos_ThenThreeEscosAreReturned()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7004/odata/Escos");
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            dynamic json = JsonConvert.DeserializeObject(responseString)!;
            ClassicAssert.AreEqual(json.value.Count, 3);

            dynamic id = json.value[0].ID.ToString();
            ClassicAssert.AreEqual(id, "1");

            id = json.value[1].ID.ToString();
            ClassicAssert.AreEqual(id, "2");

            id = json.value[2].ID.ToString();
            ClassicAssert.AreEqual(id, "3");
        }

        [Test]
        public async Task WhenQueringForAnEsco_ThenTheCorrectCompanyIsReturned()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7004/odata/Escos(2)");
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            dynamic json = JsonConvert.DeserializeObject(responseString)!;
            ClassicAssert.AreEqual(json.ID.ToString(), "2");
        }

        [Test]
        public async Task WhenSkippingOneCompany_ThenTheNextThreeEscosAreReturned()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7004/odata/Escos?$skip=1");
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            dynamic json = JsonConvert.DeserializeObject(responseString)!;
            ClassicAssert.AreEqual(json.value.Count, 3);

            dynamic id = json.value[0].ID.ToString();
            ClassicAssert.AreEqual(id, "2");

            id = json.value[1].ID.ToString();
            ClassicAssert.AreEqual(id, "3");

            id = json.value[2].ID.ToString();
            ClassicAssert.AreEqual(id, "4");
        }

        [Test]
        public async Task WhenSkippingOneAndAskingForTwo_ThenTheNextTwoEscosAreReturned()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7004/odata/Escos?$top=2&$skip=1");
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            dynamic json = JsonConvert.DeserializeObject(responseString)!;
            ClassicAssert.AreEqual(json.value.Count, 2);

            dynamic id = json.value[0].ID.ToString();
            ClassicAssert.AreEqual(id, "2");

            id = json.value[1].ID.ToString();
            ClassicAssert.AreEqual(id, "3");
        }

        [Test]
        public async Task WhenFilteringById_ThenCorrectCompanyIsReturned()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7004/odata/Escos?$filter=ID eq 1");
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            dynamic json = JsonConvert.DeserializeObject(responseString)!;
            ClassicAssert.AreEqual(json.value.Count, 1);

            dynamic id = json.value[0].ID.ToString();
            ClassicAssert.AreEqual(id, "1");
        }

        [Test]
        public async Task WhenFilteringByCompanySize_ThenCorrectEscosAreReturned()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7004/odata/Escos?$filter=Size gt 50");
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            dynamic json = JsonConvert.DeserializeObject(responseString)!;
            ClassicAssert.AreEqual(json.value.Count, 2);

            dynamic id = json.value[0].ID.ToString();
            ClassicAssert.AreEqual(id, "2");

            id = json.value[1].ID.ToString();
            ClassicAssert.AreEqual(id, "4");
        }

        [Test]
        public async Task WhenOrderingBySize_ThenOrderIsReturned()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7004/odata/Escos?$orderby=Size Desc");
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            dynamic json = JsonConvert.DeserializeObject(responseString)!;
            ClassicAssert.AreEqual(json.value.Count, 3);

            dynamic id = json.value[0].ID.ToString();
            ClassicAssert.AreEqual(id, "4");

            id = json.value[1].ID.ToString();
            ClassicAssert.AreEqual(id, "2");

            id = json.value[2].ID.ToString();
            ClassicAssert.AreEqual(id, "1");
        }
    }
}