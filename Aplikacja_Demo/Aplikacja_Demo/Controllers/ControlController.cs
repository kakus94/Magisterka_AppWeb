using Aplikacja_Demo.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

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
            Robot robot = new Robot();
            ConfigRobot configRobot = null;
            StatusRobot statusRobot = null;

            connection.Open();
            if (connection.State == System.Data.ConnectionState.Open)
            {
                ViewBag.Message = "Działa";
                MySqlCommand cmd = new MySqlCommand("SELECT battery,battery_start,localization,temperature,time_to_Start,date FROM magazyn.status where id = (select max(id)from magazyn.status) ;  ", connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    statusRobot = new StatusRobot() { BateryStart = dataReader["battery_start"].ToString(), battery = dataReader["battery"].ToString(), localization = dataReader["localization"].ToString(), temperature = dataReader["temperature"].ToString(),timeToStart = dataReader["time_to_Start"].ToString() };
                }
                dataReader.Close();

                cmd = new MySqlCommand("SELECT * FROM magazyn.config where id = (select max(id)from magazyn.config) ;  ", connection);
                 dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    configRobot = new ConfigRobot() { alarm_voltage = dataReader["alarm_voltage"].ToString(),finish_charge = dataReader["finish_charge"].ToString(),pid_kd = dataReader["pid_kd"].ToString(),pid_ki = dataReader["pid_ki"].ToString(), pid_kp = dataReader["pid_kp"].ToString(),robot_name = dataReader["robot_name"].ToString(),speed_home = dataReader["speed_home"].ToString(), speed_operation = dataReader["speed_operation"].ToString() };
                }
                dataReader.Close();

                connection.Close();

                robot.statusRobot = statusRobot;
                robot.configRobot = configRobot;
            }
            else
            {
                ViewBag.Message = "nie Działa";
            }            
         
            return View("Index",robot);
        }


        [HttpGet]
        public ActionResult SendConfig(ConfigRobot fc)
        {

            string dateTime = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fffffff");
            StatusRobot statusRobot = null;

            string cmdString = $@"INSERT INTO `magazyn`.`config` (`speed_operation`, `speed_home`, `pid_kp`, `pid_ki`, `pid_kd`, `robot_name`, `alarm_voltage`, `finish_charge`, `date`) 
VALUES ('{fc.speed_operation}', '{fc.speed_home}', '{fc.pid_kp}', '{fc.pid_ki}', '{fc.pid_kd}', '{fc.robot_name}', '{fc.alarm_voltage}', '{fc.finish_charge}', '{dateTime}');";

            connection.Open();
            MySqlCommand cmd = new MySqlCommand(cmdString, connection);
            cmd.ExecuteNonQuery();     

            cmd = new MySqlCommand("SELECT battery,battery_start,localization,temperature,time_to_Start,date FROM magazyn.status where id = (select max(id)from magazyn.status) ;  ", connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                statusRobot = new StatusRobot() { BateryStart = dataReader["battery_start"].ToString(), battery = dataReader["battery"].ToString(), localization = dataReader["localization"].ToString(), temperature = dataReader["temperature"].ToString(), timeToStart = dataReader["time_to_Start"].ToString() };
            }
            dataReader.Close();

            Robot robot = new Robot();
            robot.configRobot = fc;
            robot.statusRobot = statusRobot;

            return View("Index",robot);
        }

        [HttpGet]
        public string GetStatus(string str)
        {

            MySqlCommand cmd = new MySqlCommand("SELECT battery,battery_start,localization,temperature,time_to_Start,date FROM magazyn.status where id = (select max(id)from magazyn.status) ;  ", connection);
            connection.Open();
            MySqlDataReader dataReader = cmd.ExecuteReader();
            StatusRobot statusRobot = new StatusRobot();

            while (dataReader.Read())
            {
                statusRobot = new StatusRobot() { BateryStart = dataReader["battery_start"].ToString(), battery = dataReader["battery"].ToString(), localization = dataReader["localization"].ToString(), temperature = dataReader["temperature"].ToString(), timeToStart = dataReader["time_to_Start"].ToString() };
            }
            dataReader.Close();


            var json = new JavaScriptSerializer().Serialize(statusRobot);

            return json;
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