using GenerarPDF_NetCore_SQLServer.Models;
using GenerarPDF_NetCore_SQLServer.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Utf8Json.Formatters;

namespace GenerarPDF_NetCore_SQLServer.Controllers
{
    public class HomeController : Controller
    {
        private readonly GenerarPdfNetCoreSqlserverContext _context;

        public HomeController(GenerarPdfNetCoreSqlserverContext generarPdfNetCoreSqlserverContext)
        {
            _context = generarPdfNetCoreSqlserverContext;
        }

        public IActionResult Ventas()
        {
            List<Venta> modelos = _context.Ventas.Include(v => v.DetalleVenta)
            .Select(n => new Venta()
        {
        IdVenta = n.IdVenta,
        NumeroVenta = n.NumeroVenta,
        DocumentoCliente = n.DocumentoCliente,
        NombreCliente = n.NombreCliente,
        SubTotal = n.SubTotal,
        ImpuestoTotal = n.ImpuestoTotal,
        Total = n.Total,
        DetalleVenta = n.DetalleVenta.Select(dv => new DetalleVenta()
        {
            IdVenta = dv.IdVenta,
            NombreProducto = dv.NombreProducto,
            Cantidad = Convert.ToInt32(dv.Cantidad),
            Precio = dv.Precio,
            Total = dv.Total,
        }).ToList()
    }).ToList();

            return View(modelos);
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ImprimirVenta(int idventa)
        {
            if (idventa == null || idventa == 0)
            {
                return RedirectToAction("Error");
            }

            Venta modelo = _context.Ventas.Include(v => v.DetalleVenta).Where(z => z.IdVenta == idventa)
                .Select(n => new Venta()
                {
                    IdVenta = n.IdVenta,
                    NumeroVenta = n.NumeroVenta,
                    DocumentoCliente = n.DocumentoCliente,
                    NombreCliente = n.NombreCliente,
                    SubTotal = n.SubTotal,
                    ImpuestoTotal = n.ImpuestoTotal,
                    Total = n.Total,
                    DetalleVenta = n.DetalleVenta.Select(dv => new DetalleVenta()
                    {
                        NombreProducto = dv.NombreProducto,
                        Cantidad = dv.Cantidad,
                        Precio = dv.Precio,
                        Total = dv.Total,
                    }).ToList()
                }).FirstOrDefault();
            DateTime dateTime = DateTime.Now;
            string formattedDateTime = DateTime.Now.ToString("ddMMyyyy_HHmmss");

            string fileName = $"Venta_{modelo.NumeroVenta}_{formattedDateTime}.pdf";

            // return View();
            return new ViewAsPdf("ImprimirVenta", modelo)
            {

                FileName = fileName,
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                PageSize = Rotativa.AspNetCore.Options.Size.A4

            };
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}