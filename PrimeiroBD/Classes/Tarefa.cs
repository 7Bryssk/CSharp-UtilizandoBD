using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeiroBD.Classes
{
    public class Tarefa
    {
        public int id { get; set; }
        public string titulo { get; set; }
        public string descricao { get; set; }
        public int tempo { get; set; }
        public bool status { get; set; }
    }
}
