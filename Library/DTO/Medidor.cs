using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DTO
{
    public class Medidor
    {        
        private uint id;
        private string nombre;

        public uint Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }

        public override string ToString()
        {
            return "ID medidor: " + id + " - Nombre: " + nombre;
        }
    }
}
