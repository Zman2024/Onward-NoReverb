using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using BepInEx;
using BepInEx.Configuration;
using UnityEngine;

namespace NoReverb
{
    public static class Configuration
    {
        private static ConfigFile _config = null;

        public static ConfigEntry<bool> EnableAreaReverbOverride;
        public static ConfigEntry<bool> DisableAreaReverb;
        public static ConfigEntry<bool> EnableGunshotOverride;
        public static ConfigEntry<bool> EnableGlobalOverride;

        public static ConfigEntry<AudioReverbPreset> AreaReverbPreset;
        public static ConfigEntry<AudioReverbPreset> GunshotReverbPreset;
        public static ConfigEntry<AudioReverbPreset> GlobalReverbPreset;

        // Config definitions //
        private static class Definitions
        {
            public static readonly string ConfigSection = "NoReverb";

            public static readonly ConfigDefinition EnableAreaReverbOverride = new ConfigDefinition(ConfigSection, "EnableAreaReverbOverride");
            public static readonly ConfigDescription EnableAreaReverbOverrideDesc = new ConfigDescription("Enables or disables the AreaReverbPreset override");

            public static readonly ConfigDefinition DisableAreaReverb = new ConfigDefinition(ConfigSection, "DisableAreaReverb");
            public static readonly ConfigDescription DisableAreaReverbDesc = new ConfigDescription("Disables the entire AreaReverb script from running");

            public static readonly ConfigDefinition EnableGunshotOverride = new ConfigDefinition(ConfigSection, "EnableGunshotOverride");
            public static readonly ConfigDescription EnableGunshotOverrideDesc = new ConfigDescription("Enables or disables the GunshotReverbPreset override");

            public static readonly ConfigDefinition EnableGlobalOverride = new ConfigDefinition(ConfigSection, "EnableMiscOverride");
            public static readonly ConfigDescription EnableGlobalOverrideDesc = new ConfigDescription("Enables or disables the OtherReverbPreset override");


            public static readonly ConfigDefinition AreaReverbPreset = new ConfigDefinition(ConfigSection, "AreaReverbPreset");
            public static readonly ConfigDescription AreaReverbPresetDesc = new ConfigDescription("The AudioReverbPreset to use for all AreaReverb scripts, AreaReverb is for reverb in areas that it was explicitly added in");

            public static readonly ConfigDefinition GunshotReverbPreset = new ConfigDefinition(ConfigSection, "GunshotReverbPreset");
            public static readonly ConfigDescription GunshotReverbPresetDesc = new ConfigDescription("Changes the reverb preset on gunfire");

            public static readonly ConfigDefinition GlobalReverbPreset = new ConfigDefinition(ConfigSection, "OtherReverbPreset");
            public static readonly ConfigDescription GlobalReverbPresetDesc = new ConfigDescription("Changes the reverb preset on everything that isn't gunfire");
            
        }

        public static void LoadConfig(ConfigFile cfg)
        {
            if (_config != null) return;
            _config = cfg;
            CreateConfigBindings(cfg);
        }

        private static void CreateConfigBindings(ConfigFile cfg)
        {
            DisableAreaReverb = cfg.Bind(Definitions.DisableAreaReverb, false, Definitions.DisableAreaReverbDesc);
            EnableAreaReverbOverride = cfg.Bind(Definitions.EnableAreaReverbOverride, true, Definitions.EnableAreaReverbOverrideDesc);
            EnableGunshotOverride = cfg.Bind(Definitions.EnableGunshotOverride, true, Definitions.EnableGunshotOverrideDesc);
            EnableGlobalOverride = cfg.Bind(Definitions.EnableGlobalOverride, true, Definitions.EnableGlobalOverrideDesc);

            AreaReverbPreset = cfg.Bind(Definitions.AreaReverbPreset, AudioReverbPreset.Generic, Definitions.AreaReverbPresetDesc);
            GunshotReverbPreset = cfg.Bind(Definitions.GunshotReverbPreset, AudioReverbPreset.Generic, Definitions.GunshotReverbPresetDesc);
            GlobalReverbPreset = cfg.Bind(Definitions.GlobalReverbPreset, AudioReverbPreset.Generic, Definitions.GlobalReverbPresetDesc);
        }

    }

    [BepInPlugin("Zman2024-NoReverb", "No Reverb", "0.1.2")]
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
