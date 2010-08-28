using System.ComponentModel.DataAnnotations;

namespace BlogTestesControllerI
{
    public class UsuarioCriar
    {
        [Required(AllowEmptyStrings = false)]
        public string Nome { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Login { get; set; }
        
        [Required(AllowEmptyStrings = false)]
        public string Senha { get; set; }
    }
}