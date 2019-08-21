using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Aplikacja_Demo.Models.Warehouse;

namespace Aplikacja_Demo.Controllers
{
    public class WarehouseController : Controller
    {
        private List<Supplier> db_suppliers = new List<Supplier>();
        public MySqlConnection connection = new MySqlConnection("datasource=\"localhost\";port=3306;database=magazyn;username=Admin;password=Magisterka");

        // GET: Warehouse
        public ActionResult Index()
        {
            List<string> result = new List<string>();
            connection.Open();
            if (connection.State == System.Data.ConnectionState.Open)
            {
                ViewBag.Message = "Działa";
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM `supplier`; ", connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    db_suppliers.Add(new Supplier() { id = Int32.Parse(dataReader["id"].ToString()), name = dataReader["name"].ToString(), quantity = Int16.Parse(dataReader["quantity"].ToString()), rfid = dataReader["rfid"].ToString(), who = dataReader["Who_addition"].ToString(), disctription = dataReader["disctription"].ToString() });
                }

                dataReader.Close();

                connection.Close();

            }
            else
            {
                ViewBag.Message = "nie Działa";
            }

            return View("Warehouse", db_suppliers);
        }

        // GET: Warehouse/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Warehouse/Create
        [HttpGet]
        public ActionResult Create()
        {

            return PartialView();
        }

        // POST: Warehouse/Create
        [HttpPost]
        public ActionResult Create(Supplier supplier)
        {
            try
            {
                // TODO: Add insert logic here
                connection.Open();
                MySqlCommand cmd = new MySqlCommand($@"INSERT INTO `supplier`( `rfid`, `name`, `quantity`, `Who_addition`,`disctription`) VALUES (""{ supplier.rfid }"" , ""{ supplier.name}"" , { supplier.quantity} , ""{ supplier.who}"" ,"" {supplier.disctription}"")" , connection);
                cmd.ExecuteNonQuery();
                connection.Close();

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View();
            }
        }

        // GET: Warehouse/Edit/5
        public ActionResult Edit(int id)
        {
            Supplier supplier = null;
            connection.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT  `rfid`, `name`, `quantity`, `Who_addition`, `disctription` FROM `supplier` WHERE id=" + id, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                 supplier = new Supplier() { name = dataReader["name"].ToString(), quantity = Int16.Parse(dataReader["quantity"].ToString()), rfid = dataReader["rfid"].ToString(), who = dataReader["Who_addition"].ToString(), disctription= dataReader["disctription"].ToString() };
            }
            dataReader.Close();
            connection.Close();
            return PartialView(supplier);
        }

        // POST: Warehouse/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Supplier supplier)
        {
            try
            {
                connection.Open();
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    ViewBag.Message = "Działa";
                    MySqlCommand cmd = new MySqlCommand($@"UPDATE `supplier` SET `rfid`=""{supplier.rfid}"" ,`name`=""{supplier.name}"",`quantity`={supplier.quantity},`Who_addition`=""{supplier.who}"", `disctription`=""{supplier.disctription}"" WHERE id={id} ", connection);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        db_suppliers.Add(new Supplier() { id = Int32.Parse(dataReader["id"].ToString()), name = dataReader["name"].ToString(), quantity = Int16.Parse(dataReader["quantity"].ToString()), rfid = dataReader["rfid"].ToString(), who = dataReader["Who_addition"].ToString(), disctription= dataReader["disctription"].ToString() });
                    }

                    dataReader.Close();

                    connection.Close();

                }
                else
                {
                    ViewBag.Message = "nie Działa";
                }


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Warehouse/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: Warehouse/Delete/5
        [HttpGet]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("DELETE FROM `supplier` WHERE id="+id, connection);
                cmd.ExecuteNonQuery();
                connection.Close();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Search(string temp)
        {
            char pattern = ',';
            var content = temp.Split(pattern);
            connection.Open();
            if (connection.State == System.Data.ConnectionState.Open)
            {
                ViewBag.Message = "Działa";
                MySqlCommand cmd = new MySqlCommand($@"SELECT * FROM `supplier` WHERE ({content[0]} LIKE ""%{content[1]}%""); ", connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    db_suppliers.Add(new Supplier() { id = Int32.Parse(dataReader["id"].ToString()), name = dataReader["name"].ToString(), quantity = Int16.Parse(dataReader["quantity"].ToString()), rfid = dataReader["rfid"].ToString(), who = dataReader["Who_addition"].ToString(), disctription = dataReader["disctription"].ToString() });
                }

                dataReader.Close();

                connection.Close();
            }
                return View("Warehouse",db_suppliers);
        }

    }
}
