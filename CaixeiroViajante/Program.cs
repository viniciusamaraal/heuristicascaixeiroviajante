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
            string opcao;
            bool encerrarExecucao, opcaoInvalida;
            string titulo = string.Empty;

            int[] solucao = null;

            int numeroCidades = Util.LerNumeroCidades("C50INFO.txt");
            double[,] distancias = Util.LerMatrizDistancias("C50.txt", numeroCidades);
            
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
                Console.WriteLine("[03] Descida randomica");
                Console.WriteLine("[04] Descida com Primeiro de Melhora (First Improvement)");
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
                        solucao = ConstrutorSolucao.ConstruirSolucaoVizinhoMaisProximo(numeroCidades, distancias);

                        titulo = "[01a] Solução gulosa (vizinho mais próximo)";
                        Util.ImprimirResultadoExecucao(titulo, solucao, distancias);
                        break;
                    case "01b":
                        titulo = "[01b] Solução parcialmente gulosa (vizinho mais próximo)";
                        Console.WriteLine("Não implementado");
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
                        solucao = ConstrutorSolucao.ConstruirSolucaoSequencial(numeroCidades);
                        Util.EmbaralharVetor(solucao);

                        titulo = "[01e] Solução aleatória";
                        Util.ImprimirResultadoExecucao(titulo, solucao, distancias);
                        break;
                    case "02":
                        Console.WriteLine("Não implementado");
                        break;
                    case "03":
                        Console.WriteLine("Não implementado");
                        break;
                    case "04":
                        Console.WriteLine("Não implementado");
                        break;
                    case "05":
                        Console.WriteLine("Não implementado");
                        break;
                    case "06":
                        Console.WriteLine("Não implementado");
                        break;
                    case "07":
                        Console.WriteLine("Não implementado");
                        break;
                    case "08":
                        Console.WriteLine("Não implementado");
                        break;
                    case "09":
                        Console.WriteLine("Não implementado");
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
                        Util.ImprimirMatrizDistancias(distancias);
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
