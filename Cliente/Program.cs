using Cliente.Comunicacion;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliente
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool reintentar = false, continuar = false, ingreso = false;
            do
            {
                int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["puerto"]);
                string servidor = Convert.ToString(ConfigurationManager.AppSettings["servidor"]);

                ClienteSocket clienteSocket = new ClienteSocket(servidor, puerto);
                if (clienteSocket.Conectar())
                {
                    Console.WriteLine("Conectado a servidor 127.0.0.1 en puerto {0}", puerto);

                    Comunicacion(clienteSocket);
                    ingreso = true;
                }
                else
                {
                    Console.WriteLine("Error de comunicación");
                }

                if (ingreso)
                {
                    Console.WriteLine("¿Desea volver a conectarse al servidor para el ingreso de una nueva lectura? s/n");
                }
                else
                {
                    Console.WriteLine("¿Desea volver a intentar a conectarse al servidor? s/n");
                }
                
                do
                {
                    switch (Console.ReadLine())
                    {
                        case "s": reintentar = true; continuar = true; break;
                        case "n": reintentar = false; continuar = true; break;
                        default: Console.WriteLine("Ingrese una opción válida\nIngrese 's' o 'n'"); break;
                    }
                }while(!continuar);

            } while(reintentar);

            
        }

        public static void Comunicacion(ClienteSocket cliente)
        {
            while (true)
            {
                string mensajeServidor = cliente.Leer();
                Console.WriteLine(mensajeServidor);
                if(mensajeServidor == "ERROR: medidor no se encuentra en sistema" || 
                    mensajeServidor == "Ingrese un valor numérico válido" ||
                    mensajeServidor == "Ingrese un valor decimal válido")
                {
                    mensajeServidor = cliente.Leer();
                    Console.WriteLine(mensajeServidor);
                }else if(mensajeServidor == "Lectura ingresada correctamente en sistema")
                {
                    break;
                }

                string respuestaCliente = Console.ReadLine().Trim();
                cliente.Escribir(respuestaCliente);

            }

        }
    }
}
