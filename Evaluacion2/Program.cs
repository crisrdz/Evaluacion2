using Evaluacion2.Comunicacion;
using Library.DAL;
using Library.DTO;
using ServerSocketUtils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Evaluacion2
{
    class Program
    {
        private static ILecturasDAL lecturasDAL = LecturasDALArchivos.GetInstancia();
        private static IMedidoresDAL medidoresDAL = MedidoresDAL.GetInstancia();
        static bool Menu()
        {
            bool continuar = true;
            Console.WriteLine("Seleccione una opción: " +
                              "\n1. Ver lecturas ingresadas" +
                              "\n2. Ver medidores" +
                              "\n0. Salir");

            switch (Console.ReadLine().Trim())
            {
                case "1":
                    VerLecturas();
                    break;
                case "2":
                    VerMedidores();
                    break;
                case "0":
                    continuar = false;
                    break;
                default:
                    Console.WriteLine("Escriba una opción válida");
                    break;
            }
            return continuar;
        }
        static void Main(string[] args)
        {
            HebraServidor hebra = new HebraServidor();
            Thread t = new Thread(new ThreadStart(hebra.Ejecutar));
            t.IsBackground = true;
            t.Start();

            while (Menu());
        }

        static void VerLecturas()
        {
            List<Lectura> lecturas = null;
            lock (lecturasDAL)
            {
                lecturas = lecturasDAL.ObtenerLecturas();
            }
            Console.WriteLine("\nLecturas ingresadas:");
            foreach(Lectura lectura in lecturas)
            {
                Console.WriteLine(lectura);
            }
            Console.WriteLine("");
        }

        static void VerMedidores()
        {
            List<Medidor> medidores = null;
            lock (medidoresDAL)
            {
                medidores = medidoresDAL.ObtenerMedidores();
            }
            Console.WriteLine("\nMedidores en sistema:");
            foreach (Medidor medidor in medidores)
            {
                Console.WriteLine(medidor);
            }
            Console.WriteLine("");
        }
    }
}
