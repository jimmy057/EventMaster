using EventMaster.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EventMaster.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
	: IdentityDbContext<ApplicationUser>(options)
{
	public DbSet<Clientes> Clientes { get; set; }
	public DbSet<Administradores> Administradores { get; set; }
	public DbSet<Eventos> Eventos { get; set; }
	public DbSet<EventosDetalle> EventosDetalle { get; set; }
	public DbSet<Actividades> Actividades { get; set; }
}
