using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DTO
{
    public class Lectura
    {
        private uint idMedidor;
        private DateTime fechaMedicion;
        private double consumo;

        public uint IdMedidor { get => idMedidor; set => idMedidor = value; }
        public DateTime FechaMedicion { get => fechaMedicion; set => fechaMedicion = value; }
        public double Consumo { get => consumo; set => consumo = value; }

        public override string ToString()
        {
            return "ID Medidor: " + idMedidor + " - Fecha: " + fechaMedicion + " - Consumo: " + consumo + " kw/h";
        }
    }
}
