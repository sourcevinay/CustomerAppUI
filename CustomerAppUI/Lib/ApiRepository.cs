using Newtonsoft.Json;
using StudentCourseManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace StudentCourseManagement.Lib
{
    public class ApiRepository
    {
        private readonly string _url = "https://localhost:44385/api";
        private readonly HttpClient _httpClient;
        public ApiRepository()
        {
            _httpClient = new HttpClient();
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            var response = await _httpClient.GetAsync($"{_url}/Customers");
            response.EnsureSuccessStatusCode();
            var customers = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Customer>>(customers);
        }
        public async Task<Customer> FindCustomer(int? id)
        {
            var response = await _httpClient.GetAsync($"{_url}/Customers/{id}");
            response.EnsureSuccessStatusCode();
            var customers = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Customer>(customers);
        }
        public async Task<int> AddCustomers(Customer customer)
        {
            var json = JsonConvert.SerializeObject(customer);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_url}/Customers", content);
            if (response.IsSuccessStatusCode)
            {
                return 200;
            }
            else
            {
                return 500;
            }
        }
        public async Task<int> UpdateCustomers(int id, Customer customer)
        {
            var json = JsonConvert.SerializeObject(customer);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_url}/Customers/{id}", content);
            if (response.IsSuccessStatusCode)
            {
                return 200;
            }
            else
            {
                return 500;
            }
        }

        public async Task<int> DeleteCustomers(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_url}/Customers/{id}");
            if (response.IsSuccessStatusCode)
            {
                return 200;
            }
            else
            {
                return 500;
            }
        }
    }
}