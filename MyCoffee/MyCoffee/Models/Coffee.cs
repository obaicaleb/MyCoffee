using System;
using System.Collections.Generic;
using System.Text;

namespace MyCoffee.Models
{
    public class Coffee
    {
        /// <summary>
        /// [PrimaryKey, AutoIncrement]
        /// </summary>
        public int Id { get; set; }
        public string Roaster { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
    }
}
