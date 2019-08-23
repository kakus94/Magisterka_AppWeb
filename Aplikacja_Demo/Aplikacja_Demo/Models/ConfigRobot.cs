using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aplikacja_Demo.Models
{
    public class ConfigRobot
    {
        public string speed_operation { get; set; }
        public string speed_home { get; set; }
        public string pid_kp { get; set; }
        public string pid_ki { get; set; }
        public string pid_kd { get; set; }
        public string robot_name { get; set; }
        public string alarm_voltage { get; set; }
        public string finish_charge { get; set; }
    }

}