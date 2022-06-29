using Reloaded.Hooks.ReloadedII.Interfaces;
using Reloaded.Mod.Interfaces;
using Reloaded.ModHelper;
using NoMansSky.Api;
using libMBIN;
using libMBIN.NMS.GameComponents;
using libMBIN.NMS.Globals;
using libMBIN.NMS.Toolkit;
using System.Windows.Forms;


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
                Logger.WriteLine($"Total Units: {Player.Units.Value}");
                
                
                Player.Units.Value = Player.Units.Value + 16000000;
                
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

      

        private void logicTest()
        {
            var initialShield = Game.Player.Shield;
            var memMgr = new MemoryManager();

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

        private void randomGeneration()
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