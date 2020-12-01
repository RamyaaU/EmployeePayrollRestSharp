using EmployeePayrollRestSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;

namespace EmployeePayrollTest
{
    [TestClass]
    public class UnitTest1
    {
        RestClient client;

        [TestInitialize]
        public void SetUp()
        {
            //Initialize the base URL to execute requests made by the instance
            client = new RestClient("http://localhost:4000");
        }

        /// <summary>
        /// Gets the employee list.
        /// </summary>
        /// <returns></returns>
        //interface class to get list of employees
        private IRestResponse GetEmployeeList()
        {
            //Arrange
            //Initialize the request object with proper method and URL
            //passing rest request for employees list api using get method
            RestRequest request = new RestRequest("/employees", Method.GET);
            //Act
            //Execute the request
            IRestResponse response = client.Execute(request);
            return response;
        }

        /// <summary>
        /// UC 2 : Add new employee to the json file in JSON server and return the same
        /// </summary>
        [TestMethod]
        public void OnCallingPostAPI_ReturnEmployeeObject()
        {
            //Arrange
            //Initialize the request for POST to add new employee
            RestRequest request = new RestRequest("/employees/list", Method.POST);
            JsonObject jsonObj = new JsonObject();
            jsonObj.Add("name", "Raj");
            jsonObj.Add("salary", "7140");
            jsonObj.Add("id", "4");

            //Act
            IRestResponse response = client.Execute(request);

            //Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            Employee employee = JsonConvert.DeserializeObject<Employee>(response.Content);
            Assert.AreEqual("Raj", employee.Name);
            Assert.AreEqual("7140", employee.Salary);
            Console.WriteLine(response.Content);
        }
    }
}
