# Onward No Reverb
A BepInEx plugin that allows you to modify (or remove) reverb from various sources in Onward 1.7.7

## How to install

1. [Install BepInEx](https://github.com/BepInEx/BepInEx/wiki/Installation) to Onward

2. Either [download a release](https://github.com/Zman2024/Onward-NoReverb/releases) or build from source

3. Put `NoReverb.dll` into `Onward/BepInEx/plugins`

## Configuration

Config is located at `Onward/BepInEx/config/Zman2024-NoReverb.cfg`

### AudioReverbPreset overrides
The following settings will override the `AudioReverbPreset` that is used by the specified audio mixer:

* `AreaReverbPreset` is for the AreaReverb scripts, it changes the reverb in a specified area
* `GunshotReverbPreset` is the reverb preset used for all gunshot sounds
* `GlobalReverbPreset` is the reverb for everything else

These seetings change if overrides are enabled / disabled:

* `DisableAreaReverb` completely disables all AreaReverb scripts
* `EnableAreaReverbOverride` enables the `AreaReverbPreset` override (does nothing if `DisableAreaReverb` is `true`)
* `EnableGunshotOverride` enables the `GunshotReverbPreset` override
* `EnableGlobalOverride` enables the `GlobalReverbPreset` override

