using System.Data.Common;
using LostAndFoundAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//ItemController Author: Samuel Gopie
namespace LostAndFoundAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {

        private ItemDatabase _db = new ItemDatabase();


        //need to add pagination in future update
        //GET /api/Items/all
        [HttpGet("/all")]
        public async Task<IActionResult> GetAllItems()
        {
            var items = await _db.Items.ToListAsync(); //https://stackoverflow.com/questions/13658862/get-all-rows-using-entity-framework-dbset
            return Ok(items);
        }

        //GET /api/Items/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetItemById(Guid id)
        {
            var item = await _db.Items.FirstOrDefaultAsync(x => x.Id == id);
            return Ok(item);
        }

        //POST /api/Items/addItems/[array of items]
        [HttpPost("/addItems")]
        public async Task<IActionResult> AddItems(List<Item> newItems)
        {
            foreach (Item item in newItems)
            {
                if (await _db.Items.FirstOrDefaultAsync(x => x.Id == item.Id) == null)
                {
                    await _db.Items.AddAsync(item);
                }
            }
            return Ok(newItems);
        }

        // DELETE /api/Items/removeItems/[array of items]
        [HttpDelete("/removeItems")]
        public async Task<IActionResult> RemoveItems(List<Item> deleteItems)
        {
            foreach (Item item in deleteItems)
            {
                var itemInDb = await _db.Items.FirstOrDefaultAsync(x => x.Id == item.Id);
                if  ( itemInDb != null)
                {
                    _db.Items.Remove(itemInDb);
                    _db.SaveChanges();
                }
            }
            return Ok(deleteItems);
        }
    }
}
