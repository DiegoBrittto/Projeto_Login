using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Linq;
using System.Web;
using CompareAttribute = System.ComponentModel.DataAnnotations.CompareAttribute;

namespace AppLoginAutenticacao.ViewModels
{
    public class CadastroUsuarioViewModel
    {
        [Display (Name = "Nome")]
        [Required(ErrorMessage ="Informe o seu nome!")]
        [MaxLength(100,ErrorMessage = "O nome deve ter até 100 caracteres")]
        public string UsuNome { get; set; }

        [Required(ErrorMessage = "Informe o login!")]
        [MaxLength(50, ErrorMessage = "O login deve ter até 50 caracteres")]
        [Remote("SelectLogin","Autenticacao",ErrorMessage = "O Login já existe!")]
        public string Login { get; set; }
 
        [Required(ErrorMessage ="Informe a Senha!")]
        [MinLength(6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres")]
        [MaxLength(100, ErrorMessage = "A senha deve ter no máximo 100 caracteres")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }


        [Display(Name = "Confirmar Senha")]
        [Required(ErrorMessage = "Informe a Senha!")]
        [DataType(DataType.Password)]
        [Compare(nameof(Senha), ErrorMessage = "As senhas são diferentes!")]
        public string ConfirmarSenha { get; set; }

    }
}