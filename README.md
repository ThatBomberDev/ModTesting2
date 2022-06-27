# NoMansSky Logic Based Mod Testing
A Testing Mod for No Man's Sky using [Reloaded 2](https://github.com/Reloaded-Project/Reloaded-II/releases/latest) and the [No Man's Sky API](https://github.com/gurrenm3/NoMansSky.Api)!

For more info on modding No Mans Sky, check out the modding wiki here: [https://github.com/gurrenm3/NoMansSky.Api/wiki](https://github.com/gurrenm3/NoMansSky.Api/wiki)

## Goals
1) Setting up an in game menu to tweak global settings and export changed values into files to parse back into the mod
2) Change certain global values based on events (ex weapon damage increase when health is low)
3) Access internal classes and write methods to change values in them according to conditions.
4) Rewrite older .Pak mods in this format, potentially in a more dynamic way (Settlement settings changing based on units, Plant timers adjusting based on planet biome, Starship speeds and movement changing based on planet data, Pirate/ Enemy starship waves & difficulties adjusting to player weapons).
