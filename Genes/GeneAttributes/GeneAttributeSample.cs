using System.Collections.Generic;
using UnityEngine;
using static RootMotion.FinalIK.GenericPoser;

namespace VS.Subnautica.Genes.GeneAttributes
{
    public class GeneAttributeSample
    {
        public Creature creature;
        public GeneAttribute sample;
        public float currentValue;
        public bool isInitialized;

        public void Initialize()
        {
            if (isInitialized)
            {
                Debug.Log("Already initialized!");
                return;
            }

            currentValue = Mathf.Lerp(sample.min, sample.max, Random.Range(0, 1));
            sample.Execute(creature, Random.Range(0.5F, 1.5F));

            isInitialized = true;
        }

        public void Reset()
        {
            isInitialized = false;
        }

        private GeneAttributeSample(GeneAttribute gene, Creature creature, bool autoInitialize)
        {
            this.sample = gene;
            this.creature = creature;

            if (autoInitialize) Initialize();
        }

        public static GeneAttributeSample FromGene(GeneAttribute gene, Creature creature, bool autoInitialize)
        {
            GeneAttributeSample result = new GeneAttributeSample(gene, creature, autoInitialize);
            return result;
        }

        public static GeneAttributeSample[] FromGenes(GeneAttribute[] genes, Creature creature, bool autoInitialize)
        {
            List<GeneAttributeSample> results = new List<GeneAttributeSample>();

            foreach (GeneAttribute gene in genes)
                results.Add(FromGene(gene, creature, autoInitialize));

            return results.ToArray();
        }
    }
}
