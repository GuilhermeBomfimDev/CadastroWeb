using System.ComponentModel.DataAnnotations;

namespace CadastroBackAPI.Models
{
    public class UserDados
    {
        [Key]
        public int UserId { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Telefone { get; set; }
    }
}
