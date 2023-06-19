using GenerarPDF_NetCore_SQLServer.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GenerarPDF_NetCore_SQLServer.Models;

public partial class Venta
{
    public Venta()
    {
        DetalleVenta = new HashSet<DetalleVenta>();
    }

    [Key]
    public int IdVenta { get; set; }

    public string NumeroVenta { get; set; } = null!;

    public string DocumentoCliente { get; set; } = null!;

    public string NombreCliente { get; set; } = null!;

    public string SubTotal { get; set; } = null!;

    public string ImpuestoTotal { get; set; } = null!;

    public string Total { get; set; } = null!;
    public virtual ICollection<DetalleVenta> DetalleVenta { get; set; }

}


