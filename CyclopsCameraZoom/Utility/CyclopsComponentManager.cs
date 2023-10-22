namespace VS.Subnautica.CyclopsCameraZoom.Utility
{
    /// <summary>
    /// Responsible for storing information about all cyclops' scripts and components.
    /// </summary>
    public class CyclopsComponentManager
    {
        // Base
        public SubRoot subroot;

        // Scripts
        public CyclopsExternalCams externalCams;
        public CyclopsEntryHatch entryHatch;

        /// <summary>
        /// Creates a new CyclopsComponentManager from a subroot.
        /// </summary>
        public static CyclopsComponentManager FromSubRoot(SubRoot subroot)
        {
            CyclopsComponentManager result = new CyclopsComponentManager()
            {
                subroot = subroot,
            };

            return result;
        }

        /// <summary>
        /// Registers a new CyclopsExternalCams script for that CyclopsComponentManager.
        /// </summary>
        public CyclopsComponentManager WithExternalCameras(CyclopsExternalCams externalCams)
        {
            this.externalCams = externalCams;
            return this;
        }

        /// <summary>
        /// Registers a new CyclopsEntryHatch script for that CyclopsComponentManager.
        /// </summary>
        public CyclopsComponentManager WithEntryHatch(CyclopsEntryHatch entryHatch)
        {
            this.entryHatch = entryHatch;
            return this;
        }
    }
}
