using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.IdentityModel.Tokens;
using RestaurantReservation.DbTables;
using RestaurantReservation.DTO;
using RestaurantReservation.Repository.Interfaces;
using RestaurantReservation.Repository.Services;

namespace RestaurantReservation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableMgmtController : ControllerBase
    {
        private readonly ITableInfo _table;
        public TableMgmtController(ITableInfo table)
        {
            _table = table;
        }

        [HttpPost]
        [Route("AddTableDetails")]

        public async Task<IActionResult> AddNewTable([FromBody] TableDTO tbl)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            if (tbl == null)
            {
                return BadRequest("provide tbl capacity");
            }

            var createdTable = await _table.AddTableDetails(tbl);
            return Ok("Table Added Successfully");
        }

        [HttpGet]
        [Route("AllTableInformation")]
        public async Task<ActionResult<IEnumerable<Restaurant>>> GetAllTableinfos()
        {
            var AllTables = await _table.GetAllTableInfo();
            return Ok(AllTables);
        }

        [HttpGet]
        [Route("AvailableTableInformation")]
        public async Task<ActionResult<IEnumerable<Restaurant>>> GetAvailableTableinfos()
        {
            var AvlTables = await _table.GetAvailableTables();
            if (AvlTables.IsNullOrEmpty())
            {
                return NotFound("All tables are reserved");
            }
            return Ok(AvlTables);
        }

        [HttpGet]
        [Route("ReservedTableInformation")]
        public async Task<ActionResult<IEnumerable<Restaurant>>> GetReservedTableinfos()
        {
            var ReservedTables = await _table.GetReservedTables();
            if (ReservedTables.IsNullOrEmpty())
            {
                return NotFound("All tables are available");
            }
            return Ok(ReservedTables);
        }

        [HttpGet]
        [Route("TableInfoById")]
        public async Task<ActionResult<IEnumerable<Restaurant>>> GetTableInfoById(int id)
        {
            if (id < 0)
            {
                return BadRequest("Enter table id");
            }
            var Table = await _table.GetTableInfoById(id);   
            if (Table == null)
            {
                return NotFound("Table Info not found!");
            }
            else
            {
                return Ok(Table);
            }

        }

        [HttpDelete]
        [Route("DeleteTable")]
        public async Task<IActionResult> DeleteTableInfo(int tid)
        {
            if (tid < 0)
            {
                return BadRequest("Enter table id");
            }
            var extbl = await _table.GetTableInfoById(tid);
            if (extbl == null)
            {
                return NotFound("Table Not Found!");
            }
            else
            {
                var exrest1 = await _table.DeleteTableInfo(tid);
                return Ok("Table deleted successfully...!");
            }
        }

        [HttpPut]
        [Route("UpdateTableInfo")]
        public async Task<IActionResult> UpdateTableInfo(int tid, [FromBody] TableDTO tblInfo)
        {
            if (tid < 0)
                return BadRequest("Enter table id");
            if (tblInfo == null)
            {
                return BadRequest("Provide tablecapacity");
            }
            var extbl = await _table.GetTableInfoById(tid);
            if (extbl == null)
            {
                return NotFound("Table Not Found!");
            }
            else
            {
                var exrest1 = await _table.UpdateTableInfo(tblInfo,tid);
                return Ok("Table info updated successfully...!");
            }
        }

    }


}
