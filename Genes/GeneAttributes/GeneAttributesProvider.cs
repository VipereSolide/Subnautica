using System;
using System.Collections.Generic;
using UnityEngine;

namespace VS.Subnautica.Genes.GeneAttributes
{
    public static class GeneAttributesProvider
    {
        public struct GeneAttributeCouple
        {
            public GeneAttribute gene;
            public Type type;

            public GeneAttributeCouple(Type type, GeneAttribute gene)
            {
                this.type = type;
                this.gene = gene;
            }

            public static GeneAttributeCouple WithAnyType(GeneAttribute gene)
            {
                return new GeneAttributeCouple(typeof(Creature), gene);
            }
        }

        public static readonly List<GeneAttributeCouple> geneAttributeChecklist = new List<GeneAttributeCouple>()
        {
            GeneAttributeCouple.WithAnyType(new GeneAttribute("Scale")
                .WithMinMax(0.1F, 10F)
                .WithOnExecute((Creature creature, float value) =>
                {
                    creature.transform.localScale = Vector3.one * value;
                })
            ),
        };

        public static GeneAttribute GetFirstGeneAttributeFromType(Type type)
        {
            foreach (GeneAttributeCouple couple in geneAttributeChecklist)
            {
                if (couple.type == type)
                {
                    return couple.gene;
                }
            }

            Debug.LogWarning($"Could not find gene attribute from type \"{type.Name}\"!");
            return null;
        }
        public static GeneAttribute[] GetAllGeneAttributesFromType(Type type)
        {
            List<GeneAttribute> result = new List<GeneAttribute>();

            foreach (GeneAttributeCouple couple in geneAttributeChecklist)
            {
                if (couple.type == typeof(Creature) || couple.type == type)
                {
                    result.Add(couple.gene);
                }
            }

            return result.ToArray();
        }
    }
}
