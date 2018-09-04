using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaixeiroViajante
{
    public static class ConstrutorSolucao
    {
        public static int[] ConstruirSolucaoSequencial(int numeroCidades) // Construcao.cpp -> constroi_solucao
        {
            var solucao = new int[numeroCidades];

            for (int i = 0; i < numeroCidades; i++)
                solucao[i] = i;

            return solucao;
        }

        public static int[] ConstruirSolucaoVizinhoMaisProximo(int numeroCidades, double[,] matrizDistancias) // Construcao.cpp ->  constroi_solucao_gulosa_vizinho_mais_proximo
        {
            var cidadesAdicionadas = new List<int>(numeroCidades);

            var solucao = new int[numeroCidades];
            solucao[0] = 0;
            cidadesAdicionadas.Add(0);

            int contadorCidadeAtual = 1, contadorCidadesPendentes = 1;
            int indiceCidadeMaisProxima = 0;
            double distanciaCidadeMaisProxima;

            while (contadorCidadeAtual < numeroCidades)
            {
                distanciaCidadeMaisProxima = int.MaxValue;

                int numeroCidadesAdicionadas = cidadesAdicionadas.Count;
                contadorCidadesPendentes = 1;
                while (contadorCidadesPendentes < numeroCidades)
                {
                    if (!cidadesAdicionadas.Contains(contadorCidadesPendentes) && matrizDistancias[solucao[contadorCidadeAtual - 1], contadorCidadesPendentes] < distanciaCidadeMaisProxima)
                    {
                        distanciaCidadeMaisProxima = matrizDistancias[solucao[contadorCidadeAtual - 1], contadorCidadesPendentes];
                        indiceCidadeMaisProxima = contadorCidadesPendentes;
                    }
                    
                    contadorCidadesPendentes++;
                }

                solucao[contadorCidadeAtual] = indiceCidadeMaisProxima;
                cidadesAdicionadas.Add(indiceCidadeMaisProxima);
                contadorCidadeAtual++;
            }

            return solucao;
        }

        public static void ConstruirSolucaoAleatoria(int n)
        {

        }

        public static void ConstruirSolucaoParcialmenteGulosaVizinhoMaisProximo()
        {

        }

        public static void ConstruirSolucaoGulosaMaisBarata()
        {

        }

        public static void ConstruirSolucaoParcialmenteGulosaInsercaoMaisBarata()
        {

        }
    }
}
