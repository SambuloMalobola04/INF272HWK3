using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INF272HWK3.Models
{
    public class HomePViewModel
    {
        public IEnumerable<customers> Customerss { get; set; }
        public IEnumerable<staffs> Staffss { get; set; }
        public IEnumerable<products> Productss { get; set; }
    }
}