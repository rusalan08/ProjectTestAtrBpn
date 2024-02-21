using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectTestAtrBpn.Data;
using ProjectTestAtrBpn.Model;
using System.Linq;

namespace ProjectTestAtrBpn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AtrBpnDbContext _context;
        public ProductController(AtrBpnDbContext context) 
        {
            _context = context;
        }
        
        [HttpGet("list")]
        public IResult Get()
        {
            var list = _context.tb_products.ToList();
            return TypedResults.Ok(list);
        }

        [HttpGet("detail")]
        public IResult Get(int id)
        {
            var detail = _context.tb_products.FirstOrDefault(x =>  x.Id == id);
            if(detail == null)
            {
                return TypedResults.NotFound($"Product dengan Id = {id} tidak di temukan");
            }
            return TypedResults.Ok(detail);
        }

        [HttpGet("listbycategory")]
        //public IResult Get([FromQuery(Name = "category")] Model.List<string> category)
        public IResult Get(string category)
        {
            var detail = _context.tb_products.Where(x => x.Category == category);
            if (detail == null)
            {
                return TypedResults.NotFound($"Product dengan category = {category} tidak di temukan");
            }
            return TypedResults.Ok(detail);
        }

        [HttpPost("create")]
        public IResult Post([FromBody] Product product) 
        {
            _context.tb_products.Add(product);
            _context.SaveChanges();
            return TypedResults.Ok($"Product dengan nama = {product.Name} berhasil di simpan");
        }

        [HttpPut("update")]
        public IResult Put([FromForm] Product product)
        {
            var detail = _context.tb_products.FirstOrDefault(x => x.Id == product.Id);
            if (detail == null)
            {
                return TypedResults.NotFound($"Product dengan Id = {product.Id} tidak ditemukan");
            }
            detail.Name = product.Name;
            detail.Description = product.Description;
            detail.Category = product.Category;
            detail.Price = product.Price;
            _context.SaveChanges();
            return TypedResults.Ok($"Product dengan id = {product.Id} berhasil diupdate");
        }

        [HttpDelete("{id}/delete")]
        public IResult Delete(int id)
        {
            var detail = _context.tb_products.FirstOrDefault(x => x.Id == id);
            if (detail == null)
            {
                return TypedResults.NotFound($"Product dengan Id = {id} tidak ditemukan");
            }
            _context.tb_products.Remove(detail);
            _context.SaveChanges();
            return TypedResults.Ok($"Product dengan id = {id} berhasil dihapus");
        }
    }
}
