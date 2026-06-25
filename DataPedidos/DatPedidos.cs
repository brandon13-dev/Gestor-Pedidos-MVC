using DataPedidos.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataPedidos
{
    public class DatPedidos
    {

        private generacion38Entities _db = new generacion38Entities();

        public List<Pedidos> Obtener()
        {
            List<Pedidos> ls = new List<Pedidos>();

            ls = _db.Pedidos.Include("Clientes").ToList(); 

            return ls;
        }

        public Pedidos ObtenerPorId(int id)
        {
            Pedidos p = new Pedidos();

            p = _db.Pedidos.Where(x => x.PedidoId == id).FirstOrDefault();

            return p;
        }

        public int Agregar(Pedidos p)
        {
            try
            {
                _db.Pedidos.Add(p);
                int filas = _db.SaveChanges();
                return filas;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _db.Dispose();
            }
        }

        public int Editar(Pedidos p)
        {
            try
            {
                _db.Pedidos.AddOrUpdate(p);
                int filas = _db.SaveChanges();
                return filas;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _db.Dispose();
            }
        }

        public int Eliminar(int id)
        {
            try
            {
                Pedidos p = ObtenerPorId(id);
                _db.Pedidos.Remove(p);
                return _db.SaveChanges();
            }
            catch(Exception)
            {
                throw;
            }
            finally
            {
                _db.Dispose();
            }
        }
        
        public List<Pedidos> Buscar(string valor)
        {
            List<Pedidos> ls = new List<Pedidos>();

            ls = _db.Pedidos.Include("Clientes")
                .Where(x => x.Total.ToString() == valor || 
                            x.Descripcion.Contains(valor)||
                            x.Clientes.Nombre.Contains(valor))
                .ToList();

            return ls;
        }

        public List<Pedidos> Reporte(DateTime fechaInicial, DateTime fechaFinal)
        {
            try
            {
                List<Pedidos> ls = new List<Pedidos>();
                ls = _db.Pedidos
                        .Include("Clientes")
                        .Where(pedido => pedido.Fecha >= fechaInicial && pedido.Fecha <= fechaFinal)
                        .ToList();

                return ls;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _db.Dispose();
            }
        }
    }
}
