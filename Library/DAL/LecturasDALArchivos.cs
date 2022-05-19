using Library.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL
{
    public class LecturasDALArchivos : ILecturasDAL
    {
        //Singleton
        private LecturasDALArchivos()
        {
        }

        private static LecturasDALArchivos instancia;

        public static ILecturasDAL GetInstancia()
        {
            if (instancia == null)
            {
                instancia = new LecturasDALArchivos();
            }

            return instancia;
        }

        private static string archivo = "lecturas.txt";
        private static string ruta = Directory.GetCurrentDirectory() + "/" + archivo;
        public void IngresarLectura(Lectura lectura)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(ruta, true))
                {
                    //NOTA: CAMBIAR FECHA POR CLASE FECHA
                    string texto = lectura.IdMedidor + ";" + lectura.FechaMedicion + ";" + lectura.Consumo;
                    writer.WriteLine(texto);
                    writer.Flush();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al escribir en archivo: " + ex.Message);
            }
        }

        public List<Lectura> ObtenerLecturas()
        {
            List<Lectura> lecturas = new List<Lectura>();
            using (StreamReader reader = new StreamReader(ruta))
            {
                string texto;
                do
                {
                    texto = reader.ReadLine();
                    if (texto != null)
                    {
                        string[] textoArr = texto.Trim().Split(';');
                        uint idMedidor = Convert.ToUInt32(textoArr[0]);
                        DateTime fechaMedicion = Convert.ToDateTime(textoArr[1]);
                        double consumo = Convert.ToDouble(textoArr[2]);

                        //2. Crear persona

                        Lectura lectura = new Lectura() { IdMedidor = idMedidor, FechaMedicion = fechaMedicion, Consumo = consumo };

                        //4. Agregar a la lista

                        lecturas.Add(lectura);
                    }


                } while (texto != null);

                return lecturas;

            }
        }
    }
}
