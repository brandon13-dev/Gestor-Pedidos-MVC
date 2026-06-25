using DataPedidos.Model;
using NegocioPedidos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebPedidosEF.Controllers
{
    public class ClientesController : Controller
    {
        NegClientes negC = new NegClientes();

        // Vista de clientes
        public ActionResult Clientes()
        {
            List<Clientes> clientes = new List<Clientes>();
            try
            {
                clientes = negC.Obtener();
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message; 
            }
            
            return View("Clientes", clientes);
        }

        // Crear cliente
        public ActionResult Create()
        {
            try
            {
                Clientes cliente = new Clientes();
                return View("Create", cliente);
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("Clientes");
            }
        }

        // Crear clientepost
        public ActionResult CreatePost(Clientes cliente)
        {
            try
            {
                TempData["success"] = negC.Agregar(cliente);
                return RedirectToAction("Clientes");
            }
            catch (ArgumentException ex)
            {
                TempData["error"] = ex.Message;
                return View("Create", cliente);
            }
            catch (Exception)
            {
                TempData["error"] = "Ocurrio un error inesperado.";
                return RedirectToAction("Clientes");
            }
        }

        // Editar Cliente
        public ActionResult Edit(int id)
        {
            try
            {
                Clientes cliente = new Clientes();
                cliente = negC.Obtener(id);
                return View("Edit", cliente);
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("Clientes");
            }
        }

        // Editar Cliente POST
        public ActionResult EditPost(Clientes cliente)
        {
            try
            {
                TempData["success"] = negC.Editar(cliente);
                return RedirectToAction("Clientes");
            }
            catch (ArgumentException ex)
            {
                TempData["error"] = ex.Message;
                return View("Edit", cliente);
            }
            catch (Exception)
            {
                TempData["error"] = "Ocurrio un error inesperado";
                return View("Clientes");
            }
        }

        // Buscar Cliente
        public ActionResult Buscar(string busqueda)
        {
            List<Clientes> clientes = new List<Clientes>();
            try
            {
                clientes = negC.Buscar(busqueda);
                TempData["busqueda"] = busqueda;
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }
            return View("Clientes", clientes);
        }
    }
}