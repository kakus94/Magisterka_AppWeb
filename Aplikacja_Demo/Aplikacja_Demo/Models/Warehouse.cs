using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Aplikacja_Demo.Models
{
    public class Warehouse
    {
        public class Supplier
        {
            public int id { get; set; }
            [DisplayName("ilosc")]
            public int quantity { get; set; }
            [DisplayName("Nr RFID")]
            public string rfid { get; set; }
            [DisplayName("Nazwa")]
            public string name { get; set; }
            [DisplayName("Producent")]
            public string who { get; set; }
            [DisplayName("Opis")]
            public string disctription { get; set; }
        }

        public class Cargo
        {
            public int id { get; set; }
            public int supplier_id { get; set; }
            public string name { get; set; }
            public string position_x { get; set; }
            public string position_y { get; set; }
            public string price { get; set; }
            public int quantity { get; set; }
        }
    }
}