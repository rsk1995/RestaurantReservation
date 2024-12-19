using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.DTO;
using RestaurantReservation.Repository.Interfaces;
using RestaurantReservation.Repository.Services;

namespace RestaurantReservation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingManagementController : ControllerBase
    {
        private readonly IReserveBooking _booking;
        private readonly ICustomer _customer;
        private readonly ITableInfo _table;

        public BookingManagementController(IReserveBooking booking, ICustomer customer, ITableInfo table)
        {
            _booking = booking;
            _customer = customer;
            _table = table;
        }

        [HttpPost]
        [Route("BookTable")]

        public async Task<IActionResult> BookTable([FromBody] ReserveBookingDTO tblBookInfo)
        {
            if (tblBookInfo == null)
            {
                return BadRequest("Fill all the details");
            }
            var excust = await _customer.GetCustomerById(tblBookInfo.CId);
            var extbl = await _table.GetTableInfoById(tblBookInfo.TId);
            if (excust == null || extbl == null || extbl.Status=="Reserved")
            {
                return BadRequest("Customer not registered or table not found or table already reserved!");
            }
            else
            {
                bool result = await _booking.BookTable(tblBookInfo);
                /*if (result == true)
                {
                    return Ok("Table booked successfully");
                }
                else
                {
                    return NotFound("Table already booked");
                }*/
                return Ok("Table booked successfully");
            }
        }

    }
}
