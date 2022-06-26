using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppLoginAutenticacao.ViewModels
{
    public class LoginViewModel
    {

        public string UrlRetorno { get; set; }

        [Required(ErrorMessage = "Informe o Login")]
        [MaxLength(50, ErrorMessage = "O login deve ter no maximo 50 caracteres")]

        public string Login { get; set; }

        [Required(ErrorMessage ="Informe a senha!")]
        [MinLength(6, ErrorMessage ="A senha deve ter no minimo 6 caracteres")]
        [DataType(DataType.Password)]

        public string  Senha { get; set; }
    }
}