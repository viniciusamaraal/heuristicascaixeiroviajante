using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaixeiroViajante
{
    public static class ConstrutorSolucao
    {
        #region [01*] Soluções iniciais 

        public static int[] VizinhoMaisProximo(int numeroCidades, double[,] distancias) // Construcao.cpp ->  constroi_solucao_gulosa_vizinho_mais_proximo
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
                    if (!cidadesAdicionadas.Contains(contadorCidadesPendentes) && distancias[solucao[contadorCidadeAtual - 1], contadorCidadesPendentes] < distanciaCidadeMaisProxima)
                    {
                        distanciaCidadeMaisProxima = distancias[solucao[contadorCidadeAtual - 1], contadorCidadesPendentes];
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

        public static int[] Aleatoria(int numeroCidades)
        {
            var solucao = new int[numeroCidades];

            for (int i = 0; i < numeroCidades; i++)
                solucao[i] = i;

            solucao.EmbaralharVetor();

            return solucao;
        }

        public static int[] ParcialmenteGulosaVizinhoMaisProximo(int numeroCidades)
        {
            var solucao = new int[numeroCidades];

            return solucao;
        }

        public static int[] GulosaMaisBarata(int numeroCidades)
        {
            var solucao = new int[numeroCidades];

            return solucao;
        }

        public static int[] ParcialmenteGulosaInsercaoMaisBarata(int numeroCidades)
        {
            var solucao = new int[numeroCidades];

            return solucao;
        }

        #endregion

        #region [02] Descida com Best Improvement

        public static double DescidaBestImprovement(int[] solucaoInicial, double[,] distancias) // Descida.cpp -> descida
        {
            double resultadoFO, resultadoFOVizinho;
            int melhor_i = 0, melhor_j = 0, iter = 0, aux;
            bool melhorou;

            resultadoFO = resultadoFOVizinho = Util.Calculo.CalcularFuncaoObjetivo(solucaoInicial, distancias);

            do
            {
                melhorou = false;
                resultadoFOVizinho = CalcularMelhorVizinho(solucaoInicial, distancias, resultadoFO, ref melhor_i, ref melhor_j);
                if (resultadoFOVizinho < resultadoFO)
                {
                    iter += 1;

                    aux = solucaoInicial[melhor_j];
                    solucaoInicial[melhor_j] = solucaoInicial[melhor_i];
                    solucaoInicial[melhor_i] = aux;

                    resultadoFO = resultadoFOVizinho;
                    melhorou = true;
                }

            } while (melhorou);

            return resultadoFO;
        }

        private static double CalcularMelhorVizinho(int[] solucaoAtual, double[,] distancias, double resultadoFOAtual, ref int melhor_i, ref int melhor_j) // Descida.cpp -> melhor_vizinho
        {
            int aux;
            double resultadoFOVizinho;

            double resultadoFOMelhorVizinho = resultadoFOAtual;

            for (int i = 0; i < solucaoAtual.Length - 1; i++)
            {
                for (int j = i + 1; j < solucaoAtual.Length; j++)
                {
                    double delta1 = CalcularDelta(solucaoAtual.Length, solucaoAtual, distancias, i, j);

                    aux = solucaoAtual[j];
                    solucaoAtual[j] = solucaoAtual[i];
                    solucaoAtual[i] = aux;

                    double delta2 = CalcularDelta(solucaoAtual.Length, solucaoAtual, distancias, i, j);

                    resultadoFOVizinho = resultadoFOAtual - delta1 + delta2;

                    if (resultadoFOVizinho < resultadoFOMelhorVizinho)
                    {
                        melhor_i = i;
                        melhor_j = j;
                        resultadoFOMelhorVizinho = resultadoFOVizinho;
                    }

                    aux = solucaoAtual[j];
                    solucaoAtual[j] = solucaoAtual[i];
                    solucaoAtual[i] = aux;
                }
            }

            return resultadoFOMelhorVizinho;
        }

        #endregion

        #region [03] Descida com Random Improvement

        public static double DescidaRandomImprovement(int[] solucaoInicial, double[,] distancias, int maximoTentativasSemMelhora)
        {
            int tentativasSemMelhora = 0, melhor_i = 0, melhor_j = 0;
            bool houveMelhora = false;

            double resultadoFOAtual = Util.Calculo.CalcularFuncaoObjetivo(solucaoInicial, distancias); ;
            double resultadoFOVizinho = double.MaxValue;
            do
            {
                houveMelhora = CalcularVizinhoAleatorio(solucaoInicial, distancias, resultadoFOAtual, ref melhor_i, ref melhor_j, ref resultadoFOVizinho);

                if (houveMelhora)
                {
                    resultadoFOAtual = resultadoFOVizinho;

                    tentativasSemMelhora = 0;
                }
                else
                    tentativasSemMelhora++;

            } while (tentativasSemMelhora < maximoTentativasSemMelhora);

            return resultadoFOAtual;
        }

        private static bool CalcularVizinhoAleatorio(int[] solucaoAtual, double[,] distancias, double resultadoFOAtual, ref int melhor_i, ref int melhor_j, ref double resultadoFOVizinho)
        {
            int aux;
            int posicao1 = 0, posicao2 = 0;

            Util.Calculo.CalcularDuasPosicoesAleatoriasDiferentes(0, solucaoAtual.Length, ref posicao1, ref posicao2);

            double delta1 = CalcularDelta(solucaoAtual.Length, solucaoAtual, distancias, posicao1, posicao2);

            aux = solucaoAtual[posicao1];
            solucaoAtual[posicao1] = solucaoAtual[posicao2];
            solucaoAtual[posicao2] = aux;

            double delta2 = CalcularDelta(solucaoAtual.Length, solucaoAtual, distancias, posicao1, posicao2);

            resultadoFOVizinho = resultadoFOAtual - delta1 + delta2;

            if (resultadoFOVizinho < resultadoFOAtual)
            {
                melhor_i = posicao1;
                melhor_j = posicao2;

                return true;
            }
            else
            {
                aux = solucaoAtual[posicao1];
                solucaoAtual[posicao1] = solucaoAtual[posicao2];
                solucaoAtual[posicao2] = aux;

                return false;
            }
        }

        #endregion

        #region [04] Descida com First Improvement

        public static double DescidaFirstImprovement(int[] solucaoInicial, double[,] distancias) // Descida.cpp -> descida
        {
            double resultadoFO, resultadoFOVizinho;
            int melhor_i = 0, melhor_j = 0, iter = 0, aux;
            bool melhorou;

            resultadoFO = resultadoFOVizinho = Util.Calculo.CalcularFuncaoObjetivo(solucaoInicial, distancias);

            do
            {
                melhorou = false;
                resultadoFOVizinho = CalcularPrimeiroMelhorVizinho(solucaoInicial, distancias, resultadoFO, ref melhor_i, ref melhor_j);
                if (resultadoFOVizinho < resultadoFO)
                {
                    iter += 1;

                    aux = solucaoInicial[melhor_j];
                    solucaoInicial[melhor_j] = solucaoInicial[melhor_i];
                    solucaoInicial[melhor_i] = aux;

                    resultadoFO = resultadoFOVizinho;
                    melhorou = true;
                }

            } while (melhorou);

            return resultadoFO;
        }

        private static double CalcularPrimeiroMelhorVizinho(int[] solucaoAtual, double[,] distancias, double resultadoFOAtual, ref int melhor_i, ref int melhor_j) // Descida.cpp -> melhor_vizinho
        {
            int aux;
            double resultadoFOVizinho;
            bool encontrou = false;

            double resultadoFOMelhorVizinho = resultadoFOAtual;

            for (int i = 0; i < solucaoAtual.Length - 1 && !encontrou; i++)
            {
                for (int j = i + 1; j < solucaoAtual.Length && !encontrou; j++)
                {
                    double delta1 = CalcularDelta(solucaoAtual.Length, solucaoAtual, distancias, i, j);

                    aux = solucaoAtual[j];
                    solucaoAtual[j] = solucaoAtual[i];
                    solucaoAtual[i] = aux;

                    double delta2 = CalcularDelta(solucaoAtual.Length, solucaoAtual, distancias, i, j);

                    resultadoFOVizinho = resultadoFOAtual - delta1 + delta2;

                    if (resultadoFOVizinho < resultadoFOMelhorVizinho)
                    {
                        melhor_i = i;
                        melhor_j = j;
                        resultadoFOMelhorVizinho = resultadoFOVizinho;

                        encontrou = true;
                    }

                    aux = solucaoAtual[j];
                    solucaoAtual[j] = solucaoAtual[i];
                    solucaoAtual[i] = aux;
                }
            }

            return resultadoFOMelhorVizinho;
        }

        #endregion

        #region [06] Simulated Annealing

        // TODO: próximo a implementar

        #endregion

        #region [ Métodos auxiliares ]

        private static double CalcularDelta(int numeroCidades, int[] solucaoAtual, double[,] distancias, int i, int j) // Descida.cpp -> calcula_delta
        {
            int i_antes, i_depois, j_antes, j_depois;

            i_antes = i - 1;
            i_depois = i + 1;
            j_antes = j - 1;
            j_depois = j + 1;

            if (i == 0)
                i_antes = numeroCidades - 1;

            if (i == numeroCidades - 1)
                i_depois = 0;

            if (j == 0)
                j_antes = numeroCidades - 1;

            if (j == numeroCidades - 1)
                j_depois = 0;

            return distancias[solucaoAtual[i_antes], solucaoAtual[i]] + distancias[solucaoAtual[i], solucaoAtual[i_depois]] + distancias[solucaoAtual[j_antes], solucaoAtual[j]] + distancias[solucaoAtual[j], solucaoAtual[j_depois]];
        }

        #endregion
    }
}