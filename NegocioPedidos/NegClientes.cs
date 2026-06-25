using DataPedidos;
using DataPedidos.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegocioPedidos
{
    public class NegClientes
    {
        DatClientes datos = new DatClientes();

        // Obtener todos los clientes
        public List<Clientes> Obtener()
        {
            return datos.Obtener();
        }

        // Obtener un cliente por id
        public Clientes Obtener(int id)
        {
            return datos.Obtener(id);
        }

        // Agregar un cliente
        public string Agregar(Clientes cliente)
        {
            string errores = ValidarCliente(cliente);
            if (!string.IsNullOrEmpty(errores))
            {
                throw new ArgumentException(errores);
            }

            int filas = datos.Agregar(cliente);
            if(filas != 1)
            {
                throw new Exception("Error al agregar el cliente");
            }
            return $"Se agrego correctamente el cliente: {cliente.Nombre}";
        }

        // Editar un cliente
        public string Editar(Clientes cliente)
        {
            string errores = ValidarCliente(cliente);
            if (!string.IsNullOrEmpty(errores))
            {
                throw new ArgumentException(errores);
            }

            int filas = datos.Editar(cliente);
            if (filas != 1 && filas != 0)
            {
                throw new Exception("Error al editar el cliente");
            }
            return $"Se edito correctamente el cliente {cliente.Nombre}";
        }

        // Buscar un cliente
        public List<Clientes> Buscar(string busqueda)
        {
            if (string.IsNullOrEmpty(busqueda))
            {
                return Obtener();
            }
            return datos.Buscar(busqueda);
        }

        // Metodo auxiliar para validar que el cliente no tenga nulos
        public string ValidarCliente(Clientes cliente)
        {
            string errores = "";
            if (string.IsNullOrEmpty(cliente.Nombre))
            {
                errores += "El nombre del cliente es obligatorio.<br />";
            }
            if (string.IsNullOrEmpty(cliente.Ciudad))
            {
                errores += "La ciudad del cliente es obligatoria.<br />";
            }
            if (!cliente.Estatus.HasValue)
            {
                errores += "El nombre del cliente es obligatorio.<br />";
            }
            return errores;
        }
    }
}
