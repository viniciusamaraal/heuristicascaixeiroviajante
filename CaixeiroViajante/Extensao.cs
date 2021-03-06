﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaixeiroViajante
{
    public static class Extensao
    {
        public static void EmbaralharVetor(this int[] vetor) // Util.cpp -> embaralha_vetor
        {
            int aux, posicao1 = 0, posicao2 = 0;

            for (int i = 1; i < vetor.Length * 100; i++)
            {
                Util.Calculo.CalcularDuasPosicoesAleatoriasDiferentes(1, vetor.Length, ref posicao1, ref posicao2);

                aux = vetor[posicao1];
                vetor[posicao1] = vetor[posicao2];
                vetor[posicao2] = aux;
            }
        }
    }
}
