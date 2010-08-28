using System;
using System.Web.Mvc;

namespace BlogTestesControllerI
{
    public class CriarUsuarioController : Controller
    {
        public ViewResult Criar(UsuarioCriar usuarioCriar)
        {
            var view = View("CriarUsuarioView", usuarioCriar);
            if (ModelState.IsValid)
            {
                this.RepositorioDeUsuarios.Adicionar(
                    new Usuario
                        {
                            Login = usuarioCriar.Login,
                            Nome = usuarioCriar.Nome,
                            Senha = usuarioCriar.Senha
                        }
                    );

                view = View("Sucesso");
            }

            return view;
        }

        public IRepositorioUsuarios RepositorioDeUsuarios { get; set; }
    }
}