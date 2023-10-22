using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace VS.Subnautica.Genes.GeneAttributes
{
    public class GeneAttribute
    {
        public string name;
        public float min, max;

        public virtual float Get() => Mathf.Lerp(min, max, Random.Range(0, 1));
        public Action<Creature, float> execute;

        public GeneAttribute(string name) =>
            this.name = name;

        public GeneAttribute WithMinMax(float min, float max)
        {
            this.min = min;
            this.max = max;

            return this;
        }

        public GeneAttribute WithOnExecute(Action<Creature, float> execute)
        {
            this.execute = execute;
            return this;
        }

        public virtual void Execute(Creature creature, float value) =>
            execute?.Invoke(creature, value);
    }
}