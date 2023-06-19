namespace GenerarPDF_NetCore_SQLServer.Models.ViewModels
{
    public class ViewModelVenta
    {
        public string NumeroVenta { get; set; }

        public string DocumentoCliente { get; set; }

        public string NombreCliente { get; set; }

        public string SubTotal { get; set; }

        public string Impuesto { get; set; }

        public string Total { get; set; }

        public List<ViewModelDetalleVenta> detalleVenta { get; set; }
    }

    public class ViewModelDetalleVenta
    {
        public string Producto { get; set; }

        public string Cantidad { get; set; }

        public string Precio { get; set; }

        public string Total { get; set; }
    }
}
