
using Library.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL
{
    public class MedidoresDAL : IMedidoresDAL
    {
        //Singleton
        private MedidoresDAL()
        {
        }

        private static MedidoresDAL instancia;

        public static IMedidoresDAL GetInstancia()
        {
            if (instancia == null)
            {
                instancia = new MedidoresDAL();
            }

            return instancia;
        }

        private static List<Medidor> medidores = new List<Medidor>();

        public void LlenarLista()
        {
            for (int i = 1; i < 6; i++)
            {
                uint id = (uint)(i + 1000);
                string nombre = "Medidor " + i;
                Medidor medidor = new Medidor() { Id = id, Nombre = nombre };
                medidores.Add(medidor);
            }
        }
        public List<Medidor> ObtenerMedidores()
        {
            LlenarLista();
            return medidores;
        }
    }
}
