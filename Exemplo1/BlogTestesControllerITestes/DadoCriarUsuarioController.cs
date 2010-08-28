using BlogTestesControllerI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BlogTestesControllerITestes
{
    [TestClass]
    public class DadoCriarUsuarioController
    {
        [TestMethod]
        public void E_UsuarioCriar_Valido_Entao_Adiciona_Ao_Repositório_E_Retorna_View_Sucesso()
        {
            var controller = new CriarUsuarioController();
            var repositorio = new Mock<IRepositorioUsuarios>();
            controller.RepositorioDeUsuarios = repositorio.Object;

            var usuarioCriar = new UsuarioCriar
                                    {
                                        Nome = "Nome",
                                        Login = "Login",
                                        Senha = "Senha"
                                    };
            var view = controller.Criar(usuarioCriar);
            repositorio
                .Verify(r => r.Adicionar(It.Is<Usuario>(u => Iguais(u, usuarioCriar))), Times.Once());

            Assert.AreEqual("Sucesso", view.ViewName);
        }
        [TestMethod]
        public void E_UsuarioCriar_Inalido_Entao_Nao_Adiciona_Ao_Repositório_E_Retorna_View_CriarUsuario()
        {
            var controller = new CriarUsuarioController();
            var repositorio = new Mock<IRepositorioUsuarios>();
            controller.RepositorioDeUsuarios = repositorio.Object;

            controller.ModelState.AddModelError("Senha", "Senha deve ser preenchida");

            var usuarioCriar = new UsuarioCriar
                                    {
                                        Nome = "Teste",
                                        Login = "Login",
                                        Senha = null
                                    };
            var view = controller.Criar(usuarioCriar);

            repositorio
                .Verify(r => r.Adicionar(It.IsAny<Usuario>()), Times.Never());

            Assert.AreEqual("CriarUsuarioView", view.ViewName);
            Assert.AreEqual(usuarioCriar, view.ViewData.Model);
        }

        private bool Iguais(Usuario usuario, UsuarioCriar usuarioCriar)
        {
            return usuario.Nome == usuarioCriar.Nome &&
                   usuario.Login == usuarioCriar.Login &&
                   usuario.Senha == usuarioCriar.Senha;
        }
    }
}
