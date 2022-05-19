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

namespace Evaluacion2.Comunicacion
{
    public class HebraServidor
    {        
        public void Ejecutar()
        {
            int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["puerto"]);
            ServerSocket serverSocket = new ServerSocket(puerto);
            Console.WriteLine("\nS: Iniciando servidor en puerto {0}", puerto);

            if (serverSocket.Iniciar())
            {
                while (true)
                {
                    Console.WriteLine("S: Esperando clientes...\n");
                    Socket cliente = serverSocket.ObtenerCliente();
                    Console.WriteLine("\nS: Cliente recibido\n");

                    ClienteCom clienteCom = new ClienteCom(cliente);

                    HebraCliente clienteThread = new HebraCliente(clienteCom);
                    Thread thread = new Thread(new ThreadStart(clienteThread.Ejecutar));
                    thread.IsBackground = true;
                    thread.Start();
                }
            }
        }
    }
}
