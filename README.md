# Onward-NoReverb
A BepInEx plugin to remove reverb in Onward 1.7.7

<h2> What does this do? </h2>
This mod allows you to remove reverb from various sources

<h2> How to install </h2>

1. Install BepInEx to Onward (https://github.com/BepInEx/BepInEx/wiki/Installation)

2. Either download a release (https://github.com/Zman2024/Onward-NoReverb/releases) or build from source

3. Put `NoReverb.dll` into `Onward/BepInEx/plugins`

<h2>Config stuff</h2>

Config is located at `Onward/BepInEx/config/Zman2024-NoReverb.cfg`

There are 3 different settings:

1. `DisableAreaReverb` disables the area reverb script which defines reverb in areas that are different from the map's reverb (like a tunnel / small room)

2. `DisableGunshotReverb` disables the reverb for gunshots

3. `DisableOtherReverb` disables the reverb for everything that isn't a gunshot
