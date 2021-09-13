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
    public class MarcaController : ControllerBase {
        private readonly IConfiguration _configuration;
        public MarcaController(IConfiguration configuration){
            _configuration = configuration;
        }
        [HttpGet]
        public JsonResult Get(){
            string query = @"select * from marcas ;";
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
        public JsonResult Post(Marca marca){
            string query = @"insert into marcas (marca) values (@marca)";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Default");
            MySqlDataReader myReader;
            using(MySqlConnection mycon = new MySqlConnection(sqlDataSource)){
                mycon.Open();
                using(MySqlCommand mycommand = new MySqlCommand(query,mycon)){
                    mycommand.Parameters.AddWithValue("@marca",marca.MarcaName);
                    myReader=mycommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult("Added successfully");
        }
        [HttpPut]
        public JsonResult Put(Marca marca){
            string query = @"update marcas set  marca = @marca where id = @id ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Default");
            MySqlDataReader myReader;
            using(MySqlConnection mycon = new MySqlConnection(sqlDataSource)){
                mycon.Open();
                using(MySqlCommand mycommand = new MySqlCommand(query,mycon)){
                    mycommand.Parameters.AddWithValue("@id", marca.Id);
                    mycommand.Parameters.AddWithValue("@marca",marca.MarcaName);
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
            string query = @"delete from marcas where id = @id ";
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
