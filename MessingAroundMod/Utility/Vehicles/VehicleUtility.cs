namespace VipereSolide.Subnautica.MessingAroundMod.Utility.Vehicles
{
    public static class VehicleUtility
    {
        public static bool HasModuleInstalled(this Vehicle vehicle, TechType module)
        {
            if (vehicle == null)
                return false;

            foreach (string moduleName in vehicle.modules.equipment.Keys)
            {
                InventoryItem inventoryItem = vehicle.modules.equipment[moduleName];

                if (inventoryItem == null) continue;

                if (inventoryItem.techType == module)
                    return true;
            }

            return false;
        }

        public static TorpedoType GetTorpedo(SeaMoth seamoth, TechType type)
        {
            foreach (TorpedoType torpedo in seamoth.torpedoTypes)
                if (torpedo.techType == type) return torpedo;

            return null;
        }
    }
}
