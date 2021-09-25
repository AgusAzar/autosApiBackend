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
    public class MarcaController : ControllerBase
    {

        private readonly AppDbContext _context;
        public MarcaController(AppDbContext context){
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Get(){
            try
            {
                var listMarcas = await _context.marcas.Include(m=>m.Autos).ToListAsync();
                return Ok(listMarcas);
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
                Marca marca = await _context.marcas.FindAsync(id);
                if(marca == null){
                    return NotFound();
                }
                return Ok(marca);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post(Marca marca){
            try
            {
                _context.Add(marca);
                await _context.SaveChangesAsync();
                return Ok(marca);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public async Task<IActionResult> Put(Marca marca){
            try
            {
                _context.Update(marca);
                await _context.SaveChangesAsync();
                return Ok(marca);
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
                Marca marca = await _context.marcas.Where(m => m.MarcaId == id).Include(m => m.Autos).FirstOrDefaultAsync();
                if(marca == null){
                    return NotFound();
                }
                if( marca.Autos.Count() == 0 ){
                    _context.marcas.Remove(marca);
                    await _context.SaveChangesAsync();
                    return Ok( new { message= "Marca eliminado ccon exito" } );
                }
                throw new Exception("La marca contiene autos" );
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
