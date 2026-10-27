using ClosedXML.Excel;
using LojaVirtual.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace LojaVirtual.Controllers
{
    public class EstoqueController : Controller
    {
        private readonly IEstoqueInterface _estoqueInterface;

        public EstoqueController(IEstoqueInterface estoqueInterface)
        {
            _estoqueInterface = estoqueInterface;
        }
        public IActionResult Index()
        {
            var registros = _estoqueInterface.ListarRegistrosDeVendas();
            return View(registros);
        }

        public IActionResult GerarRelatorio()
        {
            var dados = BuscarDados();

            using (XLWorkbook workbook = new XLWorkbook())
            {
                workbook.AddWorksheet(dados, "Registro de Vendas");

                using (MemoryStream ms = new MemoryStream())
                {
                    workbook.SaveAs(ms);
                    return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "RegistroDeVendas.xlsx");
                }
            }
        }

        private DataTable BuscarDados()
        {
            DataTable dataTable = new DataTable();

            dataTable.TableName = "Registro de Vendas";
            dataTable.Columns.Add("Categoria", typeof(string));
            dataTable.Columns.Add("DataCompra", typeof(DateTime));
            dataTable.Columns.Add("ValorTotal", typeof(decimal));
            

            var dados = _estoqueInterface.ListarRegistrosDeVendas();

            if (dados != null && dados.Count() > 0)
            {
                foreach(var registro in dados)
                {
                    dataTable.Rows.Add(registro.NomeCategoria, registro.DataCompra, registro.ValorTotal);
                }
            }
            return dataTable;
        }

        [HttpPost]
        public async Task<IActionResult> BaixarEstoque(int id)
        {
            await _estoqueInterface.CriarRegistro(id);
            TempData["MenssagemSucesso"] = "Compra realizada com sucesso!";
            return RedirectToAction("Index", "Home");
        }
    }
}
