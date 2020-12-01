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
        /// UC 3 : Adds multiple employees to the json file in JSON server and returns the same
        /// </summary>
        [TestMethod]
        public void OnCallingPostAPIForAEmployeeListWithMultipleEMployees_ReturnEmployeeObject()
        {
            // Arrange
            List<Employee> employeeList = new List<Employee>();
            employeeList.Add(new Employee { Name = "Kylie ", Salary = "885040" });
            employeeList.Add(new Employee { Name = "Kendall ", Salary = "125030" });
            employeeList.Add(new Employee { Name = "Kim ", Salary = "125040" });
            //Iterate the loop for each employee
            foreach (var v in employeeList)
            {
                //Initialize the request for POST to add new employee
                RestRequest request = new RestRequest("/employees/list", Method.POST);
                JsonObject jsonObj = new JsonObject();
                jsonObj.Add("Name", v.Name);
                jsonObj.Add("Salary", v.Salary);
                // jsonObj.Add("Id", v.Id);
                //Added parameters to the request object such as the content-type and attaching the jsonObj with the request
                request.AddParameter("application/json", jsonObj, ParameterType.RequestBody);

                //Act
                IRestResponse response = client.Execute(request);

                //Assert
                Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
                Employee employee = JsonConvert.DeserializeObject<Employee>(response.Content);
                Assert.AreEqual(v.Name, employee.Name);
                Assert.AreEqual(v.Salary, employee.Salary);
                Console.WriteLine(response.Content);
            }
        }
    }
}

