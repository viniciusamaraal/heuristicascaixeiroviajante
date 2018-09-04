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
                        matrizDistancias[i, j] = (float)Math.Sqrt(Math.Pow(vet_x[i] - vet_x[j], 2) + Math.Pow(vet_y[i] - vet_y[j], 2));
                        matrizDistancias[j, i] = matrizDistancias[i, j];
                    }
                }
            }

            return matrizDistancias;
        }

        public static void EmbaralharVetor(int[] vetor) // Util.cpp -> embaralha_vetor
        {
            int aux, posicao1 = 0, posicao2 = 0;

            var random = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < vetor.Length; i++)
            {
                do
                {
                    posicao1 = random.Next(0, vetor.Length);
                    posicao2 = random.Next(0, vetor.Length);
                } while (posicao1 == posicao2);

                aux = vetor[posicao1];
                vetor[posicao1] = vetor[posicao2];
                vetor[posicao2] = aux;
            }
        }

        private static double CalcularFuncaoObjetivo(int[] solucao, double[,] matrizDistancias)
        {
            double distanciaPercorrida = 0;

            for (int i = 0; i < solucao.Length - 1; i++)
                distanciaPercorrida += matrizDistancias[solucao[i], solucao[i + 1]];

            distanciaPercorrida += matrizDistancias[solucao.Length - 1, 0];

            return distanciaPercorrida;

        }

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

            Util.ImprimirRota(solucao);

            double resultadoFuncaoObjetivo = CalcularFuncaoObjetivo(solucao, matrizDistancias);
            Console.WriteLine($"\n\nResultado da função objetivo: { Math.Round(resultadoFuncaoObjetivo, 2) }");
        }

        public static void ImprimirMatrizDistancias(double[,] distancias)
        {
            Console.SetWindowSize(120, 60);

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
