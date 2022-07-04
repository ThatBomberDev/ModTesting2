using Reloaded.Hooks.ReloadedII.Interfaces;
using Reloaded.Mod.Interfaces;
using Reloaded.ModHelper;
using NoMansSky.Api;
using libMBIN;
using libMBIN.NMS.GameComponents;
using libMBIN.NMS.Globals;
using libMBIN.NMS.Toolkit;
using System.Windows.Forms;
using libMBIN.NMS;
using System.Net.Http;

namespace NoMansSky.ModTemplate
{
    /// <summary>
    /// Your mod logic goes here.
    /// </summary>
    public class Mod : NMSMod
    {

        private static readonly HttpClient client = new HttpClient();

        /// <summary>
        /// Initializes your mod along with some necessary info.
        /// </summary>
        public Mod(IModConfig _config, IReloadedHooks _hooks, IModLogger _logger) : base(_config, _hooks, _logger)
        {
            // This is how to log a message to the Reloaded II Console.
            Logger.WriteLine("Hello Simulation!");


            // The API relies heavily on Mod Events.
            // Below are 3 examples of using them.
            Game.OnProfileSelected += () => Logger.WriteLine("The player just selected a save file");
            Game.OnMainMenu += OnMainMenu;
            Game.OnGameJoined.AddListener(GameJoined);
            CurrentSystem.OnPlanetLoaded += planetLoaded;
            CurrentSystem.OnSystemLoaded += systemChanges;









        }



        /// <summary>
        /// Called once every frame.
        /// </summary>
        public override void Update()
        {

            

           


            // Here is an example of checking for keyboard keys
            if (Keyboard.IsPressed(Key.UpArrow))
            {

                

            }

            if (Game.IsInGame)
            {


                if (Keyboard.IsPressed(Key.L))
                {
                    graphicsTestEnable();

                }

                if (Keyboard.IsPressed(Key.K))
                {
                    graphicsTestDisable();

                }
            }

        

            if (Keyboard.IsPressed(Key.DownArrow))
            {
                //testingNonGlobalFields();


                var inventory = Player.Exosuit.GetInventory();
                var inventoryList = inventory.GetItems();
                foreach (var item in inventoryList)
                {
                    Logger.WriteLine($"The Player has: {item.Amount} Of {item.ID}");


                }


                




            }


            logicTest();
            randomGeneration();


            if (Keyboard.IsPressed(Key.LeftArrow))
            {
                

                var mbinList = Game.MBinManager.GetAllMBIN();
                foreach (var file in mbinList)
                {
                    var fileType = Game.MBinManager.GetMbinType(file.Name);
                    Logger.WriteLine($"Struct [{file.Name}] has an address of {file.Address}, and is of type: {fileType}");
                    
                    

                }
                structTest();

            }


        }

       private void systemChanges(long systemAddress)
        {
            Logger.WriteLine("Aquired System Address");

            var system = CurrentSystem.GetSystemData();
            system.Planets = 8;

            system.Name.Value = "Abyssal System: C0??uP7Ed";

            system.Light.SunColour.R = Random.Range(0.000f, 1.000f);
            system.Light.SunColour.G = Random.Range(0.000f, 1.000f);
            system.Light.SunColour.B = Random.Range(0.000f, 1.000f);
            system.Light.SunColour.A = Random.Range(0.000f, 1.000f);

            system.Light.LightColour.R = Random.Range(0.000f, 1.000f);
            system.Light.LightColour.G = Random.Range(0.000f, 1.000f);
            system.Light.LightColour.B = Random.Range(0.000f, 1.000f);
            system.Light.LightColour.A = Random.Range(0.000f, 1.000f);

            var systemColours = system.Colours.Palettes;
            foreach (var pallete in systemColours)
            {
                for (var i = 0; i < pallete.Colours.Length; i++)
                {
                    Colour testColour = new Colour();
                    testColour.A = Random.Range(0.000f, 1.000f);
                    testColour.R = Random.Range(0.000f, 1.000f);
                    testColour.G = Random.Range(0.000f, 1.000f);
                    testColour.B = Random.Range(0.000f, 1.000f);
                    pallete.Colours[i] = testColour;


                }


            }

            foreach (var planetPos in system.PlanetPositions)
            {
                var posRandomizer = new Vector3f();
                posRandomizer.x = Random.Range(0, 8388608);
                posRandomizer.y = Random.Range(0, 524288);
                posRandomizer.z = Random.Range(0, 8388608);
                planetPos.x = posRandomizer.x;
                planetPos.y = posRandomizer.y;
                planetPos.z = posRandomizer.z;


            }


        }

        private void planetLoaded(long planetAddress)
        {
            
            


            var planet = CurrentSystem.GetPlanetData(planetAddress);
            Logger.WriteLine($"Planet Loaded: {planet.Name.Value.ToString()}");



            for (var i = 0; i < planet.TileColours.Length; i++)
            {
                Colour testColour = new Colour();
                testColour.A = Random.Range(0.000f, 1.000f);
                testColour.R = Random.Range(0.000f, 1.000f);
                testColour.G = Random.Range(0.000f, 1.000f);
                testColour.B = Random.Range(0.000f, 1.000f);

                planet.TileColours[i] = testColour;


            }


            var alienChance = Random.Range(0, 7);

            if (alienChance == 0)
            {
                planet.InhabitingRace.AlienRace = GcAlienRace.AlienRaceEnum.Traders;
            }
            else if (alienChance == 1)
            {
                planet.InhabitingRace.AlienRace = GcAlienRace.AlienRaceEnum.Warriors;
            }
            else if (alienChance == 2)
            {
                planet.InhabitingRace.AlienRace = GcAlienRace.AlienRaceEnum.Explorers;
            }
            else if (alienChance == 3)
            {
                planet.InhabitingRace.AlienRace = GcAlienRace.AlienRaceEnum.Robots;
            }
            else if (alienChance == 4)
            {
                planet.InhabitingRace.AlienRace = GcAlienRace.AlienRaceEnum.Atlas;
            }
            else if (alienChance == 5)
            {
                planet.InhabitingRace.AlienRace = GcAlienRace.AlienRaceEnum.Diplomats;
            }
            else if (alienChance == 6)
            {
                planet.InhabitingRace.AlienRace = GcAlienRace.AlienRaceEnum.Exotics;
            }
            else if (alienChance == 7)
            {
                planet.InhabitingRace.AlienRace = GcAlienRace.AlienRaceEnum.None;
            }





            /*
            planet.CommonSubstanceID.Value = "AF_METAL";
            planet.UncommonSubstanceID.Value = "QUICKSILVER";
            planet.RareSubstanceID.Value = "TECHFRAG";
            */

            var atmosChance = Random.Range(0, 99);
            if (atmosChance < 19)
            {
                planet.Weather.AtmosphereType = GcPlanetWeatherData.AtmosphereTypeEnum.None;
            }
            else if(atmosChance > 19)
            {
                planet.Weather.AtmosphereType = GcPlanetWeatherData.AtmosphereTypeEnum.Normal;
            }

            var stormFreqChance = Random.Range(0, 2);
            if (stormFreqChance ==0)
            {
                planet.Weather.StormFrequency = GcPlanetWeatherData.StormFrequencyEnum.None;
            }
            else if(stormFreqChance == 1)
            {
                planet.Weather.StormFrequency = GcPlanetWeatherData.StormFrequencyEnum.Low;
            }
            else if (stormFreqChance == 2)
            {
                planet.Weather.StormFrequency = GcPlanetWeatherData.StormFrequencyEnum.High;
            }

            var weatherIntensityChance = Random.Range(0, 99);
            if(weatherIntensityChance > 19)
            {
                planet.Weather.WeatherIntensity = GcPlanetWeatherData.WeatherIntensityEnum.Default;
            }
            else if(weatherIntensityChance < 19)
            {
                planet.Weather.WeatherIntensity = GcPlanetWeatherData.WeatherIntensityEnum.Extreme;
            }

            var lifeChance = Random.Range(0, 3);
            if(lifeChance ==0)
            {
                planet.Life.LifeSetting = GcPlanetLife.LifeSettingEnum.Dead;
            }
            else if(lifeChance == 1)
            {
                planet.Life.LifeSetting = GcPlanetLife.LifeSettingEnum.Low;
            }
            else if(lifeChance == 2)
            {
                planet.Life.LifeSetting = GcPlanetLife.LifeSettingEnum.Mid;
            }
            else if(lifeChance == 3)
            {
                planet.Life.LifeSetting = GcPlanetLife.LifeSettingEnum.Full;
            }

            var creatureChance = Random.Range(0, 3);
            if(creatureChance ==0)
            {
                planet.CreatureLife.LifeSetting = GcPlanetLife.LifeSettingEnum.Dead;
            }
            else if(creatureChance == 1)
            {
                planet.CreatureLife.LifeSetting = GcPlanetLife.LifeSettingEnum.Low;
            }
            else if( creatureChance == 2)
            {
                planet.CreatureLife.LifeSetting = GcPlanetLife.LifeSettingEnum.Mid;
            }
            else if(creatureChance == 3)
            {
                planet.CreatureLife.LifeSetting = GcPlanetLife.LifeSettingEnum.Full;
            }

            var primeChance = Random.Range(0, 1);
            if(primeChance == 0)
            {
                planet.GenerationData.Prime = false;
            }
            else if(primeChance == 1)
            {
                planet.GenerationData.Prime = true;
            }

            var sizeChance = Random.Range(0, 3);
            if (sizeChance == 0)
            {
                planet.GenerationData.Size.PlanetSize = GcPlanetSize.PlanetSizeEnum.Large;
            }
            else if(sizeChance == 1)
            {
                planet.GenerationData.Size.PlanetSize = GcPlanetSize.PlanetSizeEnum.Medium;
            }
            else if (sizeChance ==2)
            {
                planet.GenerationData.Size.PlanetSize = GcPlanetSize.PlanetSizeEnum.Small;
            }
            else if (sizeChance == 3)
            {
                planet.GenerationData.Size.PlanetSize = GcPlanetSize.PlanetSizeEnum.Moon;
            }
            
            
            planet.Weather.WeatherType.Weather = GcWeatherOptions.WeatherEnum.Humid;
            planet.Weather.StormScreenFilter.ScreenFilter = GcScreenFilters.ScreenFilterEnum.FreighterAbandoned;
            planet.Weather.ScreenFilter.ScreenFilter = GcScreenFilters.ScreenFilterEnum.NewVintageBright;
            //planet.Weather.HeavyAir.Filename = "";

            planet.BuildingData.PlanetRadius = 20000f;

            var aeronChance = Random.Range(0, 2);
            if (aeronChance == 0)
            {
                planet.SentinelData.SentinelLevel = GcPlanetSentinelData.SentinelLevelEnum.Low;
                planet.SentinelData.MaxActiveDrones = Random.Range(1, 5);
            }
            else if (aeronChance == 1)
            {
                planet.SentinelData.SentinelLevel = GcPlanetSentinelData.SentinelLevelEnum.Default;
                planet.SentinelData.MaxActiveDrones = Random.Range(6, 11);

            }
            else if (aeronChance == 2)
            {
                planet.SentinelData.SentinelLevel = GcPlanetSentinelData.SentinelLevelEnum.Aggressive;
                planet.SentinelData.MaxActiveDrones = Random.Range(12, 17);

            }

            planet.FlybyTimer.x = 600;
            planet.FlybyTimer.y = 600;

            planet.Clouds.Seed.Seed = ((long)Random.Range(0, 9223372036854775807));
            Logger.WriteLine($"Cloud Seed: {planet.Clouds.Seed.Seed}");
            planet.Clouds.Seed.UseSeedValue = true;

            //Temp override for airless planets
            if(planet.GenerationData.Biome.Biome == GcBiomeType.BiomeEnum.Dead)
            {
                planet.Weather.AtmosphereType = GcPlanetWeatherData.AtmosphereTypeEnum.Normal;


            }

            

            var seedRandomizer = new GcSeed();
            seedRandomizer.Seed = Random.Range(0, 2147483647);
            seedRandomizer.UseSeedValue = true;
            planet.GenerationData.Seed = seedRandomizer;

            //planet.GenerationData.Biome.Biome = GcBiomeType.BiomeEnum.Lush;

            foreach(var index in planet.TileTypeIndices)
            {
                planet.TileTypeSet = Random.Range(0, index);
            }

            foreach(var feature in planet.Terrain.Features)
            {
                feature.Active = true;


            }

            planet.Terrain.MinimumCaveDepth = 100;

            var sandwormData = new GcCreatureSpawnData();
            sandwormData.Resource.Filename.Value = "MODELS/PLANETS/CREATURES/SANDWORM/SANDWORM.SCENE.MBIN";
            sandwormData.Resource.Seed.Seed = Random.Range(0, 2147483647);
            planet.SpawnData.Creatures.Add(sandwormData);

            foreach(var creature in planet.SpawnData.Creatures)
            {
                creature.AllowFur = true;
                creature.MaxScale = 10f;
                creature.MinScale = 0.5f;
                creature.CreatureMaxGroupSize = 50;
                creature.CreatureMinGroupSize = 20;
                creature.Herd = true;
            }

            planet.GenerationData.CreatureRoles.HasSandWorms = true;
            

            var planetColours = planet.Colours.Palettes;
            foreach (var pallete in planetColours)
            {
                for (var i = 0; i < pallete.Colours.Length; i++)
                {
                    Colour testColour = new Colour();
                    testColour.A = Random.Range(0.000f, 1.000f);
                    testColour.R = Random.Range(0.000f, 1.000f);
                    testColour.G = Random.Range(0.000f, 1.000f);
                    testColour.B = Random.Range(0.000f, 1.000f);
                    pallete.Colours[i] = testColour;


                }


            }
            CurrentSystem.SetPlanetData(planetAddress, planet);
        }

        private void structTest()
        {
            

            var memMgr = new MemoryManager();
            var missionTableAddress = Game.MBinManager.GetMbin("GcMissionTable").Address;
            var missionTableInMem = memMgr.GetValue<GcMissionTable>(missionTableAddress);
            Logger.WriteLine($"The Amount Of Missions Loaded Is: {missionTableInMem.Missions.Count}");
            for(var i=0; i< missionTableInMem.Missions.Count; i++)
            {
                Logger.WriteLine($"Loaded Mission: {missionTableInMem.Missions[i].MissionID}");


            }

            var combatEffectsTableAddress = Game.MBinManager.GetMbin("GcCombatEffectsTable").Address;
            var GcCombatEffectsTableInMem = memMgr.GetValue<GcCombatEffectsTable>(combatEffectsTableAddress);
            Logger.WriteLine($"Currently Loaded {GcCombatEffectsTableInMem.EffectsData.Length} effects");
            for(var i=0; i<GcCombatEffectsTableInMem.EffectsData.Length; i++)
            {
                Logger.WriteLine($"Combat Effect: {GcCombatEffectsTableInMem.EffectsData[i].ParticlesId.Value.ToString()} is active");


            }


        }
            
           

        private void graphicsTestEnable()
        {
            var memMgr = new MemoryManager();
            memMgr.SetValue("GcGraphicsGlobals.Redo_On", true);
            memMgr.SetValue("GcGraphicsGlobals.ShadowQuantized", false);
            memMgr.SetValue("GcGraphicsGlobals.DOFEnableBokeh", true);
            memMgr.SetValue("GcGraphicsGlobals.UseImposters", false);
            memMgr.SetValue("GcGraphicsGlobals.UseTaaResolve", true);
            //memMgr.SetValue("GcGraphicsGlobals.ApplyTaaTest", true);
            
            memMgr.SetValue("GcGraphicsGlobals.ShowTaaBuf", true);
            memMgr.SetValue("GcGraphicsGlobals.ShowTaaVarianceBuf", true);
            memMgr.SetValue("GcGraphicsGlobals.ShowTaaNVarianceBuf", true);
            memMgr.SetValue("GcGraphicsGlobals.ShowTaaNVarianceBuf", true);
            
            memMgr.SetValue("GcGraphicsGlobals.ForceStreamAllTextures", true);
            memMgr.SetValue("GcGraphicsGlobals.ForceEvictAllTextures", true);
            memMgr.SetValue("GcGraphicsGlobals.TargetTextureMemUsageMB", 1600);
            Logger.WriteLine("Applied Developer Graphical Settings");
        }

        private void graphicsTestDisable()
        {
            var memMgr = new MemoryManager();
            memMgr.SetValue("GcGraphicsGlobals.Redo_On", false);
            memMgr.SetValue("GcGraphicsGlobals.ShadowQuantized", true);
            memMgr.SetValue("GcGraphicsGlobals.DOFEnableBokeh", false);
            memMgr.SetValue("GcGraphicsGlobals.UseImposters", true);
            memMgr.SetValue("GcGraphicsGlobals.UseTaaResolve", false);
            //memMgr.SetValue("GcGraphicsGlobals.ApplyTaaTest", false);
            
            memMgr.SetValue("GcGraphicsGlobals.ShowTaaBuf", false);
            memMgr.SetValue("GcGraphicsGlobals.ShowTaaVarianceBuf", false);
            memMgr.SetValue("GcGraphicsGlobals.ShowTaaNVarianceBuf", false);
            memMgr.SetValue("GcGraphicsGlobals.ShowTaaNVarianceBuf", false);
            
            memMgr.SetValue("GcGraphicsGlobals.ForceStreamAllTextures", false);
            memMgr.SetValue("GcGraphicsGlobals.ForceEvictAllTextures", false);
            memMgr.SetValue("GcGraphicsGlobals.TargetTextureMemUsageMB", 1280);
            Logger.WriteLine("Disabled Developer Graphical Settings");
        }


        private void logicTest()
        {
            var initialShield = Game.Player.Shield;
            var memMgr = new MemoryManager();

            if (Game.IsInGame)
            {
                if (initialShield < 38)
                {


                    memMgr.SetValue("GcGameplayGlobals.WaterLandingDamageMultiplier", 0.000f);

                    memMgr.SetValue("GcPlayerGlobals.HealthRechargeMinTimeSinceDamage", 1);

                    memMgr.SetValue("GcPlayerGlobals.ShieldRechargeMinTimeSinceDamage", 1);

                    memMgr.SetValue("GcSpaceshipGlobals.ShieldRechargeMinHitTime", 1);

                    memMgr.SetValue("GcPlayerGlobals.WeaponZoomFOV", 0.5);

                    memMgr.SetValue("GcPlayerGlobals.WeaponChangeModeTime", 0.25);

                }

                else
                {
                    memMgr.SetValue("GcGameplayGlobals.WaterLandingDamageMultiplier", 0.333f);

                    memMgr.SetValue("GcPlayerGlobals.HealthRechargeMinTimeSinceDamage", 10);

                    memMgr.SetValue("GcPlayerGlobals.ShieldRechargeMinTimeSinceDamage", 30);

                    memMgr.SetValue("GcSpaceshipGlobals.ShieldRechargeMinHitTime", 60);

                    memMgr.SetValue("GcPlayerGlobals.WeaponZoomFOV", 0.7);

                    memMgr.SetValue("GcPlayerGlobals.WeaponChangeModeTime", 0.75);

                }

            }

        }

        private void randomGeneration()
        {
            if (Game.IsInGame)
            {
                var memMgr = new MemoryManager();

                memMgr.SetValue("GcTerrainGlobals.MinHighWaterLevel", Random.Range(0, 300));
                memMgr.SetValue("GcTerrainGlobals.MaxHighWaterLevel", Random.Range(300, 600));

                memMgr.SetValue("GcTerrainGlobals.NumGeneratorCalls", Random.Range(0, 16));
                memMgr.SetValue("GcTerrainGlobals.NumPolygoniseCalls", Random.Range(0, 16));
                memMgr.SetValue("GcTerrainGlobals.NumPostPolygoniseCalls", Random.Range(0, 16));
                //Disabling these since theyre buggy.

                //memMgr.SetValue("GcWaterGlobals.WaveHeight", Random.Range(0, 3));
                //memMgr.SetValue("GcWaterGlobals.WaveChoppiness", Random.Range(0, 4));

                memMgr.SetValue("GcSimulationGlobals.ProceduralBuildingsGenerationSeed", Random.Range(0, 2147483647));

                memMgr.SetValue("GcSolarGenerationGlobals.SolarSystemMaximumRadius", Random.Range(0, 2147483647));

                
                /*
                Colour newRed = memMgr.GetValue<Colour>("GcGameplayGlobals.ScannerColour1");
                newRed.R = Random.Range(0.000f, 1.000f);
                newRed.G = Random.Range(0.000f, 1.000f);
                newRed.B = Random.Range(0.000f, 1.000f);
                newRed.A = 1.000f;

                memMgr.SetValue("GcGameplayGlobals.ScannerColour1", newRed);
               */ 


            }
        }

      


        private void OnMainMenu()
        {
            Logger.WriteLine("Main Menu shown!");
            


        }

        private void GameJoined()
        {
            Logger.WriteLine("The game was joined!");
            Logger.WriteLine("Testing");
            

        }

        
       


    }
}