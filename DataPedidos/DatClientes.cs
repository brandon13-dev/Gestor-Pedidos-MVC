using DataPedidos.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataPedidos
{
    public class DatClientes
    {
        private generacion38Entities _db = new generacion38Entities();

        // Obtener todos los clientes ordenados por nombre
        public List<Clientes> Obtener()
        {
            List<Clientes> ls = _db.Clientes.OrderBy(x => x.Nombre).ToList();
            return ls;
        }

        // Obtener un cliente por id
        public Clientes Obtener(int id)
        {
            Clientes cliente = new Clientes();
            cliente = _db.Clientes.Where(x => x.ClientesId == id).FirstOrDefault();
            return cliente;
        }

        // Agregar un cliente
        public int Agregar(Clientes cliente)
        {
            try
            {
                _db.Clientes.Add(cliente);
                int filas = _db.SaveChanges();
                return filas;

            } 
            catch
            {
                throw;
            }
            finally
            {
                _db.Dispose();
            }
        }

        // Editar un cliente
        public int Editar(Clientes cliente)
        {
            try
            {
                _db.Clientes.AddOrUpdate(cliente);
                int filas = _db.SaveChanges();
                return filas;
            }
            catch
            {
                throw;
            }
            finally
            {
                _db.Dispose();
            }
        }


        // Buscar un cliente
        public List<Clientes> Buscar(string busqueda)
        {
            List<Clientes> lsClientes = new List<Clientes>();

            lsClientes = _db.Clientes
                .Where(x => x.Nombre.Contains(busqueda) || 
                            x.Ciudad.Contains(busqueda))
                .ToList();

            return lsClientes;
        }
    }
}
