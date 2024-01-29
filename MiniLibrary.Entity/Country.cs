using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniLibrary.Entity
{
    public class Country
    {
        public string Iso3 { get; set; }
        public string Name { get; set; }
        public List<State> States { get; set; }
    }
}
