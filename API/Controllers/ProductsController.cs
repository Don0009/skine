﻿using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Migrations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController (IGenericRepository<Product> repo): ControllerBase
    {


        [HttpGet]

        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts(string? brand, string? type, string? sort)
        {
            return Ok(await repo.ListAllAsync());
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await repo.GetByIdAsync(id);

            if (product == null) return NotFound();

            return Ok(product);
        }

        [HttpPost]

        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            repo.Add(product);

            if (await repo.SaveAllAsync()) 
            {
                return CreatedAtAction("GetProduct", new { id = product.Id}, product);
            }

            return BadRequest("Problem creating Product");
        }

       
    [HttpPut("{id:int}")]

    public async Task<ActionResult> UpdateProduct(int id,Product product)
        {
            if (product.Id != id || ProductExists(id)) 
                
                return BadRequest("Cant Update Product");

            repo.Update(product);

            if (await repo.SaveAllAsync()) {
            
            return NoContent();
            
            }

            return BadRequest("Problem Updating the product");
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
           var product = await repo.GetByIdAsync(id);

            if (product == null) return NotFound();

            repo.Remove(product);

            if (await repo.SaveAllAsync()) { return NoContent(); }
            return BadRequest("Problem deleting the product");
        }

        [HttpGet("brands")]

        public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
        {
            return Ok();
        }

        [HttpGet("brands")]

        public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
        {
            return Ok();
        }

        private bool ProductExists(int id)
        {
            return repo.Exists(id);
        }

    }
}
