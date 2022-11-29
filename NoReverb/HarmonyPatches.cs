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
            if (Configuration.DisableAreaReverb)
            {
                Logger.LogDebug("Disabling AreaReverb instance");
                __instance.enabled = false;
                return false;
            }
            return true;
        }
        
        // This is basically the reverb for everything that isn't a gunshot
        [HarmonyPrefix]
        [HarmonyPatch(typeof(AudioManager), nameof(AudioManager.GetMixerFromPreset))]
        public static void GetMixerFromPreset(object[] __args)
        {
            Logger.LogInfo($"Requested environment mixer for: {((AudioReverbPreset)__args[0]).ToString()}");
            if (Configuration.DisableOtherReverb)
            {
                Logger.LogInfo("Forcing environment reverb to AudioReverbPreset.Off");
                __args[0] = AudioReverbPreset.Off;
            }
        }

        // Reverb for gunshots
        [HarmonyPrefix]
        [HarmonyPatch(typeof(AudioManager), nameof(AudioManager.GetGunshotMixerFromPreset))]
        public static void GetGunshotMixerFromPreset_NoReverb(object[] __args)
        {
            Logger.LogInfo($"Requested gunshot mixer for: {((AudioReverbPreset)__args[0]).ToString()}");
            if (Configuration.DisableGunshotReverb)
            {
                Logger.LogInfo("Forcing gunshot reverb to AudioReverbPreset.Off");
                __args[0] = AudioReverbPreset.Off;
            }
        }

    }
}
