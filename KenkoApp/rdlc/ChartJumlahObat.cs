using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KenkoApp.rdlc
{
    public class ChartJumlahObat
    {
        public List<int> jumlah{ get; set; }
        public List<string> bulan { get; set; }

        public ChartJumlahObat() 
        {
            jumlah = new List<int>();
            bulan = new List<string>();
        }
    }
}
