using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Aplikacja_Demo.Models.Warehouse;

namespace Aplikacja_Demo.Controllers
{
    public class CargoController : Controller
    {
        private List<Cargo> db_cargo = new List<Cargo>();
      //  public MySqlConnection connection = new MySqlConnection("datasource=localhost;port=3306;database=magazyn;username=root;password=");
        public MySqlConnection connection = new MySqlConnection("datasource=\"localhost\";port=3306;database=magazyn;username=Admin;password=Magisterka");


        // GET: Warehouse
        public ActionResult Index()
        {
            List<string> result = new List<string>();
            connection.Open();
            if (connection.State == System.Data.ConnectionState.Open)
            {
                ViewBag.Message = "Działa";
                MySqlCommand cmd = new MySqlCommand("SELECT supplier.name, supplier.quantity, cargo.price, cargo.position_x, cargo.position_y, cargo.id FROM `cargo` INNER JOIN supplier ON cargo.supplier_id = supplier.Id WHERE 1", connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    db_cargo.Add(new Cargo() { id = Int16.Parse( dataReader["id"].ToString()), name = dataReader["name"].ToString(), quantity = Int16.Parse(dataReader["quantity"].ToString()), position_x = dataReader["position_x"].ToString(), position_y = dataReader["position_y"].ToString(), price = dataReader["price"].ToString() });
                }

                dataReader.Close();

                connection.Close();

            }
            else
            {
                ViewBag.Message = "nie Działa";
            }

            return View("Storage", db_cargo);
        }


        public ActionResult SaveChange(string str)
        {
            char pattern = '|';
            var content = str.Split(pattern);
            connection.Open();
            if (connection.State == System.Data.ConnectionState.Open)
            {              
                MySqlCommand cmd = new MySqlCommand($@"UPDATE `cargo` SET `price`={content[0]} WHERE id={content[1]}", connection);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            else
            {
                ViewBag.Message = "nie Działa";
            }

            return View("Storage", db_cargo);
        }
    }
}
