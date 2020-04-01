using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CalculadoraMVC.Models;

namespace CalculadoraMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        /// <summary>
        /// invocação da View inicial do projeto
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Visor = "0";
            return View();
        }


        /// <summary>
        /// Apresentação da calculadora
        /// </summary>
        /// <param name="visor">apresenta os numeros no ecrã da calculadora e o resultado das operações</param>
        /// <param name="bt">recolhe a escolha do utilizador perante os diversos botões da calculadora</param>
        /// <param name="primeiroOperando">assegura o efeito de memoria do 'HTTP', guarda os algarismos que veem da view </param>
        /// <param name="operador">assegura o efeito de memoria do 'HTTP', guarda as operações que veem da view </param>
        /// <param name="limpaVisor">especifica se o visor deve ser limpo ou não</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Index(String visor, string bt, string primeiroOperando, string operador, string limpaVisor)
        {

            //filtrar o conteudo da variavel bt
            switch (bt){
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                case "0":
                    //processar os algarismos
                    if (visor == "0" || limpaVisor=="true") visor = bt;
                    else visor += bt;

                    ViewBag.primeiroOperando = primeiroOperando;
                    ViewBag.operador = operador;
                    ViewBag.limpaVisor = "false";
                    break;

                case "+/-":
                    visor = Convert.ToDouble(visor) * -1 + "";
                    //processamento de Strings
                    //dentro do valor visor->.startsWith(), .substring, length
                    ViewBag.primeiroOperando = primeiroOperando;
                    ViewBag.operador = operador;
                    ViewBag.limpaVisor = "false";
                    break;
                case ",":
                    //processar o separador 
                    if (!visor.Contains(",")) visor += ",";
                    ViewBag.primeiroOperando = primeiroOperando;
                    ViewBag.operador = operador;
                    ViewBag.limpaVisor = "false";
                    break;
                case "C":
                    ViewBag.visor = "0";
                    ViewBag.primeiroOperando = primeiroOperando;
                    ViewBag.operador = operador;
                    ViewBag.limpaVisor = "true";
                    return View();
                    break;

                case "+":
                case "-":
                case "x":
                case ":":
                case "=":
                    //'processar os operadoes'
                    //
                    //

                    if (operador == null)
                    {
                        //efeito de memoria do HTTP que é basicamente estar sempre a enviar os valores do back para o front-end
                        ViewBag.primeiroOperando = visor;
                        ViewBag.operador = bt;

                        ViewBag.limpaVisor = "true";
                    }
                    else
                    {
                        //pesca o segundo operando, sabendo que já temos o primeiro
                        double auxprimeiroOperando = Convert.ToDouble(primeiroOperando);
                        double auxSegundoOperando = Convert.ToDouble(visor);
                        switch (operador)
                        {
                            case "+":
                                visor = auxprimeiroOperando + auxSegundoOperando + "";
                                break;
                            case "-":
                                visor = auxprimeiroOperando - auxSegundoOperando + "";
                                break;
                            case "x":
                                visor = auxprimeiroOperando * auxSegundoOperando + "";
                                break;
                            case ":":
                                visor = auxprimeiroOperando / auxSegundoOperando + "";
                                break;
                        }

                        ViewBag.primeiroOperando = visor;
                        ViewBag.operador = bt;
                        ViewBag.limpaVisor = "true";
                    }

                    if(bt == "=")
                    {
                        ViewBag.operador = null;
                    }

                    break;
                    /*
                case "C":
                    ViewBag.primeiroOperando = null;
                    ViewBag.operador = null;
                    ViewBag.limpaVisor = "true";
                    visor = "0";
                    break;
                    */

            }
            ViewBag.Visor = visor;
            return View();

        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(){
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
