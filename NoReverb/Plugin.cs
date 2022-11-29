using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using BepInEx;
using BepInEx.Configuration;

namespace NoReverb
{
    public static class Configuration
    {
        private static ConfigFile _config = null;

        public static bool DisableAreaReverb = true;

        public static bool DisableGunshotReverb = true;

        public static bool DisableOtherReverb = true;

        // Config definitions //
        private static readonly string ConfigSection = "NoReverb";
        private static readonly ConfigDefinition areaReverb = new ConfigDefinition(ConfigSection, "DisableAreaReverb");
        private static readonly ConfigDefinition gunshotReverb = new ConfigDefinition(ConfigSection, "DisableGunshotReverb");
        private static readonly ConfigDefinition otherReverb = new ConfigDefinition(ConfigSection, "DisableOtherReverb");

        public static void LoadConfig(ConfigFile cfg)
        {
            _config = cfg;
            CreateConfigBindings(cfg);

            DisableAreaReverb = (bool)_config[areaReverb].BoxedValue;
            DisableGunshotReverb = (bool)_config[gunshotReverb].BoxedValue;
            DisableOtherReverb = (bool)_config[otherReverb].BoxedValue;
        }

        public static void CreateConfigBindings(ConfigFile cfg)
        {
            cfg.Bind(areaReverb, true, new ConfigDescription("Disables the AreaReverb script from running, stopping reverb in areas that it was explicitly added in"));
            cfg.Bind(gunshotReverb, true, new ConfigDescription("Disables reverb on gunshots"));
            cfg.Bind(otherReverb, true, new ConfigDescription("Disables reverb on everything that isn't a gunshot"));
        }

    }

    [BepInPlugin("Zman2024-NoReverb", "No Reverb", "0.1.1")]
    public class Plugin : BaseUnityPlugin
    {
        public void Start()
        {
            HarmonyPatches.Logger = this.Logger;
            try
            {
                Logger.LogInfo("Loading config...");
                Configuration.LoadConfig(Config);

                Logger.LogInfo("Applying harmony patches...");
                Harmony.CreateAndPatchAll(typeof(HarmonyPatches));
                
                Logger.LogInfo("Finished");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return;
            }
        }

    }
}
