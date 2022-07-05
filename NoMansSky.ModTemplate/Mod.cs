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
            









        }



        /// <summary>
        /// Called once every frame.
        /// </summary>
        public override void Update()
        {


            

        

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
                    var fileType = Game.MBinManager.GetMBinType(file.Name);
                    Logger.WriteLine($"Struct [{file.Name}] has an address of {file.Address}, and is of type: {fileType}");
                    
                    

                }
                structTest();

            }


        }


        private void planetLoaded(IPlanet planet)
        {
            planet.ModifyPlanetData(planetData =>
            {
                planetData.SentinelData.MaxActiveDrones = 100;
                planetData.Terrain.MinimumCaveDepth = 500;

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