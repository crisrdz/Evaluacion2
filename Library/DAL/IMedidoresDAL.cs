using Library.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL
{
    public interface IMedidoresDAL
    {
        List<Medidor> ObtenerMedidores();
    }
}
