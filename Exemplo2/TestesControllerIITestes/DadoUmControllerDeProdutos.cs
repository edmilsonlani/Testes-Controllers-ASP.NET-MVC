using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TestesControllerII;

namespace TestesControllerIITestes
{
    [TestClass]
    public class DadoUmControllerDeProdutos
    {
        private ProdutosController controller;
        private Mock<IRepositorioProdutos> repositorio;

        [TestInitialize]
        public void ConfiguraControllerERepositorio()
        {
            controller = new ProdutosController();
            repositorio = new Mock<IRepositorioProdutos>();
            controller.RepositorioProdutos = repositorio.Object;
        }

        [TestMethod]
        public void Action_Listar_Retorna_Todos_Produtos_Para_View_ListarProdutos()
        {
            var todosProdutos = new List<Produto>
                            {
                                new Produto("Computador"),
                                new Produto("Teclado"),
                                new Produto("Mouse"),
                                new Produto("Webcam"),
                                new Produto("Microfone"),
                            };

            repositorio
                .Setup(r => r.Todos())
                .Returns(todosProdutos);

            var resultView = controller.Listar();

            Assert.AreEqual("ListarProdutos", resultView.ViewName);
            Assert.AreEqual(todosProdutos, resultView.ViewData.Model);
            Assert.IsTrue(resultView.ViewData.Model is List<Produto>);
        }

        [TestMethod]
        public void Action_Listar_Sem_Nenhum_Produto_Deve_Retornar_View_NenhumProduto()
        {
            repositorio
                .Setup(r => r.Todos())
                .Returns(new List<Produto>());

            var viewResult = controller.Listar();

            Assert.AreEqual("NenhumProduto", viewResult.ViewName);
        }

        [TestMethod]
        public void Action_Obter_Deve_Retornar_Produtos_Em_Formato_Json()
        {
            var todosProdutos = new List<Produto>
                    {
                        new Produto("Teclado"),
                        new Produto("Monitor")
                    };
            repositorio
                .Setup(r => r.Todos())
                .Returns(todosProdutos);

            var viewResult = controller.Obter(pagina: 1, quantidade: 10);

            Assert.IsTrue(viewResult is JsonResult, "Deve ser um JsonResult");

            var jsonResult = viewResult as JsonResult;
            var jsonData = jsonResult.Data as List<Produto>;
            Assert.AreEqual(2, jsonData.Count);
        }
    }
}
