using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutosApi.Models;
using AutosApi.Context;

namespace AutosApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AutosController : ControllerBase {
        private readonly AppDbContext _context;
        public AutosController(AppDbContext context){
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Get(){
            try
            {
                var listAutos = await _context.autos.Include(a=>a.MarcaActual).ToListAsync();
                return Ok(listAutos);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id){
            try
            {
                Auto auto = await _context.autos.Where(a => a.AutoId == id).Include(a => a.MarcaActual).FirstOrDefaultAsync();
                if(auto == null){
                    return NotFound();
                }
                return Ok(auto);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post(Auto auto){
            try
            {
                Marca marca = _context.marcas.Find(auto.MarcaId);
                marca.Autos.Add(auto);
                await _context.SaveChangesAsync();
                return Ok(auto);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public async Task<IActionResult> Put(Auto auto){
            try
            {
                _context.Update(auto);
                await _context.SaveChangesAsync();
                return Ok(auto);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id){
            try
            {
                var auto = await _context.autos.FindAsync(id);
                if(auto == null){
                    return NotFound();
                }
                _context.autos.Remove(auto);
                await _context.SaveChangesAsync();
                return Ok( new { message= "Auto eliminado ccon exito" } );
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
