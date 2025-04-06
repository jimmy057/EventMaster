using EventMaster.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventMaster.Models;

public class Clientes
{
    [Key]
    public int ClienteId { get; set; }
    public string UsuarioId { get; set; }
    [ForeignKey("UsuarioId")]
    public ApplicationUser Usuario { get; set; }
	public string Nombre { get; set; }
	public string Apellido { get; set; }
}
