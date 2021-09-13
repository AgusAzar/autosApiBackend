using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Data;
using MySql.Data.MySqlClient;
using AutosApi.Models;

namespace AutosApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AutosController : ControllerBase {
        private readonly IConfiguration _configuration;
        public AutosController(IConfiguration configuration){
            _configuration = configuration;
        }
        [HttpGet]
        public JsonResult Get(){
            string query = @"select * from autos ;";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Default");
            MySqlDataReader myReader;
            using(MySqlConnection mycon = new MySqlConnection(sqlDataSource)){
                mycon.Open();
                using(MySqlCommand mycommand = new MySqlCommand(query,mycon)){
                    myReader=mycommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult(table);
        }
        [HttpPost]
        public JsonResult Post(Auto auto){
            string query = @"insert into autos (modelo,fotoUrl,marcaId) values (@modelo,@fotoUrl,@marcaId)";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Default");
            MySqlDataReader myReader;
            using(MySqlConnection mycon = new MySqlConnection(sqlDataSource)){
                mycon.Open();
                using(MySqlCommand mycommand = new MySqlCommand(query,mycon)){
                    mycommand.Parameters.AddWithValue("@modelo",auto.Modelo);
                    mycommand.Parameters.AddWithValue("@fotoUrl",auto.FotoUrl);
                    mycommand.Parameters.AddWithValue("@marcaId",auto.MarcaId);
                    myReader=mycommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult("Added successfully");
        }
        [HttpPut]
        public JsonResult Put(Auto auto){
            string query = @"update autos set modelo = @modelo, fotoUrl = @fotoUrl, marcaId = @marcaId where id = @id ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Default");
            MySqlDataReader myReader;
            using(MySqlConnection mycon = new MySqlConnection(sqlDataSource)){
                mycon.Open();
                using(MySqlCommand mycommand = new MySqlCommand(query,mycon)){
                    mycommand.Parameters.AddWithValue("@id", auto.Id);
                    mycommand.Parameters.AddWithValue("@modelo",auto.Modelo);
                    mycommand.Parameters.AddWithValue("@fotoUrl",auto.FotoUrl);
                    mycommand.Parameters.AddWithValue("@marcaId",auto.MarcaId);
                    myReader=mycommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult("Updated successfully");
        }
        [HttpDelete]
        public JsonResult Delete(int id){
            string query = @"delete from autos where id = @id ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Default");
            MySqlDataReader myReader;
            using(MySqlConnection mycon = new MySqlConnection(sqlDataSource)){
                mycon.Open();
                using(MySqlCommand mycommand = new MySqlCommand(query,mycon)){
                    mycommand.Parameters.AddWithValue("@id", id);
                    myReader=mycommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult("Deleted successfully");
        }
    }
}
