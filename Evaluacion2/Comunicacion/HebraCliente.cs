using Library.DAL;
using Library.DTO;
using ServerSocketUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evaluacion2.Comunicacion
{
    public class HebraCliente
    {
        private static ILecturasDAL lecturasDAL = LecturasDALArchivos.GetInstancia();
        private static IMedidoresDAL medidoresDAL = MedidoresDAL.GetInstancia();
        private static List<Medidor> listaMedidores;
        
        private ClienteCom clienteCom;
        public HebraCliente(ClienteCom clienteCom)
        {
            this.clienteCom = clienteCom;
        }

        public void Ejecutar()
        {
            lock (medidoresDAL)
            {
                listaMedidores = medidoresDAL.ObtenerMedidores();
            }
            bool continuar = false;
            uint medidor = 0;
            double consumo = 0;

            while (continuar == false)
            {
                clienteCom.Escribir("Ingrese medidor: ");
                string medidorString = clienteCom.Leer();

                try
                {
                    medidor = Convert.ToUInt32(medidorString.Trim());
                    foreach (Medidor medidorLista in listaMedidores)
                    {
                        if (medidor == medidorLista.Id)
                        {
                            continuar = true;
                        }
                    }
                    if (continuar == false)
                    {
                        clienteCom.Escribir("ERROR: medidor no se encuentra en sistema");
                    }
                }
                catch (Exception ex)
                {
                    clienteCom.Escribir("Ingrese un valor numérico válido");
                }
            }
            continuar = false;
            while (continuar == false)
            {
                clienteCom.Escribir("Ingrese consumo del medidor (con , decimal): ");
                string consumoString = clienteCom.Leer();
                try
                {
                    consumo = Convert.ToDouble(consumoString.Trim());
                    continuar = true;

                }
                catch (Exception ex)
                {
                    clienteCom.Escribir("Ingrese un valor decimal válido");
                }
            }

            DateTime fechaMedicion = DateTime.Now;
            Lectura lectura = new Lectura() { IdMedidor = medidor, FechaMedicion = fechaMedicion, Consumo = consumo };

            lock (lecturasDAL)
            {
                lecturasDAL.IngresarLectura(lectura);
            }
            clienteCom.Escribir("Lectura ingresada correctamente en sistema");
            Console.WriteLine("Nueva lectura ingresada en medidor: " + medidor + ", consumo: " + consumo + ", con fecha: " + fechaMedicion);

            clienteCom.Desconectar();
        }
    }
}
