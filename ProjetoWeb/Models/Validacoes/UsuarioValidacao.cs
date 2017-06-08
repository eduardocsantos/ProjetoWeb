using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjetoWeb.Models
{
    [MetadataType(typeof(UsuarioValidacao))]
    public partial class Usuario
    {
        
    }

    public class UsuarioValidacao
    {
        [Display(Name = "Grupo")]
        public Nullable<int> IdGrupo { get; set; }

        [Display(Name = "Nome Completo")]
        [Required]
        [StringLength(150, ErrorMessage = "Nome não pode ser maior que 150")]
        [MinLength(3, ErrorMessage = "Nome deve conter mais de 3 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Email é obrigatório")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email inválido")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}