using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Aplikacja_Demo.Controllers
{
    public class ControlController : Controller
    {
        public MySqlConnection connection = new MySqlConnection("datasource=\"localhost\";port=3306;database=magazyn;username=Admin;password=Magisterka");

        enum eCMD{
            CMD_Stop=1,CMD_Start,CMD_Status,CMD_Config,CMD_GetData
        }


        // GET: Control
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendOrder(string str)
        {
            int CMD_code;
            CMD_code = Int16.Parse(str);
            string  cmd_str = null;

            switch (CMD_code)
            { 
                case (int)eCMD.CMD_Start:
                    cmd_str = $@"INSERT INTO `magazyn`.`comandsmode` (`cmd_mode`, `exe`) VALUES ('{CMD_code}', '0');";
                    break;
                case (int)eCMD.CMD_Stop:
                    cmd_str = $@"INSERT INTO `magazyn`.`comandsmode` (`cmd_mode`, `exe`) VALUES ('{CMD_code}', '0');";
                    break;
                case (int)eCMD.CMD_Status:

                    break;
                case (int)eCMD.CMD_Config:

                    break;
                case (int)eCMD.CMD_GetData:
                    cmd_str = $@"INSERT INTO `magazyn`.`comandsmode` (`cmd_mode`, `exe`) VALUES ('{CMD_code}', '0');";
                    break;
            }

            connection.Open();
            if (connection.State == System.Data.ConnectionState.Open)
            {
                MySqlCommand cmd = new MySqlCommand(cmd_str, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
            }


            return RedirectToAction("Index");
        }
    }
}