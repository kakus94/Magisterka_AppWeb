using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aplikacja_Demo.Models
{
    public class StatusRobot
    {
        public string battery { get; set; }
        public string localization { get; set; }
        public string temperature { get; set; }
        public string timeToStart { get; set; }
        public string BateryStart { get; set; }
    }
}