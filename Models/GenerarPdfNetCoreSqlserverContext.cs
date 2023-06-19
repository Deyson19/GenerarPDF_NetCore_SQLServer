using System;
using System.Collections.Generic;
using GenerarPDF_NetCore_SQLServer.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace GenerarPDF_NetCore_SQLServer.Models;

public partial class GenerarPdfNetCoreSqlserverContext : DbContext
{
    public GenerarPdfNetCoreSqlserverContext()
    {
    }

    public GenerarPdfNetCoreSqlserverContext(DbContextOptions<GenerarPdfNetCoreSqlserverContext> options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Venta>()
            .HasMany(v => v.DetalleVenta)
            .WithOne()
            .HasForeignKey(dv => dv.IdVenta);
    }


    public virtual DbSet<DetalleVenta> DetalleVentas { get; set; }

    public virtual DbSet<Venta> Ventas { get; set; }

    
}
