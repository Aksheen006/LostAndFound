using System.Data.Common;
using LostAndFoundAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
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

        //POST /api/Items/addItems/{newitem}
        [HttpPost("/addItems/{newitem}")]
        public async Task<IActionResult> AddItemById(Item newItem)
        {

            if (await _db.Items.FirstOrDefaultAsync(x => x.Id == newItem.Id) == null)
            {
                await _db.Items.AddAsync(newItem);
            }
            return Ok(newItem);
        }

        // DELETE /api/Items/removeItems/[array of item ids]
        [HttpDelete("/removeItems")]
        public async Task<IActionResult> RemoveItems(List<Guid> deleteIds)
        {
            foreach (Guid id in deleteIds)
            {
                var itemInDb = await _db.Items.FirstOrDefaultAsync(x => x.Id == id);
                if (itemInDb != null)
                {
                    _db.Items.Remove(itemInDb);
                }
                _db.SaveChanges();
            }
            return Ok(deleteIds);
        }

        // DELETE /api/Items/removeItems/{deleteItemId}
        [HttpDelete("/removeItems/{id}")]
        public async Task<IActionResult> RemoveItemById(Guid id)
        {

            var itemInDb = await _db.Items.FirstOrDefaultAsync(x => x.Id == id);
            if (itemInDb != null)
            {
                _db.Items.Remove(itemInDb);
            }
            _db.SaveChanges();
            return Ok(id);
        }

        // PUT /api/Items/updateItems
        [HttpDelete("/updateItems")]
        public async Task<IActionResult> UpdateItems(List<Item> updatedItems)
        {
            foreach (Item updatedItem in updatedItems)
            {
                var itemInDb = await _db.Items.FirstOrDefaultAsync(x => x.Id == updatedItem.Id);
                if (itemInDb != null)
                {
                    if (updatedItem.Name != null)
                    {
                        itemInDb.Name = updatedItem.Name;
                    }
                    if (updatedItem.isFound == true)
                    {
                        itemInDb.isFound = updatedItem.isFound;
                    }
                    _db.SaveChanges();
                }
            }
            return Ok(updatedItems);
        }

        // PUT /api/Items/updateItems/{id}
        [HttpDelete("/updateItems/{id}")]
        public async Task<IActionResult> UpdateItem(Guid id, string newName = null, bool newFound = false)
        {
            var updateItem = await _db.Items.FirstOrDefaultAsync(x => x.Id == id);
            if (updateItem != null)
            {
                if (newName != null)
                {
                    updateItem.Name = newName;
                }
                if (newFound == true)
                {
                    updateItem.isFound = true;
                }
                _db.SaveChanges();
                return Ok(id);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
