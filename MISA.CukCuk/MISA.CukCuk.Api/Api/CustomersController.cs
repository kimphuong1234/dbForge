using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.CukCuk.Api.Model;
using MySql.Data.MySqlClient;

namespace MISA.CukCuk.Api.Api
{
    [Route("api/v1/customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        /// <summary>
        /// Lấy dữ liệu khách hàng không truyền tham số
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: ABC(11/1/2021)
        [HttpGet]
        public IActionResult Get()
        {
            
            //Kết nối tới Database 
            var connetionString = "Host=103.124.92.43; Port=3306; Database=MS0_147_NVMANH_CukCuk; User Id=nvmanh; Password=12345678";
            IDbConnection dbConnection = new MySqlConnection(connetionString);
            //Dữ liệu từ database
            var customers = dbConnection.Query<Customer>("Proc_GetCustomers",commandType: CommandType.StoredProcedure);
            //Trả dữ liệu cho Client:

            return Ok(customers);
        }

        /// <summary>
        /// Lấy thông tin khách hàng theo Id => có truyền tham số
        /// </summary>
        /// <param name="Id">Id của khách hàng</param>
        /// <returns></returns>
        /// CreatedBy: ABC(11/01/2021)
        [HttpGet("{customerId}")]
        public IActionResult GetCustomer(string Id)
        {
            //Kết nối tới Database 
            var connetionString = "Host=103.124.92.43; Port=3306; Database=MS0_147_NVMANH_CukCuk; User Id=nvmanh; Password=12345678";
            IDbConnection dbConnection = new MySqlConnection(connetionString);
            //Dữ liệu từ database
            var customers = dbConnection.Query<Customer>("Proc_GetCustomerById",new {CustomerId = Id }, commandType: CommandType.StoredProcedure);
            //Trả dữ liệu cho Client:
            return Ok(customers);
        }
        /// <summary>
        /// Thêm mới khách hàng
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        /// CreatedBy: ABC(11/01/2021)
        [HttpPost]
        public IActionResult Post([FromBody] Customer customer)
        {
            customer.FullName = "Nguyễn Anh Mạnh";
            return Ok(customer);
        }

        [HttpPut]
        public IActionResult Put()
        {
            return Ok("PUT");
        }

        [HttpDelete]
        public IActionResult Delete()
        {
            return Ok("Delete");
        }
    }
}