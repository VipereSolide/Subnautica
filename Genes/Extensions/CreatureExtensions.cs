using VS.Subnautica.Genes.GeneAttributes;
using VS.Subnautica.Genes.Monos;

namespace VS.Subnautica.Genes.Extensions
{
    public static class CreatureExtensions
    {
        public static GeneAttributesHolder RegisterGenes(this Creature creature, GeneAttribute[] genes)
        {
            GeneAttributesHolder holder = creature.gameObject.AddComponent<GeneAttributesHolder>();
            holder.genes = GeneAttributeSample.FromGenes(genes, creature, false);

            return holder;
        }
    }
}
