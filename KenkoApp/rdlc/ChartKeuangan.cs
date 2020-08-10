using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KenkoApp.rdlc
{
    public class ChartKeuangan
    {
        public List<double> keuangan { get; set; }
        public List<string> bulan { get; set; }

        public ChartKeuangan() 
        {
            keuangan = new List<double>();
            bulan = new List<string>();
        }
    }
}
