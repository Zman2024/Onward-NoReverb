using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;

namespace NoReverb
{
    [HarmonyPatch]
    public static class HarmonyPatches
    {
        public static BepInEx.Logging.ManualLogSource Logger;

        // AreaReverb is the cause of all spot/spatial reverb
        [HarmonyPrefix]
        [HarmonyPatch(typeof(AreaReverb), "Start")]
        public static bool AreaReverb_Start(AreaReverb __instance)
        {
            if (Configuration.DisableAreaReverb.Value)
            {
                Logger.LogDebug("Disabling AreaReverb instance");
                __instance.enabled = false;
                return false;
            }
            if (Configuration.EnableAreaReverbOverride.Value)
            {
                Logger.LogDebug($"Replacing AreaReverb's preset: {__instance.AreaPreset} with {Configuration.AreaReverbPreset.Value}");
                __instance.AreaPreset = Configuration.AreaReverbPreset.Value;
            }
            return true;
        }
        
        // This is basically the reverb for everything that isn't a gunshot
        [HarmonyPrefix]
        [HarmonyPatch(typeof(AudioManager), nameof(AudioManager.GetMixerFromPreset))]
        public static void GetMixerFromPreset(object[] __args)
        {
            AudioReverbPreset preset = ((AudioReverbPreset)__args[0]);
            if (Configuration.EnableMiscOverride.Value)
            {
                Logger.LogDebug($"Forcing environment reverb to {Configuration.OtherReverbPreset.Value} instead of {preset}");
                __args[0] = Configuration.OtherReverbPreset.Value;
            }
        }

        // Reverb for gunshots
        [HarmonyPrefix]
        [HarmonyPatch(typeof(AudioManager), nameof(AudioManager.GetGunshotMixerFromPreset))]
        public static void GetGunshotMixerFromPreset_NoReverb(object[] __args)
        {
            AudioReverbPreset preset = ((AudioReverbPreset)__args[0]);
            if (Configuration.EnableGunshotOverride.Value)
            {
                Logger.LogDebug($"Forcing gunshot reverb to {Configuration.GunshotReverbPreset.Value} instead of {preset}");
                __args[0] = Configuration.GunshotReverbPreset.Value;
            }
        }

    }
}
