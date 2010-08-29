using System;
using System.Linq;
using System.Web.Mvc;

namespace TestesControllerII
{
    public class ProdutosController : Controller
    {
        public IRepositorioProdutos RepositorioProdutos { get; set; }

        public ViewResult Listar()
        {
            var produtos = this.RepositorioProdutos.Todos();

            if (produtos.Count > 0)
                return View("ListarProdutos", this.RepositorioProdutos.Todos());
            else
                return View("NenhumProduto");
        }

        public ActionResult Obter(int pagina, int quantidade)
        {
            var produtos = this.RepositorioProdutos.Todos()
                                    .Skip((pagina - 1) * 10)
                                    .Take(quantidade).ToList();

            return Json(produtos);
        }
    }
}