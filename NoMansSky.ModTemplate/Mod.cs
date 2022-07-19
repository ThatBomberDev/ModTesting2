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
            Game.Colors.DaySkyColors.OnLoaded.AddListener(dayColours);
            Game.Colors.DuskSkyColors.OnLoaded.AddListener(duskColours);
            Game.Colors.NightSkyColors.OnLoaded.AddListener(nightColours);
            //Game.CurrentSystem.OnSystemLoaded.AddListener(systemLoaded);
            Game.Colors.SpaceSkyColors.OnLoaded.AddListener(spaceNebulas);

            Game.Colors.WaterColors.OnLoaded.AddListener(waterColours);

            Game.Reality.Products_NMS.OnLoaded.AddListener(createItems);
            Game.Reality.FrigateFlybyTable.OnLoaded.AddListener(setFlybys);
            Game.Reality.PurchaseableSpecials.OnLoaded.AddListener(setSpecials);
            



        }

        
        private void randomizeGalaxyMapColours()
        {
            var galaxyGlobal = Game.Globals.GalaxyGlobals.GetValue<GcGalaxyGlobals>();
            for(var i = 0; i< galaxyGlobal.DefaultRenderSetup.MapLargeAreaPrimaryDefaultColours.Length; i++)
            {
                galaxyGlobal.DefaultRenderSetup.MapLargeAreaPrimaryDefaultColours[i] = colourRandomizer();
            }

        }

        private void setSpecials()
        {
            var specialTable = Game.Reality.PurchaseableSpecials.GetValue();
            var clonableSpecialEntry = specialTable.Table[1];
            clonableSpecialEntry.ID.Value = "FREIGHTER_PASS";
            clonableSpecialEntry.IsConsumable = true;
            clonableSpecialEntry.MissionTier = 1;
            clonableSpecialEntry.ShopNumber = 1;
            Game.Reality.PurchaseableSpecials.SetValue(specialTable);
        }

        private void checkForItem()
        {
            var inventory = Game.Player.Exosuit.GetInventory();
            foreach(var item in inventory.GetItems())
            {
                if (item.ID == "ASTEROID1")
                {
                    Logger.WriteLine($"Found the requested item: {item.ID}");

                }
                else 
                { 
                    Logger.WriteLine($"Item not found, skipping"); 
                }
            }
        }

        private void setFlybys()
        {
            var flybyTable = Game.Reality.FrigateFlybyTable.GetValue();
            foreach(var entry in flybyTable.Entries)
            {
                entry.FlybyType.FrigateFlybyType = GcFrigateFlybyType.FrigateFlybyTypeEnum.AmbientGroup;
                foreach(var frigate in entry.Frigates)
                {
                    frigate.FrigateClass.FrigateClass = Random.GetEnum<GcFrigateClass.FrigateClassEnum>();
                    frigate.MaxCount = Random.Range(10, 20);
                    frigate.MinCount = Random.Range(1, 10);
                }
            }
        }

        private void nightColours()
        {
            Game.Colors.NightSkyColors.ModifyAsync(nightSkyColours =>
            {
                foreach (var setting in nightSkyColours.Settings)
                {
                    setting.CloudColour1 = colourRandomizer();
                    setting.CloudColour2 = colourRandomizer();
                    setting.FogColour = colourRandomizer();
                    setting.HeightFogColour = colourRandomizer();
                    setting.HorizonColour = colourRandomizer();
                    setting.LightColour = colourRandomizer();
                    setting.SkyColour = colourRandomizer();
                    setting.SkySolarColour = colourRandomizer();
                    setting.SkyUpperColour = colourRandomizer();
                    setting.SunColour = colourRandomizer();
                }
            });
        }

        private void duskColours()
        {
            Game.Colors.DuskSkyColors.ModifyAsync(duskSkyColours =>
            {
                foreach (var setting in duskSkyColours.Settings)
                {
                    setting.CloudColour1 = colourRandomizer();
                    setting.CloudColour2 = colourRandomizer();
                    setting.FogColour = colourRandomizer();
                    setting.HeightFogColour = colourRandomizer();
                    setting.HorizonColour = colourRandomizer();
                    setting.LightColour = colourRandomizer();
                    setting.SkyColour = colourRandomizer();
                    setting.SkySolarColour = colourRandomizer();
                    setting.SkyUpperColour = colourRandomizer();
                    setting.SunColour = colourRandomizer();
                }
            });
        }

        private void dayColours()
        {
            Game.Colors.DaySkyColors.ModifyAsync(daySkyColours =>
            {
                foreach (var setting in daySkyColours.Settings)
                {
                    setting.CloudColour1 = colourRandomizer();
                    setting.CloudColour2 = colourRandomizer();
                    setting.FogColour = colourRandomizer();
                    setting.HeightFogColour = colourRandomizer();
                    setting.HorizonColour = colourRandomizer();
                    setting.LightColour = colourRandomizer();
                    setting.SkyColour = colourRandomizer();
                    setting.SkySolarColour = colourRandomizer();
                    setting.SkyUpperColour = colourRandomizer();
                    setting.SunColour = colourRandomizer();
                }
            });
        }

        private void waterColours()
        {
            Game.Colors.WaterColors.ModifyAsync(waterColours =>
            {
                foreach (var setting in waterColours.Settings)
                {
                    setting.FoamColour = colourRandomizer();
                    setting.WaterColourAdd = colourRandomizer();
                    setting.WaterColourBase = colourRandomizer();
                    setting.WaterFogColourFar = colourRandomizer();
                    setting.WaterFogColourNear = colourRandomizer();

                }

            });
        }

        private void spaceNebulas()
        {
            Game.Colors.SpaceSkyColors.ModifyAsync(spaceColours =>
            {
                foreach (var setting in spaceColours.Settings)
                {
                    setting.BottomColour = colourRandomizer();
                    setting.BottomColourPlanet = colourRandomizer();
                    setting.CloudColour = colourRandomizer();
                    setting.FogColour = colourRandomizer();
                    setting.FogColour2 = colourRandomizer();
                    setting.LightColour = colourRandomizer();
                    setting.MidColour = colourRandomizer();
                    setting.MidColourPlanet = colourRandomizer();
                    setting.NebulaColour1 = colourRandomizer();
                    setting.NebulaColour2 = colourRandomizer();
                    setting.NebulaColour3 = colourRandomizer();
                    setting.TopColour = colourRandomizer();
                    setting.TopColourPlanet = colourRandomizer();
                }


            });

            Game.Colors.RareSpaceSkyColors.ModifyAsync(spaceColours =>
            {
                foreach (var setting in spaceColours.Settings)
                {
                    setting.BottomColour = colourRandomizer();
                    setting.BottomColourPlanet = colourRandomizer();
                    setting.CloudColour = colourRandomizer();
                    setting.FogColour = colourRandomizer();
                    setting.FogColour2 = colourRandomizer();
                    setting.LightColour = colourRandomizer();
                    setting.MidColour = colourRandomizer();
                    setting.MidColourPlanet = colourRandomizer();
                    setting.NebulaColour1 = colourRandomizer();
                    setting.NebulaColour2 = colourRandomizer();
                    setting.NebulaColour3 = colourRandomizer();
                    setting.TopColour = colourRandomizer();
                    setting.TopColourPlanet = colourRandomizer();
                }
            });

        }

        private void systemLoaded()
        {
            var systemData = CurrentSystem.GetSystemData();
            Logger.WriteLine($"Aquired System: {systemData.Name.Value.ToString()}");
            Logger.WriteLine($"System Race: {systemData.InhabitingRace.AlienRace.ToString()}");
            Logger.WriteLine($"Maximum System Freighters: {systemData.MaxNumFreighters}");
            
            /*
            CurrentSystem.SetSystemData(systemData);
            Logger.WriteLine($"Set System");
            */
        }

        public Colour colourRandomizer()
        {
            var newColour = new Colour();



            newColour.R = Random.Range(0.000f, 1.000f);
            newColour.G = Random.Range(0.000f, 1.000f);
            newColour.B = Random.Range(0.000f, 1.000f);
            newColour.A = Random.Range(0.500f, 1.000f);

            return newColour;

        }


        /// <summary>
        /// Called once every frame.
        /// </summary>
        public override void Update()
        {
            if (Keyboard.IsPressed(Key.UpArrow))
            {
                spaceNebulas();
                dayColours();
                duskColours();
                nightColours();
                waterColours();

                
                randomizeGalaxyMapColours();

                Game.Creatures.CreatureBehaviors.ModifyAsync(behaviors =>
                {
                    foreach(var behavior in behaviors.BehaviourTree)
                    {
                        Logger.WriteLine($"Behavior Value:[{behavior.Id.Value}] Loaded");
                    }

                });
                    CurrentSystem.ForEachPlanet(planet =>
                    {
                        

                        planet.ModifyPlanetDataAsync(planetData =>
                        {
                            Logger.WriteLine($"Planet {planetData.Name.Value.ToString()} has a seed {planetData.GenerationData.Seed.Seed.ToHex()}");
                            
                            planetData.Life.LifeSetting = Random.GetEnum<GcPlanetLife.LifeSettingEnum>();
                            planetData.CreatureLife.LifeSetting = Random.GetEnum<GcPlanetLife.LifeSettingEnum>();
                            planetData.SentinelData.SentinelLevel = Random.GetEnum<GcPlanetSentinelData.SentinelLevelEnum>();

                            planetData.Weather.AtmosphereType = Random.GetEnum<GcPlanetWeatherData.AtmosphereTypeEnum>();
                            planetData.GenerationData.Seed.Seed = Random.Range(0, 9223372036854775807);
                            planetData.Terrain.SeaLevel = planetData.Terrain.SeaLevel + 1;
                            Logger.WriteLine($"Async Modified {planetData.Name.Value.ToString()}");

                        });
                    
                    
                    });

                    
                


            }

            

            if (Keyboard.IsPressed(Key.DownArrow))
            {
                CurrentSystem.ForEachPlanet(planet =>
                {


                    planet.ModifyPlanetDataAsync(planetData =>
                    {
                        
                        planetData.Terrain.SeaLevel = planetData.Terrain.SeaLevel - 1;
                        Logger.WriteLine($"Async Modified {planetData.Name.Value.ToString()}");

                    });


                });
            }

            if (Keyboard.IsHeld(Key.Control))
            {
                if (Keyboard.IsPressed(Key.UpArrow))
                {
                    Logger.WriteLine($"Testing Combination Hotkeys...");

                    var playerInv = Game.Player.Exosuit.GetInventory();
                    var invList = playerInv.GetItems();
                    foreach (var item in invList)
                    {
                        if (item.ID == "FUEL1")
                        {
                            item.Amount = 9999;
                        }

                    }

                }
            }




            if (Keyboard.IsPressed(Key.RightArrow))
            {
                var invBalance = Game.Player.DefaultInventoryBalance.GetValue();
                invBalance.DefaultSubstanceMaxAmount = 999999;
                invBalance.DefaultProductMaxAmount = 9999;


                var inventory = Player.Exosuit.GetInventory();
                var inventoryList = inventory.GetItems();
                foreach (var item in inventoryList)
                {
                    Logger.WriteLine($"The Player has: {item.Amount} Of {item.ID}");




                }


                var tableTest = Game.Reality.Rewards.GetValue();
                foreach (var reward in tableTest.InteractionTable)
                {
                    Logger.WriteLine($"Interaction Table Reward: {reward.Id.Value.ToString()}");
                }

                checkForItem();



            }


            


            if (Keyboard.IsPressed(Key.LeftArrow))
            {


                var mbinList = Game.MBinManager.GetAllMBin();
                foreach (var file in mbinList)
                {
                    var fileType = Game.MBinManager.GetMBinType(file.FullName);
                    Logger.WriteLine($"Struct [{file.FullName}] has an address of {file.Address}, and is of type: {fileType}");



                }
                structTest();

            }


        }

        private void createItems()
        {
            var itemTable = Game.Reality.Products_NMS.GetValue();
            var clonableEntry = itemTable.Table[1];

            clonableEntry.Name.Value = "FREIGHTER PASS";
            clonableEntry.NameLower.Value = "Freighter Pass";
            clonableEntry.Description.Value = "A specialist pass required to board stationary freighters. Failure to present a valid pass may result in termination by sentinel forces.";
            clonableEntry.Id.Value = "FREIGHTER_PASS";

            itemTable.Table.Add(clonableEntry);
            Game.Reality.Products_NMS.SetValue(itemTable);
            
            

        }


        private void coloursLoaded(IColorsFile colourFile)
        {
            colourFile.ModifyAsync<GcWeatherColourSettings>(allColours =>

            {
                foreach(var genericSetting in allColours.GenericSettings.Settings)
                {
                    genericSetting.CloudColour1 = colourRandomizer();
                    genericSetting.CloudColour2 = colourRandomizer();
                    genericSetting.FogColour = colourRandomizer();
                    genericSetting.HeightFogColour = colourRandomizer();
                    genericSetting.HorizonColour = colourRandomizer();
                    genericSetting.LightColour = colourRandomizer();
                    genericSetting.SkyColour = colourRandomizer();
                    genericSetting.SkySolarColour = colourRandomizer();
                    genericSetting.SkyUpperColour = colourRandomizer();
                    genericSetting.SunColour = colourRandomizer();
                }
            });


            
        }

        

        //Testing Env Objects
        private void envLoaded(IEnvironmentObject environmentObject)
        {


            environmentObject.ModifySpawnData(objData =>
                {
                foreach (var creature in objData.Creatures)
                {
                    Logger.WriteLine($"Modifying {creature.CreatureID} creature");
                    creature.AllowFur = true;
                    creature.CreatureMaxGroupSize = creature.CreatureMaxGroupSize + 50;
                    creature.CreatureMinGroupSize = creature.CreatureMinGroupSize + 100;
                    creature.MaxScale = creature.MaxScale + 10;
                    creature.MinScale = creature.MinScale + 10;
                    //creature.Resource.AltId = "_VERS_1 _HEADB_1 _TOPB_1 _EYESE_1 _BRHACC_7A _TREX_4 _RHIHACC_10";
                }

                foreach (var thing in objData.Objects)
                    {
                        thing.MinAngle = thing.MinAngle + Random.Range(0,2);
                        thing.MaxAngle = thing.MaxAngle + Random.Range(0, 2);

                        thing.MinScale = thing.MinScale + Random.Range(0, 2);
                        thing.MaxScale = thing.MaxScale + Random.Range(0, 2);


                        thing.MaxXZRotation = thing.MaxXZRotation + Random.Range(0, 5);
                    }
                foreach (var thing in objData.DetailObjects)
                    {
                        thing.MinAngle = thing.MinAngle + Random.Range(0, 2);
                        thing.MaxAngle = thing.MaxAngle + Random.Range(0, 2);

                        thing.MinScale = thing.MinScale + Random.Range(0, 2);
                        thing.MaxScale = thing.MaxScale + Random.Range(0, 2);
                    }
                foreach(var thing in objData.DistantObjects)
                    {
                        thing.MinAngle = thing.MinAngle + Random.Range(0, 2);
                        thing.MaxAngle = thing.MaxAngle + Random.Range(0, 2);

                        thing.MinScale = thing.MinScale + Random.Range(0, 2);
                        thing.MaxScale = thing.MaxScale + Random.Range(0, 2);
                    }
                foreach(var thing in objData.Landmarks)
                    {
                        thing.MinAngle = thing.MinAngle + Random.Range(0, 2);
                        thing.MaxAngle = thing.MaxAngle + Random.Range(0, 2);

                        thing.MinScale = thing.MinScale + Random.Range(0, 2);
                        thing.MaxScale = thing.MaxScale + Random.Range(0, 2);
                    }


                 

                }

                );
            


        }

        private void planetLoaded(IPlanet planet)
        {

            CurrentSystem.ForEachPlanetAsync(planet =>
            {
                planet.ModifyPlanetDataAsync(planetData =>
                {
                    //Testing
                    Logger.WriteLine($"{planetData.GenerationData.Biome.Biome.ToString()} Planet {planetData.Name.Value} has been loaded");



                    //Testing Names
                    var initialName = planetData.Name.Value;
                    planetData.Name.Value = ($"Corrupted Planet: {initialName}");

                    //Randomize Colours
                    foreach (var pallette in planetData.Colours.Palettes)
                    {
                        for (var i = 0; i < pallette.Colours.Length; i++)
                        {

                            pallette.Colours[i] = colourRandomizer();


                        }

                    }

                    //Randomize Atmos
                    var atmosChance = Random.Range(0, 100);
                    if (atmosChance < 30)
                    {
                        planetData.Weather.AtmosphereType = GcPlanetWeatherData.AtmosphereTypeEnum.None;
                    }
                    else if (atmosChance > 30)
                    {
                        planetData.Weather.AtmosphereType = GcPlanetWeatherData.AtmosphereTypeEnum.Normal;
                    }

                    //Randomize Sentinels
                    var aeronChance = Random.Range(0, 2);
                    if (aeronChance == 0)
                    {
                        planetData.SentinelData.SentinelLevel = GcPlanetSentinelData.SentinelLevelEnum.Low;
                        planetData.SentinelData.MaxActiveDrones = Random.Range(0, 8);
                    }
                    else if (aeronChance == 1)
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
                    planetData.ResourceLevel = Random.GetEnum<GcPlanetData.ResourceLevelEnum>();


                    //Randomize Life (Flora) Chance
                    planetData.Life.LifeSetting = Random.GetEnum<GcPlanetLife.LifeSettingEnum>();


                    //Randomize Creature(Fauna) Chance
                    planetData.CreatureLife.LifeSetting = Random.GetEnum<GcPlanetLife.LifeSettingEnum>();

                    //Activate All Terrain Features
                    foreach (var feature in planetData.Terrain.Features)
                    {
                        feature.Active = true;
                    }

                    //Randomize Building Density
                    planetData.BuildingLevel.BuildingDensity = Random.GetEnum<GcBuildingDensityLevels.BuildingDensityEnum>();

                    //Randomize Alien Race Chance
                    planetData.InhabitingRace.AlienRace = Random.GetEnum<GcAlienRace.AlienRaceEnum>();

                    //Randomize Water HeavyAir Colours
                    foreach (var heavyAirColour in planetData.Water.HeavyAir.Colours)
                    {

                        heavyAirColour.Colour1 = colourRandomizer();
                        heavyAirColour.Colour2 = colourRandomizer();

                    }

                    //Randomize Weather HeavyAir Colours
                    foreach (var weatherHeavyAir in planetData.Weather.HeavyAir.Colours)
                    {

                        weatherHeavyAir.Colour1 = colourRandomizer();
                        weatherHeavyAir.Colour2 = colourRandomizer();

                    }

                    //Randomize Tile Colours
                    for (var i = 0; i < planetData.TileColours.Length; i++)
                    {

                        planetData.TileColours[i] = colourRandomizer();
                    }



                    //Ring Colours
                    planetData.Rings.Colour1 = colourRandomizer();
                    planetData.Rings.Colour2 = colourRandomizer();

                    //Change Screen Filters
                    planetData.Weather.ScreenFilter.ScreenFilter = GcScreenFilters.ScreenFilterEnum.Default;
                    planetData.Weather.StormScreenFilter.ScreenFilter = Random.GetEnum<GcScreenFilters.ScreenFilterEnum>();

                    //Enlarge Caves
                    planetData.Terrain.MinimumCaveDepth = 100;
                });
            });
            
            
          
        }

        private void structTest()
        {
            

            var memMgr = new MemoryManager();
            var missionTableAddress = Game.MBinManager.GetMBin("GcMissionTable").Address;
            var missionTableInMem = memMgr.GetValue<GcMissionTable>(missionTableAddress);
            Logger.WriteLine($"The Amount Of Missions Loaded Is: {missionTableInMem.Missions.Count}");
            for(var i=0; i< missionTableInMem.Missions.Count; i++)
            {
                Logger.WriteLine($"Loaded Mission: {missionTableInMem.Missions[i].MissionID.Value.ToString()}");


            }

            var combatEffectsTableAddress = Game.MBinManager.GetMBin("GcCombatEffectsTable").Address;
            var GcCombatEffectsTableInMem = memMgr.GetValue<GcCombatEffectsTable>(combatEffectsTableAddress);
            Logger.WriteLine($"Currently Loaded {GcCombatEffectsTableInMem.EffectsData.Length} effects");
            for(var i=0; i<GcCombatEffectsTableInMem.EffectsData.Length; i++)
            {
                Logger.WriteLine($"Combat Effect: {GcCombatEffectsTableInMem.EffectsData[i].ParticlesId.Value.ToString()} is active");


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