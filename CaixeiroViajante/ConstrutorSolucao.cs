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

        public static int[] GulosaInsercaoMaisBarata(int numeroCidades)
        {
            var solucao = new int[numeroCidades];

            // TODO: implementar este

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

        private static bool CalcularVizinhoAleatorio(int[] solucaoAtual, double[,] distancias, double resultadoFOAtual, ref int i_calculado, ref int j_calculado, ref double resultadoFOVizinho)
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
                i_calculado = posicao1;
                j_calculado = posicao2;

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

        public static double SimulatedAnnealing(int[] solucaoAtual, double[,] distancias, double fatorAlteracaoTemperatura, int numMaximoIteracoesTemperatura,
            double temperaturaInicial, double temperaturaFinal)
        {
            int qtdTotalIteracoes = 0, qtdTotalSolucoesMelhora = 0, qtdSolucoesPiorAceitas = 0, qtdTotalSolucoesNaoAceitas = 0;

            int iteracaoAtualTemperatura = 0, iTroca = -1, jTroca = -1, aux = 0;
            double resultadoFOMelhorSolucao, resultadoFOSolucaoAtual, delta1 = 0, delta2 = 0, deltaFinal = 0;

            int[] melhorSolucao = new int[solucaoAtual.Length];
            Array.Copy(solucaoAtual, melhorSolucao, solucaoAtual.Length);

            resultadoFOSolucaoAtual = resultadoFOMelhorSolucao = Util.Calculo.CalcularFuncaoObjetivo(solucaoAtual, distancias);
            while (temperaturaInicial > temperaturaFinal)
            {
                while (iteracaoAtualTemperatura < numMaximoIteracoesTemperatura)
                {
                    qtdTotalIteracoes++;

                    iteracaoAtualTemperatura++;
                    
                    Util.Calculo.CalcularDuasPosicoesAleatoriasDiferentes(0, solucaoAtual.Length, ref iTroca, ref jTroca);
                    delta1 = CalcularDelta(solucaoAtual.Length, solucaoAtual, distancias, iTroca, jTroca);

                    aux = solucaoAtual[jTroca];
                    solucaoAtual[jTroca] = solucaoAtual[iTroca];
                    solucaoAtual[iTroca] = aux;

                    delta2 = CalcularDelta(solucaoAtual.Length, solucaoAtual, distancias, iTroca, jTroca);

                    deltaFinal = -delta1 + delta2;

                    // verifica se a solução gerada é melhor que a solução atual
                    if (deltaFinal < 0)
                    {
                        qtdTotalSolucoesMelhora++; // apenas teste
                        resultadoFOSolucaoAtual = resultadoFOSolucaoAtual + deltaFinal;

                        // verifica se a solução gerada é melhor que a melhor das soluções encontradas
                        if (resultadoFOSolucaoAtual < resultadoFOMelhorSolucao)
                        {
                            resultadoFOMelhorSolucao = resultadoFOSolucaoAtual;
                            Array.Copy(solucaoAtual, melhorSolucao, solucaoAtual.Length);
                        }
                    }
                    else
                    {
                        double x;
                        x = new Random().NextDouble();

                        // verifica o grau de aceitação da solução de piora baseado na temperatura atual
                        if (x < Math.Exp(-deltaFinal / temperaturaInicial))
                        {
                            qtdSolucoesPiorAceitas++; // apenas teste
                            resultadoFOSolucaoAtual = resultadoFOSolucaoAtual + deltaFinal;
                        }
                        else
                        {
                            qtdTotalSolucoesNaoAceitas++; // apenas teste

                            // Caso a solução não seja aceita desfaz a troca
                            aux = solucaoAtual[jTroca];
                            solucaoAtual[jTroca] = solucaoAtual[iTroca];
                            solucaoAtual[iTroca] = aux;
                        }
                    }
                }

                temperaturaInicial = temperaturaInicial * fatorAlteracaoTemperatura;
                iteracaoAtualTemperatura = 0;
            }

            Array.Copy(melhorSolucao, solucaoAtual, solucaoAtual.Length);

            return resultadoFOMelhorSolucao;
        }

        public static double CalcularTemperaturaInicialSimulatedAnealling(int[] solucaoAtual, double[,] distancias, 
            double taxaAquecimento, double taxaResfriamento, int numMaximoIteracoesTemperatura, double temperaturaInicial)
        {
            int iTroca = -1, jTroca = -1, aux, iteracaoAtual = 0, aceitos = 0;
            double delta1 = 0, delta2 = 0, deltaFinal = 0;
            bool continua = true;

            while (continua)
            {
                aceitos = 0;
                iteracaoAtual = 0;
                
                while (iteracaoAtual < numMaximoIteracoesTemperatura)
                {
                    iteracaoAtual++;

                    Util.Calculo.CalcularDuasPosicoesAleatoriasDiferentes(0, solucaoAtual.Length, ref iTroca, ref jTroca);
                    delta1 = CalcularDelta(solucaoAtual.Length, solucaoAtual, distancias, iTroca, jTroca);

                    aux = solucaoAtual[jTroca];
                    solucaoAtual[jTroca] = solucaoAtual[iTroca];
                    solucaoAtual[iTroca] = aux;

                    delta2 = CalcularDelta(solucaoAtual.Length, solucaoAtual, distancias, iTroca, jTroca);

                    deltaFinal = -delta1 + delta2;

                    if (deltaFinal < 0)
                        aceitos++;
                    else
                    {
                        double x;
                        x = new Random().NextDouble();
                        if (x < Math.Exp(-deltaFinal / temperaturaInicial))
                            aceitos++;
                    }

                    aux = solucaoAtual[iTroca];
                    solucaoAtual[iTroca] = solucaoAtual[jTroca];
                    solucaoAtual[jTroca] = aux;
                }

                if (aceitos < taxaResfriamento * numMaximoIteracoesTemperatura)
                    temperaturaInicial = taxaAquecimento * temperaturaInicial;
                else
                    continua = false;
            }

            return temperaturaInicial;
        }

        #endregion

        #region [07] Busca Tabu

        public static double BuscaTabu(int[] solucaoAtual, double[,] distancias, int numMaximoIterSemMelhora, int numIteracoesProibicao)
        {
            int iterAtual = 0, melhorIter = 0, melhor_i = -1, melhor_j = -1;
            double resultadoFOMelhorSolucao, resultadoFOMelhorVizinho;
            
            int[,] matrizTabu = new int[solucaoAtual.Length, solucaoAtual.Length];

            int[] melhorSolucao = new int[solucaoAtual.Length];
            Array.Copy(solucaoAtual, melhorSolucao, solucaoAtual.Length);

            resultadoFOMelhorVizinho = resultadoFOMelhorSolucao = Util.Calculo.CalcularFuncaoObjetivo(solucaoAtual, distancias);
            while (iterAtual - melhorIter < numMaximoIterSemMelhora)
            {
                iterAtual++;

                resultadoFOMelhorVizinho = CalcularMelhorVizinhoBuscaTabu(solucaoAtual, distancias, iterAtual, resultadoFOMelhorVizinho, resultadoFOMelhorSolucao, matrizTabu, ref melhor_i, ref melhor_j);

                // Troca os elementos de acordo com a melhor vizinhança retornada
                int aux = solucaoAtual[melhor_i];
                solucaoAtual[melhor_i] = solucaoAtual[melhor_j];
                solucaoAtual[melhor_j] = aux;

                // Atualiza a matriz tabu com a nova restrição
                matrizTabu[melhor_i, melhor_j] = iterAtual + numIteracoesProibicao;
                matrizTabu[melhor_j, melhor_i] = iterAtual + numIteracoesProibicao;

                // Verifica se a solução vizinha é melhor que a melhor solução encontrada até o momento
                if (Math.Round(resultadoFOMelhorVizinho, 2) < Math.Round(resultadoFOMelhorSolucao, 2))
                {
                    melhorIter = iterAtual;
                    resultadoFOMelhorSolucao = resultadoFOMelhorVizinho;

                    int aux2 = melhorSolucao[melhor_i];
                    melhorSolucao[melhor_i] = melhorSolucao[melhor_j];
                    melhorSolucao[melhor_j] = aux2;

                    Array.Copy(solucaoAtual, melhorSolucao, solucaoAtual.Length);
                }
            }

            Array.Copy(melhorSolucao, solucaoAtual, solucaoAtual.Length);

            return resultadoFOMelhorSolucao;
        }


        private static double CalcularMelhorVizinhoBuscaTabu(int[] solucaoAtual, double[,] distancias, int iteracaoAtual, double resultadoFOAtual, double resultadoFOMelhorSolucao, 
            int[,] matrizRestricoes, ref int melhor_i, ref int melhor_j)
        {
            int aux;
            double resultadoFOVizinho;

            double resultadoFOMelhorVizinho = int.MaxValue;

            for (int i = 0; i < solucaoAtual.Length - 1; i++)
            {
                for (int j = i + 1; j < solucaoAtual.Length; j++)
                {
                    double delta1 = CalcularDelta(solucaoAtual.Length, solucaoAtual, distancias, i, j);

                    // Faz o movimento de troca da vizinhança
                    aux = solucaoAtual[j];
                    solucaoAtual[j] = solucaoAtual[i];
                    solucaoAtual[i] = aux;

                    double delta2 = CalcularDelta(solucaoAtual.Length, solucaoAtual, distancias, i, j);

                    resultadoFOVizinho = resultadoFOAtual - delta1 + delta2;
                    
                    if (matrizRestricoes[i, j] < iteracaoAtual || resultadoFOVizinho < resultadoFOMelhorSolucao)
                    {
                        if (resultadoFOVizinho < resultadoFOMelhorVizinho)
                        {
                            melhor_i = i;
                            melhor_j = j;
                            resultadoFOMelhorVizinho = resultadoFOVizinho;
                        }
                    }

                    // Desfaz o movimento de troca para analisar o restante da vizinhança
                    aux = solucaoAtual[j];
                    solucaoAtual[j] = solucaoAtual[i];
                    solucaoAtual[i] = aux;
                }
            }

            return resultadoFOMelhorVizinho;
        }

        #endregion

        #region [08] ILS - Iterate Local Search

        public static double ILSSmart(int[] solucaoAtual, double[,] distancias, int numMaximoIterSemMelhora, int numMaximoIterMesmoNivel)
        {
            int iterAtual = 1, melhorIter = 0, nivelAtual = 1, iterMesmoNivel = 1;
            int[] solucaoPerturbada;

            double resultadoFOsolucaoAtual = Util.Calculo.CalcularFuncaoObjetivo(solucaoAtual, distancias);
            while (iterAtual - melhorIter < numMaximoIterSemMelhora)
            {
                solucaoPerturbada = PerturbarVetor(solucaoAtual, nivelAtual);
                DescidaFirstImprovement(solucaoPerturbada, distancias);

                double resultadoFOsolucaoPerturbada = Util.Calculo.CalcularFuncaoObjetivo(solucaoPerturbada, distancias);
                if (resultadoFOsolucaoPerturbada < resultadoFOsolucaoAtual)
                {
                    Array.Copy(solucaoPerturbada, solucaoAtual, solucaoAtual.Length);
                    resultadoFOsolucaoAtual = resultadoFOsolucaoPerturbada;
                    melhorIter = iterAtual;
                    nivelAtual = 1;
                    iterMesmoNivel = 1;
                }
                else
                {
                    if (iterMesmoNivel >= numMaximoIterMesmoNivel)
                    {
                        nivelAtual++;
                        iterMesmoNivel = 1;
                    }
                    else
                    {
                        iterMesmoNivel++;
                    }
                }

                iterAtual++;
            }

            return resultadoFOsolucaoAtual;
        }

        private static int[] PerturbarVetor(int[] solucaoAtual, int nivelAtual)
        {
            int[] solucaoPerturbada = new int[solucaoAtual.Length];
            Array.Copy(solucaoAtual, solucaoPerturbada, solucaoAtual.Length);

            int posicao1 = 0, posicao2 = 0, aux = 0;

            int numeroTrocas = 0;
            while (numeroTrocas < nivelAtual)
            {
                Util.Calculo.CalcularDuasPosicoesAleatoriasDiferentes(0, solucaoAtual.Length, ref posicao1, ref posicao2);

                aux = solucaoPerturbada[posicao1];
                solucaoPerturbada[posicao1] = solucaoPerturbada[posicao2];
                solucaoPerturbada[posicao2] = aux;

                numeroTrocas++;
            }

            return solucaoPerturbada;
        }

        #endregion

        #region [ GRASP ]

        public static double Grasp(int[] solucaoAtual, double[,] distancias, double taxaAceitacaoSolucoes, int numMaximoIteracoes) //  = 0.1 1200
        {
            int iterAtual = 0;
            double resultadoFOMelhorSolucao = int.MaxValue;

            while (iterAtual < numMaximoIteracoes)
            {

            }

            return 0;
        }

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