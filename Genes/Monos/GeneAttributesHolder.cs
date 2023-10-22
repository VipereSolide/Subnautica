using VS.Subnautica.Genes.GeneAttributes;
using UnityEngine;

namespace VS.Subnautica.Genes.Monos
{
    public class GeneAttributesHolder : MonoBehaviour
    {
        public GeneAttributeSample[] genes;

        public void Start()
        {
            if (genes == null || genes.Length == 0)
            {
                Debug.LogWarning($"Cannot initialize with null gene attributes!");
                return;
            }

            foreach(GeneAttributeSample geneSample in genes)
            {
                geneSample.Initialize();
            }
        }
    }
}
