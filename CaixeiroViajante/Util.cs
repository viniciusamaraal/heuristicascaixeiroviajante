using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaixeiroViajante
{
    public static class Util
    {
        public static class Arquivo
        {
            public static int LerNumeroCidades(string nomeArquivo) // Util.cpp -> obter_parametros_pcv
            {
                int numeroCidades = 0;

                using (var leitorArquivo = new StreamReader(nomeArquivo))
                {
                    string linhaArquivo = leitorArquivo.ReadLine();
                    numeroCidades = linhaArquivo.Split(' ').Select(x => int.Parse(x)).First();
                }

                return numeroCidades;
            }

            public static double[,] LerMatrizDistancias(string nomeArquivo, int numeroCidades) // Util.cpp -> le_arq_matriz
            {
                var matrizDistancias = new double[numeroCidades, numeroCidades];

                var vet_x = new int[numeroCidades];
                var vet_y = new int[numeroCidades];

                using (var leitorArquivo = new StreamReader(nomeArquivo))
                {
                    string linhaArquivo = leitorArquivo.ReadLine();

                    do
                    {
                        int[] parametrosLinha = linhaArquivo.Split(' ').Select(x => int.Parse(x)).ToArray();

                        vet_x[parametrosLinha[0]] = parametrosLinha[1];
                        vet_y[parametrosLinha[0]] = parametrosLinha[2];

                        linhaArquivo = leitorArquivo.ReadLine();
                    } while (!string.IsNullOrEmpty(linhaArquivo));

                    for (int i = 0; i < numeroCidades - 1; i++)
                    {
                        matrizDistancias[i, i] = 0;
                        for (int j = i + 1; j < numeroCidades; j++)
                        {
                            matrizDistancias[i, j] = Math.Sqrt(Math.Pow(vet_x[i] - vet_x[j], 2) + Math.Pow(vet_y[i] - vet_y[j], 2));
                            matrizDistancias[j, i] = matrizDistancias[i, j];
                        }
                    }
                }

                return matrizDistancias;
            }
        }
        
        public static class Calculo
        {
            public static double CalcularFuncaoObjetivo(int[] solucao, double[,] matrizDistancias)
            {
                double distanciaPercorrida = 0;

                for (int i = 0; i < solucao.Length - 1; i++)
                    distanciaPercorrida += matrizDistancias[solucao[i], solucao[i + 1]];

                distanciaPercorrida += matrizDistancias[solucao[solucao.Length - 1], 0];

                return distanciaPercorrida;
            }

            public static void CalcularDuasPosicoesAleatoriasDiferentes(int valorMinimo, int valorMaximo, ref int posicao1, ref int posicao2)
            {
                var random = new Random(DateTime.Now.Millisecond);

                do
                {
                    posicao1 = random.Next(valorMinimo, valorMaximo);
                    posicao2 = random.Next(valorMinimo, valorMaximo);
                } while (posicao1 == posicao2);
            }

            public static List<int> CalcularDiferencaEntreDoisVetores(int[] vetor1, int[] vetor2)
            {
                var resultado = new List<int>();

                for (int i = 0; i < vetor1.Length; i++)
                {
                    if (vetor1[i] != vetor2[i])
                        resultado.Add(i);
                }

                return resultado;
            }
        }

        public static class Impressao
        {
            private static void ImprimirRota(int[] solucao)
            {
                Console.Write("\n[ ");
                Console.Write($"{ String.Join(" ", solucao.Select(x => x.ToString() + " => ")) } ");

                Console.Write("0 ");
                Console.Write("]");
            }

            public static void ImprimirResultadoExecucao(string titulo, int[] solucao, double[,] matrizDistancias)
            {
                Console.Clear();
                Console.SetWindowSize(150, 30);

                Console.WriteLine($"\t\t\t\tResultado da execução utilizando a heurística \"{ titulo }\"");

                Util.Impressao.ImprimirRota(solucao);

                double resultadoFuncaoObjetivo = Util.Calculo.CalcularFuncaoObjetivo(solucao, matrizDistancias);
                Console.WriteLine($"\n\nResultado da função objetivo: { Math.Round(resultadoFuncaoObjetivo, 2) }");
            }

            public static void ImprimirMatrizDistancias(double[,] distancias)
            {
                try
                {
                    Console.SetWindowSize(120, 60);
                }
                catch (Exception) { }

                Console.Write("".PadRight(6, ' '));
                for (int i = 0; i < distancias.GetLength(0); i++)
                    Console.Write($"{ i }".PadRight(6, ' '));

                Console.WriteLine("");
                for (int i = 0; i < distancias.GetLength(0); i++)
                {
                    Console.Write($"{ i }".PadRight(6, ' '));
                    for (int j = 0; j < distancias.GetLength(1); j++)
                        Console.Write($"{ Math.Round(distancias[i, j], 2) } ".PadRight(6, ' '));

                    Console.WriteLine();
                }
            }
        }
    }
}