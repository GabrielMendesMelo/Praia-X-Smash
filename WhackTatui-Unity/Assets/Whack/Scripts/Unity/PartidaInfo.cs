using System.Collections.Generic;
using UnityEngine;

namespace Whack.Scripts.Unity
{
    public static class PartidaInfo
    {
        public static void Rodar()
        {
            Time.timeScale = 1;
        }

        public static void Parar()
        {
            Time.timeScale = 0;
        }

        public static Buraco[] Buracos
        {
            get
            {
                return Object.FindObjectsOfType<Buraco>();

            }
        }

        public static List<Buraco> BuracosDisponiveis
        {
            get
            {
                List<Buraco> disponiveis = new List<Buraco>();

                foreach (Buraco b in Buracos)
                {
                    if (!b.Ocupado)
                    {
                        disponiveis.Add(b);
                    }
                }
                return disponiveis;
            }
        }
    }
}
