using AppLoginAutenticacao.Models;
using AppLoginAutenticacao.ViewModels;
using AppLoginAutenticacao.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Claims;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;

namespace AppLoginAutenticacao.Controllers
{
    public class AutenticacaoController : Controller
    {
        public ActionResult Error(string mensagem)
        {
            ViewBag.error = "NÃO FAÇA NADA, O BANCO ESTÁ INDISPONIVÉL SEU BIZONHO, Mostre essa mensagem para o TI: {" + mensagem + "}";
            return View();
        }
        Usuario usuario = new Usuario();
        [HttpGet]
        public ActionResult Insert()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Insert(CadastroUsuarioViewModel usuview)
        {
            if (!ModelState.IsValid)
            
                return View(usuview);


            Usuario novousuario = new Usuario
            {
                UsuNome = usuview.UsuNome,
                Login = usuview.Login,
                Senha = Hash.GerarHash(usuview.Senha)
            };
            MySqlConnection conexao = new MySqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
            try
            {
                conexao.Open();
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", new { mensagem = e.Message });
            }
            finally
            {
                if (conexao.State == ConnectionState.Open)
                    conexao.Close();
            }


            novousuario.InsertUsuario(novousuario);
                TempData["MensagemLogin"] = "Cadastro realizado com sucesso! Faça o Login";
                return RedirectToAction("Login", "Autenticacao");

        }

        public ActionResult SelectLogin(string Login)
        {
            MySqlConnection conexao = new MySqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
            try
            {
                conexao.Open();
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", new { mensagem = e.Message });
            }
            finally
            {
                if (conexao.State == ConnectionState.Open)
                    conexao.Close();
            }

            bool LoginExists;
            Usuario usuariotemp = new Usuario();
            string login = usuariotemp.SelectLogin(Login);
            if (login.Length == 0)
                LoginExists = false;
            else
                LoginExists = true;
            return Json(!LoginExists, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Login(string ReturnUrl)
        {
            var viewmodel = new LoginViewModel
            {
                UrlRetorno = ReturnUrl
            };
            return View(viewmodel);
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel viewmodel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewmodel);
            };

            MySqlConnection conexao = new MySqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
            try
            {
                conexao.Open();
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", new { mensagem = e.Message });
            }
            finally
            {
                if (conexao.State == ConnectionState.Open)
                    conexao.Close();
            }
            Usuario usuario = new Usuario();
            usuario = usuario.SelectUsuario(viewmodel.Login);

            if (usuario == null | usuario.Login != viewmodel.Login)
            {
                ModelState.AddModelError("Login", "Login Incorreto");
                return View(viewmodel);
            }
            if (usuario.Senha != Hash.GerarHash(viewmodel.Senha))
            {
                ModelState.AddModelError("Senha", "Senha incorreta!");
                return View(viewmodel);
            }
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, usuario.Login),
                new Claim("Login", usuario.Login)
            }, "AppAplicationCookie");

            Request.GetOwinContext().Authentication.SignIn(identity);

            if (!String.IsNullOrWhiteSpace(viewmodel.UrlRetorno) || Url.IsLocalUrl(viewmodel.UrlRetorno))
                return Redirect(viewmodel.UrlRetorno);
            else
                return RedirectToAction("Index", "Home");
          
        }

        public ActionResult Logout()
        {
            Request.GetOwinContext().Authentication.SignOut("AppAplicationCookie");
            return RedirectToAction("Index", "Home");
        }

        public  ActionResult AlterarSenha(AlterarSenhaViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var identity = User.Identity as ClaimsIdentity;
            var Login = identity.Claims.FirstOrDefault(c => c.Type == "Login").Value;
            Usuario usuario = new Usuario();
            usuario = usuario.SelectUsuario(Login);

            if (Hash.GerarHash(viewModel.NovaSenha) == usuario.Senha)
            {
                ModelState.AddModelError("SenhaAtual", "Senha incorreta");
                return View();
            }
            usuario.Senha = Hash.GerarHash(viewModel.NovaSenha);
            usuario.UpdateSenha(usuario);

            TempData["MensagemLogin"] = "Senha Alterada com Sucesso!";
            return RedirectToAction("Index", "Home");

        }
    }
}