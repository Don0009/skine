﻿using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController (IProductRepository repo): ControllerBase
    {

       


        [HttpGet]

        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts(string? brand, string? type)
        {
            return Ok(await repo.GetProductsAsync(brand, type));
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await repo.GetProductByIdAsync(id);

            if (product == null) return NotFound();

            return Ok(product);
        }

        [HttpPost]

        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            repo.AddProduct(product);

            if (await repo.SaveChangesAsync()) 
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

            repo.UpdateProduct(product);

            if (await repo.SaveChangesAsync()) {
            
            return NoContent();
            
            }

            return BadRequest("Problem Updating the product");
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
           var product = await repo.GetProductByIdAsync(id);

            if (product == null) return NotFound();

            repo.DeleteProduct(product);

            if (await repo.SaveChangesAsync()) { return NoContent(); }
            return BadRequest("Problem deleting the product");
        }

        [HttpGet("brands")]

        public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
        {
            return Ok(await repo.GetBrandsAsync());
        }

        [HttpGet("brands")]

        public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
        {
            return Ok(await repo.GetTypesAsync());
        }

        private bool ProductExists(int id)
        {
            return repo.ProductExists(id);
        }

    }
}
