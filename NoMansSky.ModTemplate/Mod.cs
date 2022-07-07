using Reloaded.Hooks.ReloadedII.Interfaces;
using Reloaded.Mod.Interfaces;
using Reloaded.ModHelper;
using NoMansSky.Api;
using libMBIN;
using libMBIN.NMS.GameComponents;
using libMBIN.NMS.Globals;
using libMBIN.NMS.Toolkit;
using libMBIN.NMS;


namespace NoMansSky.ModTemplate
{
    /// <summary>
    /// Your mod logic goes here.
    /// </summary>
    public class Mod : NMSMod
    {

        
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
            CurrentSystem.OnPlanetLoaded.AddListener(planet => planetLoaded(planet));
            Game.OnEnvironmentObjectLoaded.AddListener(environmentObject => envLoaded(environmentObject));
            Game.SpaceColors.OnColorLoaded.AddListener(spaceColours => coloursLoaded(spaceColours));








        }



        /// <summary>
        /// Called once every frame.
        /// </summary>
        public override void Update()
        {
            if(Keyboard.IsPressed(Key.UpArrow))
            {
                foreach(var planet in CurrentSystem.Planets)
                {
                    var planetData = planet.GetPlanetData();
                    Logger.WriteLine($"Planet {planetData.Name.Value.ToString()} has a seed {planetData.GenerationData.Seed.Seed.ToHex()}");

                    
                }

            }

            var spaceColours = Game.SpaceColors.DefaulColorSettings.GetValue();
            foreach (var setting in spaceColours.Settings)
            {
                var colourRandomizer = new Colour();
                colourRandomizer.R = 1.000f;
                colourRandomizer.G = 0.000f;
                colourRandomizer.B = 0.518f;
                colourRandomizer.A = 1.000f;

                /*
                colourRandomizer.R = Random.Range(0.000f, 1.000f);
                colourRandomizer.G = Random.Range(0.000f, 1.000f);
                colourRandomizer.B = Random.Range(0.000f, 1.000f);
                colourRandomizer.A = Random.Range(0.000f, 1.000f);
                */

                setting.BottomColour = colourRandomizer;
                setting.BottomColourPlanet = colourRandomizer;
                setting.CloudColour = colourRandomizer;
                setting.FogColour = colourRandomizer;
                setting.FogColour2 = colourRandomizer;
                setting.LightColour = colourRandomizer;
                setting.MidColour = colourRandomizer;
                setting.MidColourPlanet = colourRandomizer;
                setting.NebulaColour1 = colourRandomizer;
                setting.NebulaColour2 = colourRandomizer;
                setting.NebulaColour3 = colourRandomizer;
                setting.TopColour = colourRandomizer;
                setting.TopColourPlanet = colourRandomizer;
                Logger.WriteLine($"Randomized Space Colours");
            }
            Game.SpaceColors.DefaulColorSettings.SetValue(spaceColours);
            

        

            if (Keyboard.IsPressed(Key.DownArrow))
            {
               


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
                

                var mbinList = Game.MBinManager.GetAllMBin();
                foreach (var file in mbinList)
                {
                    var fileType = Game.MBinManager.GetMBinType(file.MBinName);
                    Logger.WriteLine($"Struct [{file.MBinName}] has an address of {file.Address}, and is of type: {fileType}");
                    
                    

                }
                structTest();

            }


        }

        private void coloursLoaded(ISpaceColorSetting _spaceColors)
        {
            _spaceColors.Modify(colourData =>
            {
                foreach(var setting in colourData.Settings)
                {
                    var colourRandomizer = new Colour();
                    colourRandomizer.R = 1.000f;
                    colourRandomizer.G = 0.000f;
                    colourRandomizer.B = 0.518f;
                    colourRandomizer.A = 1.000f;
                    
                    /*
                    colourRandomizer.R = Random.Range(0.000f, 1.000f);
                    colourRandomizer.G = Random.Range(0.000f, 1.000f);
                    colourRandomizer.B = Random.Range(0.000f, 1.000f);
                    colourRandomizer.A = Random.Range(0.000f, 1.000f);
                    */

                    setting.BottomColour = colourRandomizer;
                    setting.BottomColourPlanet = colourRandomizer;
                    setting.CloudColour = colourRandomizer;
                    setting.FogColour = colourRandomizer;
                    setting.FogColour2 = colourRandomizer;
                    setting.LightColour = colourRandomizer;
                    setting.MidColour = colourRandomizer;
                    setting.MidColourPlanet = colourRandomizer;
                    setting.NebulaColour1 = colourRandomizer;
                    setting.NebulaColour2 = colourRandomizer;
                    setting.NebulaColour3 = colourRandomizer;
                    setting.TopColour = colourRandomizer;
                    setting.TopColourPlanet = colourRandomizer;
                    Logger.WriteLine($"Randomized Space Colours");





                }
            });


            
        }

        //Testing Env Objects
        private void envLoaded(IEnvironmentObject environmentObject)
        {


            environmentObject.ModifySpawnData(objData =>
                {
                    foreach(var creature in objData.Creatures)
                    {
                        Logger.WriteLine($"Modifying {creature.CreatureID} creature");
                        creature.AllowFur = true;
                        creature.CreatureMaxGroupSize = 200;
                        creature.CreatureMinGroupSize = 100;
                        creature.MaxScale = 500;
                        creature.MinScale = 100;
                        //creature.Resource.AltId = "_VERS_1 _HEADB_1 _TOPB_1 _EYESE_1 _BRHACC_7A _TREX_4 _RHIHACC_10";
                    }
                    

                 

                }

                );
            


        }

        private void planetLoaded(IPlanet planet)
        {
            planet.ModifyPlanetData(planetData =>
            {
                //Testing
                Logger.WriteLine($"{planetData.GenerationData.Biome.Biome.ToString()} Planet {planetData.Name.Value} has been loaded");
                
                

                //Testing Names
                var nameString = new NMSString0x80();
                nameString = "Testing Name";
                planetData.Name = nameString;

                //Randomize Colours
                foreach(var pallette in planetData.Colours.Palettes)
                {
                    for(var i=0; i< pallette.Colours.Length; i++)
                    {
                        var colourRandomizer = new Colour();
                        colourRandomizer.R = Random.Range(0.000f, 1.000f);
                        colourRandomizer.G = Random.Range(0.000f, 1.000f);
                        colourRandomizer.B = Random.Range(0.000f, 1.000f);
                        colourRandomizer.A = Random.Range(0.000f, 1.000f);
                        pallette.Colours[i] = colourRandomizer;

                        
                    }

                }

                //Randomize Atmos
                var atmosChance = Random.Range(0, 100);
                if (atmosChance < 30)
                {
                    planetData.Weather.AtmosphereType = GcPlanetWeatherData.AtmosphereTypeEnum.None;
                }
                else if(atmosChance > 30)
                {
                    planetData.Weather.AtmosphereType = GcPlanetWeatherData.AtmosphereTypeEnum.Normal;
                }
                
                //Randomize Sentinels
                var aeronChance = Random.Range(0, 2);
                if(aeronChance == 0)
                {
                    planetData.SentinelData.SentinelLevel = GcPlanetSentinelData.SentinelLevelEnum.Low;
                    planetData.SentinelData.MaxActiveDrones = Random.Range(0, 8);
                }
                else if(aeronChance == 1)
                {
                    planetData.SentinelData.SentinelLevel = GcPlanetSentinelData.SentinelLevelEnum.Default;
                    planetData.SentinelData.MaxActiveDrones = Random.Range(9, 16);
                }
                else if (aeronChance == 2)
                {
                    planetData.SentinelData.SentinelLevel = GcPlanetSentinelData.SentinelLevelEnum.Aggressive;
                    planetData.SentinelData.MaxActiveDrones = Random.Range(17, 24);
                }

                //Randomize Resource Chance
                var resourceChance  = Random.Range(0, 1);
                if(resourceChance == 0)
                {
                    planetData.ResourceLevel = GcPlanetData.ResourceLevelEnum.Low;
                }
                else if (resourceChance == 1)
                {
                    planetData.ResourceLevel = GcPlanetData.ResourceLevelEnum.High;
                }

                //Randomize Life (Flora) Chance
                var lifeChance = Random.Range(0, 3);
                if(lifeChance == 0)
                {
                    planetData.Life.LifeSetting = GcPlanetLife.LifeSettingEnum.Dead;
                }
                else if (lifeChance == 1)
                {
                    planetData.Life.LifeSetting = GcPlanetLife.LifeSettingEnum.Low;
                }
                else if (lifeChance == 2)
                {
                    planetData.Life.LifeSetting = GcPlanetLife.LifeSettingEnum.Mid;
                }
                else if (lifeChance == 2)
                {
                    planetData.Life.LifeSetting = GcPlanetLife.LifeSettingEnum.Full;
                }

                //Randomize Creature(Fauna) Chance
                var creatureChance = Random.Range(0, 3);
                if (creatureChance == 0)
                {
                    planetData.CreatureLife.LifeSetting = GcPlanetLife.LifeSettingEnum.Dead;
                }
                else if (creatureChance == 1)
                {
                    planetData.CreatureLife.LifeSetting = GcPlanetLife.LifeSettingEnum.Low;
                }
                else if (creatureChance == 2)
                {
                    planetData.CreatureLife.LifeSetting = GcPlanetLife.LifeSettingEnum.Mid;
                }
                else if (creatureChance == 2)
                {
                    planetData.CreatureLife.LifeSetting = GcPlanetLife.LifeSettingEnum.Full;
                }

                //Activate All Terrain Features
                foreach(var feature in planetData.Terrain.Features)
                {
                    feature.Active = true;
                }

                //Randomize Building Density
                var buildingChance = Random.Range(0, 5);
                if (buildingChance == 0)
                {
                    planetData.BuildingLevel.BuildingDensity = GcBuildingDensityLevels.BuildingDensityEnum.Dead;
                }
                else if (buildingChance == 1)
                {
                    planetData.BuildingLevel.BuildingDensity = GcBuildingDensityLevels.BuildingDensityEnum.Low;
                }
                else if (buildingChance == 2)
                {
                    planetData.BuildingLevel.BuildingDensity = GcBuildingDensityLevels.BuildingDensityEnum.Mid;
                }
                else if (buildingChance == 3)
                {
                    planetData.BuildingLevel.BuildingDensity = GcBuildingDensityLevels.BuildingDensityEnum.Full;
                }
                else if (buildingChance == 4)
                {
                    planetData.BuildingLevel.BuildingDensity = GcBuildingDensityLevels.BuildingDensityEnum.Weird;
                }
                else if (buildingChance == 4)
                {
                    planetData.BuildingLevel.BuildingDensity = GcBuildingDensityLevels.BuildingDensityEnum.HalfWeird;
                }

                //Randomize Alien Race Chance
                var alienChance = Random.Range(0, 2);
                if(alienChance == 0)
                {
                    planetData.InhabitingRace.AlienRace = GcAlienRace.AlienRaceEnum.Traders;
                }
                else if (alienChance == 1)
                {
                    planetData.InhabitingRace.AlienRace = GcAlienRace.AlienRaceEnum.Warriors;
                }
                else if (alienChance == 2)
                {
                    planetData.InhabitingRace.AlienRace = GcAlienRace.AlienRaceEnum.Explorers;
                }

                //Randomize Water HeavyAir Colours
                foreach(var heavyAirColour in planetData.Water.HeavyAir.Colours)
                {
                    var colourRandomizer = new Colour();
                    colourRandomizer.R = Random.Range(0.000f, 1.000f);
                    colourRandomizer.G = Random.Range(0.000f, 1.000f);
                    colourRandomizer.B = Random.Range(0.000f, 1.000f);
                    colourRandomizer.A = Random.Range(0.000f, 1.000f);
                    heavyAirColour.Colour1 = colourRandomizer;
                    heavyAirColour.Colour2 = colourRandomizer;

                }

                //Randomize Weather HeavyAir Colours
                foreach(var weatherHeavyAir in planetData.Weather.HeavyAir.Colours)
                {
                    var colourRandomizer = new Colour();
                    colourRandomizer.R = Random.Range(0.000f, 1.000f);
                    colourRandomizer.G = Random.Range(0.000f, 1.000f);
                    colourRandomizer.B = Random.Range(0.000f, 1.000f);
                    colourRandomizer.A = Random.Range(0.000f, 1.000f);
                    weatherHeavyAir.Colour1 = colourRandomizer;
                    weatherHeavyAir.Colour2 = colourRandomizer;

                }

                //Randomize Tile Colours
                for(var i=0;i<planetData.TileColours.Length;i++)
                {
                    var colourRandomizer = new Colour();
                    colourRandomizer.R = Random.Range(0.000f, 1.000f);
                    colourRandomizer.G = Random.Range(0.000f, 1.000f);
                    colourRandomizer.B = Random.Range(0.000f, 1.000f);
                    colourRandomizer.A = Random.Range(0.000f, 1.000f);
                    planetData.TileColours[i] = colourRandomizer;
                }

                //Change Screen Filters
                planetData.Weather.ScreenFilter.ScreenFilter = GcScreenFilters.ScreenFilterEnum.FreighterAbandoned;
                planetData.Weather.StormScreenFilter.ScreenFilter = GcScreenFilters.ScreenFilterEnum.Nexus;

                //Enlarge Caves
                planetData.Terrain.MinimumCaveDepth = 100;

            }
            );
            
          
        }

        private void structTest()
        {
            

            var memMgr = new MemoryManager();
            var missionTableAddress = Game.MBinManager.GetMBin("GcMissionTable").Address;
            var missionTableInMem = memMgr.GetValue<GcMissionTable>(missionTableAddress);
            Logger.WriteLine($"The Amount Of Missions Loaded Is: {missionTableInMem.Missions.Count}");
            for(var i=0; i< missionTableInMem.Missions.Count; i++)
            {
                Logger.WriteLine($"Loaded Mission: {missionTableInMem.Missions[i].MissionID}");


            }

            var combatEffectsTableAddress = Game.MBinManager.GetMBin("GcCombatEffectsTable").Address;
            var GcCombatEffectsTableInMem = memMgr.GetValue<GcCombatEffectsTable>(combatEffectsTableAddress);
            Logger.WriteLine($"Currently Loaded {GcCombatEffectsTableInMem.EffectsData.Length} effects");
            for(var i=0; i<GcCombatEffectsTableInMem.EffectsData.Length; i++)
            {
                Logger.WriteLine($"Combat Effect: {GcCombatEffectsTableInMem.EffectsData[i].ParticlesId.Value.ToString()} is active");


            }


        }
            
           
        /*
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
        */

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

                memMgr.SetValue("GcTerrainGlobals.MinHighWaterLevel",  Random.Range(0, 300));
                memMgr.SetValue("GcTerrainGlobals.MaxHighWaterLevel",  Random.Range(300, 600));

                memMgr.SetValue("GcTerrainGlobals.NumGeneratorCalls",  Random.Range(0, 16));
                memMgr.SetValue("GcTerrainGlobals.NumPolygoniseCalls",  Random.Range(0, 16));
                memMgr.SetValue("GcTerrainGlobals.NumPostPolygoniseCalls",  Random.Range(0, 16));
               

                memMgr.SetValue("GcSimulationGlobals.ProceduralBuildingsGenerationSeed",  Random.Range(0, 2147483647));

                memMgr.SetValue("GcSolarGenerationGlobals.SolarSystemMaximumRadius",  Random.Range(0, 2147483647));

                
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