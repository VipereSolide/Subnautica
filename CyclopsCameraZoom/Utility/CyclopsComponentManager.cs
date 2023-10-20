namespace VS.Subnautica.CyclopsCameraZoom.Utility
{
    public class CyclopsComponentManager
    {
        public SubRoot subroot;
        public CyclopsExternalCams externalCams;
        public CyclopsEntryHatch entryHatch;

        public static CyclopsComponentManager FromSubRoot(SubRoot subroot)
        {
            CyclopsComponentManager result = new CyclopsComponentManager()
            {
                subroot = subroot,
            };

            return result;
        }

        public CyclopsComponentManager WithExternalCameras(CyclopsExternalCams externalCams)
        {
            this.externalCams = externalCams;
            return this;
        }

        public CyclopsComponentManager WithEntryHatch(CyclopsEntryHatch entryHatch)
        {
            this.entryHatch = entryHatch;
            return this;
        }
    }
}
