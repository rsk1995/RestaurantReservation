using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantReservation.DbTables;
using RestaurantReservation.DTO;
using RestaurantReservation.Repository.Interfaces;
using System.Security.Cryptography;
using static System.Reflection.Metadata.BlobBuilder;

namespace RestaurantReservation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantMgmtController : ControllerBase
    {
        private readonly IRestaurant _restaurant;
        public RestaurantMgmtController(IRestaurant restaurant)
        {
            _restaurant = restaurant;
        }

        [HttpPost]
        [Route("AddRestaurant")]

        public async Task<IActionResult> AddNewRestaurant([FromBody] AddRestaurant rest)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            
            if (rest == null)
            {
                return BadRequest("Fill all the details");
            }

            var createdRestaurant = await _restaurant.AddResta(rest);
            return Ok("Restaurant Added Successfully");
        }

        [HttpGet]
        [Route("AllRestaurant")]
        public async Task<ActionResult<IEnumerable<Restaurant>>> GetAllRests()
        {
            var AllRests = await _restaurant.GetAllRestaurants();
            return Ok(AllRests);
        }

        [HttpGet]
        [Route("RestaurantById")]
        public async Task<ActionResult<IEnumerable<Restaurant>>> GetRestById(int id)
        {
            if (id == 0)
            {
                return BadRequest("Enter restaurant id");
            }
            var AllRests = await _restaurant.GetRestaurantById(id);
            if (AllRests == null)
            {
                return NotFound("Restaurant not found!");
            }
            else
            {
                return Ok(AllRests);
            }
            
        }

        [HttpDelete]
        [Route("DeleteRestaurant")]
        public async Task<IActionResult> DeleteRestaurant(int rid)
        {
            if (rid == 0)
            {
                return BadRequest("Enter restaurant id");
            }
            var exrest = await _restaurant.GetRestaurantById(rid);
            if (exrest == null)
            {
                return NotFound("Restaurant Not Found!");
            }
            else
            {
                var exrest1 = await _restaurant.DeleteRestaurant(rid);
                return Ok("Restaurant deleted successfully...!");
            }
        }

        [HttpPut]
        [Route("UpdateRestaurantInfo")]
        public async Task<IActionResult> UpdateBookInfo(int rid, [FromBody] AddRestaurant restInfo)
        {
            if (rid == 0)
                return BadRequest("Enter resturant id");
            if (restInfo == null)
            {
                return BadRequest("Fill all the details");
            }
            var exrest = await _restaurant.GetRestaurantById(rid);
            if (exrest == null)
            {
                return NotFound("Restaurant Not Found!");
            }
            else
            {
                var exrest1 = await _restaurant.UpdateRestInformation(restInfo,rid);
                return Ok("Restaurant updated successfully...!");
            }
        }
    }
}














