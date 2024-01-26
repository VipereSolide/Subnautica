using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensive.Patches
{
    public static class ResourceTrackerDatabaseExtension
    {
        static readonly List<TechType> ExtendedTechTypes = new List<TechType>()
        {
            TechType.Warper,
            TechType.GhostLeviathan,
            TechType.GhostLeviathanJuvenile,
            TechType.ReaperLeviathan,
            TechType.SeaDragon,
        };

        public static void ExtendResourceTypes()
        {
            for (int i = 0; i < ExtendedTechTypes.Count; i++)
                ResourceTrackerDatabase.resources.GetOrAddNew(ExtendedTechTypes[i]);
        }
    }
}
