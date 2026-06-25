using DataPedidos;
using DataPedidos.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace NegocioPedidos
{
    public class NegPedidos
    {
        DatPedidos datos = new DatPedidos();
        DatClientes datosCliente = new DatClientes();

        public List<Pedidos> Obtener()
        {
            return datos.Obtener();
        }

        public Pedidos ObtenerPorId(int id)
        {
            return datos.ObtenerPorId(id);
        }

        public string Agregar(Pedidos pedido)
        {
            // Validamos con el metodo auxiliar el pedido
            string errores = ValidarPedido(pedido);
            if (!string.IsNullOrEmpty(errores))
            {
                throw new ArgumentException(errores);
            }

            int filas = datos.Agregar(pedido);
            if (filas != 1)
            {
                throw new Exception("Error al agregar el pedido.");
            }
            return $"Se agrego correctamente el pedido con fecha {pedido.Fecha?.ToShortDateString() ?? "sin fecha"}";
        }

        public string Editar(Pedidos pedido)
        {
            string errores = ValidarPedido(pedido);
            if (!string.IsNullOrEmpty(errores))
            {
                throw new ArgumentException(errores);
            }

            int filasAfectadas = datos.Editar(pedido);
            if (filasAfectadas != 1 && filasAfectadas != 0)
            {
                throw new Exception("Error al editar el pedido");
            }
            var cliente = datosCliente.Obtener((int)pedido.ClientesId);
            string nombreCliente = cliente?.Nombre ?? "cliente desconocido";
            return $"Se edito correctamente el pedido del cliente {nombreCliente}";
        }

        public string Eliminar(int id)
        {
            int filasAfectadas = datos.Eliminar(id);
            if (filasAfectadas != 1)
            {
                throw new Exception("Error al eliminar el pedido.");
            }
            return $"Se elimino correctamente el pedido con id {id}";
        }

        public List<Pedidos> Buscar(string valor)
        {
            if (string.IsNullOrEmpty(valor))
            {
                return Obtener();
            }
            return datos.Buscar(valor);
        }

        public List<Pedidos> Reporte(DateTime? fechaInicial, DateTime? fechaFinal)
        {
            if (!fechaInicial.HasValue || !fechaFinal.HasValue)
            {
                throw new ArgumentException("Debe de proporcionar ambas fechas");
            }
            if (fechaInicial > fechaFinal)
            {
                throw new ArgumentException("La fecha inicial no puede ser mayor a la final");
            }

            return datos.Reporte(fechaInicial.Value, fechaFinal.Value);
        }

        // Metodo auxiliar para validar que el pedido no tenga nulos
        public string ValidarPedido(Pedidos pedido)
        {
            string errores = "";
            if (!pedido.Fecha.HasValue)
            {
                errores += "La fecha del pedido es obligatoria.<br />";
            } else if (pedido.Fecha.Value <= DateTime.Today)
            {
                errores += "La fecha del pedido debe ser mayor a hoy. <br />";
            }
            if (!pedido.Total.HasValue || pedido.Total <= 0)
            {
                errores += "El total del pedido debe de ser mayor a $0.00<br />";
            }
            if (string.IsNullOrEmpty(pedido.Descripcion))
            {
                errores += "La descripcion del pedido es obligatoria.<br />";
            }
            if (!pedido.ClientesId.HasValue)
            {
                errores += "El cliente del pedido es obligatorio";
            }
            return errores;
        }
    }
}
