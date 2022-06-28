﻿using Reloaded.Hooks.ReloadedII.Interfaces;
using Reloaded.Mod.Interfaces;
using Reloaded.ModHelper;
using NoMansSky.Api;
using libMBIN;
using libMBIN.NMS.GameComponents;
using libMBIN.NMS.Globals;
using libMBIN.NMS.Toolkit;

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

            ModSettingBool EnableEverythingIsFree = new ModSettingBool();
            
           


        }

        /// <summary>
        /// Called once every frame.
        /// </summary>
        public override void Update()
        {
            // Here is an example of checking for keyboard keys
            if (Keyboard.IsPressed(Key.UpArrow))
            {
                Logger.WriteLine("The Up Arrow was just pressed!");
                Player.Units.Value = Player.Units.Value + 100;
            }
            
            if(Keyboard.IsPressed(Key.RightArrow))
            {

                if(Game.Player.Exosuit.SuitRefiner.Input.ID == "ASTEROID1")
                {
                    Game.Player.Exosuit.SuitRefiner.Output.ID = "QUICKSILVER";
                    Game.Player.Exosuit.SuitRefiner.Output.ItemType = Api.GcInventoryType.Substance;
                    Logger.WriteLine("Added Custom Refiner Recipe: Silver - Quicksilver");

                }
                


            }

            if(Keyboard.IsPressed(Key.DownArrow))
            {
                testingNonGlobalFields();


            }


            logicTest();
            editDebugGLobals();
            randomGeneration();


            if (Keyboard.IsPressed(Key.LeftArrow))
            {
                var mbinList = Game.MBinManager.GetAllMBIN();
                foreach (var file in mbinList)
                {
                    Logger.WriteLine("[" + file.Name + "]: " + file.Address);

                }
            }


        }

        private void editDebugGLobals()
        {
            var memManager = new MemoryManager(); //Mem Manager
            bool EverythingIsFree = memManager.GetValue<bool>("GcDebugOptions.EverythingIsFree"); 
           // Logger.WriteLine($"GcDebugOptions.EverythingIsFree is set to: {EverythingIsFree.ToString()}");

            if(Keyboard.IsPressed(Key.G) && EverythingIsFree == false)
            {
                memManager.SetValue("GcDebugOptions.EverythingIsFree", true);
                Logger.WriteLine("Enabled Everything Is Free Cheat");



            }
            
            if(Keyboard.IsPressed(Key.H) && EverythingIsFree == true)
            {
                memManager.SetValue("GcDebugOptions.EverythingIsFree", false);
                Logger.WriteLine("Disabled Everything Is Free Cheat");


            }



                


        }

        private void logicTest()
        {
            var initialShield = Game.Player.Shield;
            var memMgr = new MemoryManager();

            if (initialShield < 50)
            {

                
                memMgr.SetValue("GcGameplayGlobals.WaterLandingDamageMultiplier", 0.000f);

                memMgr.SetValue("GcPlayerGlobals.HealthRechargeMinTimeSinceDamage", 1);

                memMgr.SetValue("GcPlayerGlobals.ShieldRechargeMinTimeSinceDamage", 1);



            }

            else
            {
                memMgr.SetValue("GcGameplayGlobals.WaterLandingDamageMultiplier", 0.333f);

                memMgr.SetValue("GcPlayerGlobals.HealthRechargeMinTimeSinceDamage", 10);

                memMgr.SetValue("GcPlayerGlobals.ShieldRechargeMinTimeSinceDamage", 30);



            }



        }

        private void randomGeneration()
        {
            var memMgr = new MemoryManager();
           //Disabling these since theyre read every second.
           //memMgr.SetValue("GcTerrainGlobals.MinHighWaterLevel", Random.Range(0, 150));
           // memMgr.SetValue("GcTerrainGlobals.MaxHighWaterLevel", Random.Range(150, 300));

            memMgr.SetValue("GcTerrainGlobals.NumGeneratorCalls", Random.Range(0, 16));
            memMgr.SetValue("GcTerrainGlobals.NumPolygoniseCalls", Random.Range(0, 16));
            memMgr.SetValue("GcTerrainGlobals.NumPostPolygoniseCalls", Random.Range(0, 16));

            memMgr.SetValue("GcWaterGlobals.WaveHeight", Random.Range(0, 3));
            memMgr.SetValue("GcWaterGlobals.WaveChoppiness", Random.Range(0, 4));

            memMgr.SetValue("GcSimulationGlobals.ProceduralBuildingsGenerationSeed", Random.Range(0, 2147483647));

            memMgr.SetValue("GcSolarGenerationGlobals.SolarSystemMaximumRadius", Random.Range(0, 2147483647));

        }

       private void testingNonGlobalFields()
        {
            var memMgr = new MemoryManager();

            int primaryPlanet = memMgr.GetValue<int>("GcPlayerStateData.PrimaryPlanet");
            Logger.WriteLine($"Primary Planet: {primaryPlanet}");

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