using UnityEngine;
using System;

namespace Utilidades
{
    public static class Ordenacao
    {
        public static void HeapSort(int[] sortValues)
        {
            int tamanho = sortValues.Length;
            int i = tamanho / 2;
            int pai, filho, t;

            while (true)
            {
                if (i > 0)
                {
                    i--;
                    t = sortValues[i];
                }
                else
                {
                    tamanho--;
                    if (tamanho <= 0)
                    {
                        return;
                    }
                    t = sortValues[tamanho];
                    sortValues[tamanho] = sortValues[0];
                }
                pai = i;
                filho = ((i * 2) + 1);

                while (filho < tamanho)
                {
                    if ((filho + 1 < tamanho) && (sortValues[filho + 1] > sortValues[filho]))
                    {
                        filho++;
                    }
                    if (sortValues[filho] > t)
                    {
                        sortValues[pai] = sortValues[filho];
                        pai = filho;
                        filho = pai * 2 + 1;
                    }
                    else
                    {
                        break;
                    }
                }
                sortValues[pai] = t;
            }
        }
    }

    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.Items;
        }

        public static string ToJson<T>(T[] array, bool prettyPrint)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper, prettyPrint);
        }

        [Serializable]
        private class Wrapper<T>
        {
            public T[] Items;
        }
    }
}
