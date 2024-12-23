using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RestaurantReservation.DbTables;
using RestaurantReservation.DTO;
using RestaurantReservation.Repository.Interfaces;
using RestaurantReservation.Repository.Services;

namespace RestaurantReservation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerMgmtController : ControllerBase
    {
        private readonly ICustomer _customer;
        public CustomerMgmtController(ICustomer customer)
        {
            _customer = customer;
        }

        [HttpPost]
        [Route("AddCustomer")]

        public async Task<IActionResult> AddNewCustomer([FromBody] CustomerDTO cust)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            if (cust == null)
            {
                return BadRequest("Fill all the details");
            }

            var createdRestaurant = await _customer.AddCustomer(cust);
            return Ok("Customer Added Successfully");
        }

        [Authorize]
        [HttpGet]
        [Route("AllCustomers")]
        public async Task<ActionResult<IEnumerable<CustomerTbl>>> GetAllCust()
        {
            var AllCusts = await _customer.GetAllCustomers();
            if (AllCusts.IsNullOrEmpty())
            {
                return NoContent();
            }
            else
            {
                return Ok(AllCusts);
            }
            
        }

        [HttpGet]
        [Route("CustomerById")]
        public async Task<ActionResult<IEnumerable<CustomerTbl>>> GetCustById(int id)
        {
            if (id == 0)
            {
                return BadRequest("Enter customer id");
            }
            var AllCusts = await _customer.GetCustomerById(id);
            if (AllCusts == null)
            {
                return NotFound("Customer not found!");
            }
            else
            {
                return Ok(AllCusts);
            }

        }


        [HttpDelete]
        [Route("DeleteCustomer")]
        public async Task<IActionResult> DeleteCustomer(int cid)
        {

            if (cid < 0) 
            {
                return BadRequest("Enter customer id");
            }
            var Cust = await _customer.GetCustomerById(cid);
            
            if (Cust==null)
            {
                return NotFound("Customer Not Found!");
            }
            else
            {
                var excust = await _customer.DeleteCustomer(cid);
                //var exrest1 = await _customer.DeleteCustomer(cid);
                return Ok("Customer deleted successfully...!");
            }
        }

        [HttpPut]
        [Route("UpdateCustomerInfo")]
        public async Task<IActionResult> UpdateCustomerInfo(int cid, [FromBody] UpdateCustomerDTO  CustInfo)
        {
            if (cid == 0)
                return BadRequest("Enter customer id");
            if (CustInfo == null)
            {
                return BadRequest("Fill all the details");
            }
            var excust = await _customer.GetCustomerById(cid);
            if (excust == null)
            {
                return NotFound("Customer Not Found!");
            }
            else
            {
                var excust1 = await _customer.UpdateCustInformation(CustInfo, cid);
                return Ok("Customer updated successfully...!");
            }
        }
    }
}
