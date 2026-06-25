using DataPedidos.Model;
using NegocioPedidos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebPedidosEF.Controllers
{
    public class HomeController : Controller
    {
        NegPedidos negP = new NegPedidos();
        NegClientes negC = new NegClientes();

        // GET: Home
        public ActionResult Index()
        {
            List<Pedidos> ls = new List<Pedidos>();
            try
            {
                ls = negP.Obtener();
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }
            return View("Index", ls);
        }

        public ActionResult Create()
        {
            try
            {
                Pedidos pedido = new Pedidos();
                List<Clientes> ls = negC.Obtener();
                ViewBag.ClientesId = new SelectList(ls, "ClientesId", "Nombre");
                return View("Create", pedido);
            } catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        public ActionResult CreatePost(Pedidos p)
        {
            try
            {
                TempData["success"] = negP.Agregar(p);
                return RedirectToAction("Index");
            }
            catch (ArgumentException ex)
            {
                TempData["error"] = ex.Message;
                List<Clientes> ls = negC.Obtener();
                ViewBag.ClientesId = new SelectList(ls, "ClientesId", "Nombre", p.ClientesId);
                return View("Create", p);
            }
            catch (Exception)
            {
                TempData["error"] = "Ocurrio un error inesperado.";
                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(int id)
        {
            try
            {
                Pedidos pedido = negP.ObtenerPorId(id);
                List<Clientes> ls = negC.Obtener();
                ViewBag.ClientesId = new SelectList(ls, "ClientesId", "Nombre", pedido.ClientesId);
                return View("Edit", pedido);
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        public ActionResult EditPost(Pedidos pedido)
        {
            try
            {
                TempData["success"] = negP.Editar(pedido);
                return RedirectToAction("Index");
            }
            catch (ArgumentException ex)
            {
                TempData["error"] = ex.Message;
                List<Clientes> ls = negC.Obtener();
                ViewBag.ClientesId = new SelectList(ls, "ClientesId", "Nombre", pedido.ClientesId);
                return View("Edit", pedido);
            }
            catch (Exception)
            {
                TempData["error"] = "Ocurrio un error inesperado.";
                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                Pedidos pedido = negP.ObtenerPorId(id);
                return View("Delete", pedido);
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        public ActionResult DeletePost(int id)
        {
            try
            {
                TempData["success"] = negP.Eliminar(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                Pedidos pedido = negP.ObtenerPorId(id);
                return View("Delete", pedido);
            }
        }

        public ActionResult Buscar(string busqueda)
        {
            List<Pedidos> pedidos = new List<Pedidos>();
            try
            {
                pedidos = negP.Buscar(busqueda);
                TempData["busqueda"] = busqueda;
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }
            return View("Index", pedidos);
        }

        public ActionResult IrReporte()
        {
            try
            {
                List<Pedidos> listaPedidos = negP.Obtener();
                return View("Reporte", listaPedidos);
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return View("Reporte", new List<Pedidos>());
            }
        }

        public ActionResult Reporte(DateTime? fechaInicial, DateTime? fechaFinal)
        {
            try
            {
                List<Pedidos> listaPedidos = new List<Pedidos>();
                listaPedidos = negP.Reporte(fechaInicial, fechaFinal);

                ViewBag.FechaInicial = fechaInicial?.ToString("yyyy-MM-dd") ?? "";
                ViewBag.FechaFinal = fechaFinal?.ToString("yyyy-MM-dd") ?? "";

                return View("Reporte", listaPedidos);
            }
            catch(ArgumentException ex) // Error de validacion
            {
                TempData["error"] = ex.Message;
                ViewBag.FechaInicial = fechaInicial?.ToString("yyyy-MM-dd") ?? "";
                ViewBag.FechaFinal = fechaFinal?.ToString("yyyy-MM-dd") ?? "";
                return View("Reporte", new List<Pedidos>());
            }
            catch (Exception ex) // Error general
            {
                TempData["error"] = ex.Message;
                ViewBag.FechaInicial = fechaInicial?.ToString("yyyy-MM-dd") ?? "";
                ViewBag.FechaFinal = fechaFinal?.ToString("yyyy-MM-dd") ?? "";
                return View("Reporte", new List<Pedidos>());
            }
        }
    }
}