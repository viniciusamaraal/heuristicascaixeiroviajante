using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaixeiroViajante
{
    class Program
    {
        static void Main(string[] args)
        {
            int paramMaximoTentativasSemMelhora = 100000; // Usado nos métodos: Descida com Random Improvement

            string opcao;
            bool encerrarExecucao, opcaoInvalida;
            string titulo = string.Empty;
            int numeroCidades = 0;
            double[,] distancias = null;

            int[] solucao = null;

            // Instância fornecida pelo professor
            numeroCidades = Util.Arquivo.Professor.LerNumeroCidades("..\\..\\_instancias\\professor\\C50INFO.txt");
            distancias = Util.Arquivo.Professor.LerMatrizDistancias("..\\..\\_instancias\\professor\\C50.txt", numeroCidades);

            // Instância obtida na internet com um número menor de cidades. Fonte: https://people.sc.fsu.edu/~jburkardt/datasets/tsp/tsp.html
            //numeroCidades = 15;
            //distancias = Util.Arquivo.P01.LerMatrizDistancias("..\\..\\_instancias\\p01_15\\TSP_IO_PRB_15.txt", numeroCidades);

            do
            {
                encerrarExecucao = opcaoInvalida = false;

                Console.Clear();
                Console.SetWindowSize(80, 30); 

                Console.WriteLine("\t\t\tHeurísticas Computacionais");

                Console.WriteLine("\nEscolha a opção desejada: ");
                Console.WriteLine("\n[01] Gerar solucao inicial:");
                Console.WriteLine("\t[01a] Solução gulosa (vizinho mais próximo)");
                Console.WriteLine("\t[01b] Solução parcialmente gulosa (vizinho mais próximo)");
                Console.WriteLine("\t[01c] Solução gulosa (inserção mais barata)");
                Console.WriteLine("\t[01d] Solução parcialmente gulosa (inserção mais barata)");
                Console.WriteLine("\t[01e] Solução aleatória - 01 default");
                Console.WriteLine("[02] Descida com Best Improvement");
                Console.WriteLine("[03] Descida com Random Improvement");
                Console.WriteLine("[04] Descida com First Improvement");
                Console.WriteLine("[05] Multi-Start");
                Console.WriteLine("[06] Simulated Annealing");
                Console.WriteLine("[07] Busca Tabu");
                Console.WriteLine("[08] ILS");
                Console.WriteLine("[09] GRASP");
                Console.WriteLine("[10] VND");
                Console.WriteLine("[11] VNS");
                Console.WriteLine("[12] Algoritmos Genéticos");
                Console.WriteLine("[13] Algoritmos Meméticos");
                Console.WriteLine("[14] Colônia de Formigas");

                Console.WriteLine("[15] Imprimir matriz de distâncias");

                Console.WriteLine("\n[0] Sair");

                Console.Write("\nSua escolha: ");

                opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "01a":
                        solucao = ConstrutorSolucao.VizinhoMaisProximo(numeroCidades, distancias);

                        titulo = "[01a] Solução gulosa (vizinho mais próximo)";
                        Util.Impressao.ImprimirResultadoExecucao(titulo, solucao, distancias);
                        break;
                    case "01b":
                        double fatorConsideracaoListaCandidatos = 0.2;
                        solucao = ConstrutorSolucao.ParcialmenteGulosaVizinhoMaisProximo(numeroCidades, distancias, fatorConsideracaoListaCandidatos);

                        titulo = "[01b] Solução parcialmente gulosa (vizinho mais próximo)";
                        Util.Impressao.ImprimirResultadoExecucao(titulo, solucao, distancias);
                        break;
                    case "01c":
                        titulo = "[01c] Solução gulosa (inserção mais barata)";
                        Console.WriteLine("Não implementado");
                        break;
                    case "01d":
                        titulo = "[01d] Solução parcialmente gulosa (inserção mais barata)";
                        Console.WriteLine("Não implementado");
                        break;
                    case "01":
                    case "01e":
                        solucao = ConstrutorSolucao.Aleatoria(numeroCidades);

                        titulo = "[01e] Solução aleatória";
                        Util.Impressao.ImprimirResultadoExecucao(titulo, solucao, distancias);
                        break;
                    case "02":
                        if (solucao == null)
                            solucao = ConstrutorSolucao.VizinhoMaisProximo(numeroCidades, distancias);

                        ConstrutorSolucao.DescidaBestImprovement(solucao, distancias);

                        titulo = "[02] Descida com Best Improvement";
                        Util.Impressao.ImprimirResultadoExecucao(titulo, solucao, distancias);
                        break;
                    case "03":
                        if (solucao == null)
                            solucao = ConstrutorSolucao.VizinhoMaisProximo(numeroCidades, distancias);

                        ConstrutorSolucao.DescidaRandomImprovement(solucao, distancias, paramMaximoTentativasSemMelhora);

                        titulo = "[03] Descida com Random Improvement";
                        Util.Impressao.ImprimirResultadoExecucao(titulo, solucao, distancias);
                        break;
                    case "04":
                        if (solucao == null)
                            solucao = ConstrutorSolucao.VizinhoMaisProximo(numeroCidades, distancias);

                        ConstrutorSolucao.DescidaFirstImprovement(solucao, distancias);

                        titulo = "[04] Descida com First Improvement";
                        Util.Impressao.ImprimirResultadoExecucao(titulo, solucao, distancias);
                        break;
                    case "05":
                        Console.WriteLine("Não implementado");
                        break;
                    case "06":
                        if (solucao == null)
                            solucao = ConstrutorSolucao.VizinhoMaisProximo(numeroCidades, distancias);

                        int numMaximoIteracoesTemperatura = 500;
                        double taxaAquecimentoCalculoTemperaturaInicial = 1.1, taxaResfriamentoCalculoTemperaturaInicial = 0.95, fatorAlteracaoTemperatura = 0.99, temperaturaFinal = 0.01;
                        double temperaturaInicial = 10.0;
                        temperaturaInicial = 1500;//ConstrutorSolucao.CalcularTemperaturaInicialSimulatedAnealling(solucao, distancias, taxaAquecimentoCalculoTemperaturaInicial, taxaResfriamentoCalculoTemperaturaInicial, numMaximoIteracoesTemperatura, temperaturaInicial);
                        ConstrutorSolucao.SimulatedAnnealing(solucao, distancias, fatorAlteracaoTemperatura, numMaximoIteracoesTemperatura, temperaturaInicial, temperaturaFinal);

                        titulo = "[06] Simulated Annealing";
                        Util.Impressao.ImprimirResultadoExecucao(titulo, solucao, distancias);

                        break;
                    case "07":
                        if (solucao == null)
                            solucao = ConstrutorSolucao.Aleatoria(numeroCidades);

                        int numMaximoIteracoesSemMelhoraILS = 50;
                        int numIteracoesProibicao = 7;
                        ConstrutorSolucao.BuscaTabu(solucao, distancias, numMaximoIteracoesSemMelhoraILS, numIteracoesProibicao);

                        titulo = "[07] Busca Tabu";
                        Util.Impressao.ImprimirResultadoExecucao(titulo, solucao, distancias);
                        break;
                    case "08":
                        if (solucao == null)
                            solucao = ConstrutorSolucao.Aleatoria(numeroCidades);

                        int numMaximoIteracoesSemMelhoraBT = 300;
                        int numMaximoIteracoesMesmoNivel = 150;
                        ConstrutorSolucao.ILSSmart(solucao, distancias, numMaximoIteracoesSemMelhoraBT, numMaximoIteracoesMesmoNivel);

                        titulo = "[08] ILS";
                        Util.Impressao.ImprimirResultadoExecucao(titulo, solucao, distancias);
                        break;
                    case "09":
                        if (solucao == null)
                            solucao = ConstrutorSolucao.Aleatoria(numeroCidades);

                        double fatorConsideracaoListaCandidatosGrasp = 0.1;
                        int numMaximoIteracoesGrasp = 500;

                        ConstrutorSolucao.Grasp(solucao, distancias, numMaximoIteracoesGrasp, fatorConsideracaoListaCandidatosGrasp);
                        titulo = "[09] GRASP";
                        Util.Impressao.ImprimirResultadoExecucao(titulo, solucao, distancias);
                        break;
                    case "10":
                        Console.WriteLine("Não implementado");
                        break;
                    case "11":
                        Console.WriteLine("Não implementado");
                        break;
                    case "12":
                        Console.WriteLine("Não implementado");
                        break;
                    case "13":
                        Console.WriteLine("Não implementado");
                        break;
                    case "14":
                        Console.WriteLine("Não implementado");
                        break;
                    case "15":
                        Util.Impressao.ImprimirMatrizDistancias(distancias);
                        break;
                    case "0":
                        encerrarExecucao = true;
                        break;
                    default:
                        opcaoInvalida = true;
                        break;
                }

                if (opcaoInvalida)
                    Console.Write("\nOpção inválida! Pressione qualquer tecla para tentar novamente...");
                else if (encerrarExecucao)
                    Console.Write("\nFim da execução! Pressione qualquer tecla para finalizar...");
                else
                    Console.Write("\nPresione qualquer tecla para retornar ao menu principal...");

                Console.ReadKey();
            } while (encerrarExecucao == false);
        }
    }
}
