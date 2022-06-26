using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AppLoginAutenticacao.ViewModels
{
    public class AlterarSenhaViewModel
    {
        [Display(Name = "Senha Atual")]
        [Required(ErrorMessage ="Informe a senha atual")]
        [MinLength(6, ErrorMessage = "A senha deve ter no minimo 6 caracteres")]
        [DataType(DataType.Password)]

        public string SenhaAtual { get; set; }

        [Display(Name = "Nova senha")]
        [Required(ErrorMessage = "Informe a nova senha")]
        [MinLength(6, ErrorMessage = "A senha deve ter no minimo 6 caracteres")]
        [DataType(DataType.Password)]

        public string NovaSenha { get; set; }

        [Display(Name = "Confirmar senha")]
        [Required(ErrorMessage = "Confirme a senha")]
        [DataType(DataType.Password)]
        [Compare(nameof(NovaSenha), ErrorMessage = "A senhas são diferentes")]

        public string ConfirmaSenha { get; set; }
    }
}