using BepInEx;
using System.Linq;
using System.Runtime.InteropServices;
using Zeekerss;
using Zeekerss.Core;
using Zeekerss.Core.Singletons;
using System;
using HarmonyLib;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Reflection;
using GameNetcodeStuff;
using ModelReplacement;

//using System.Numerics;

namespace GreyWolfModelReplacement
{



    [BepInPlugin("GreywolfModelReplacement", "Greywolf Model", "1.0")]
    [BepInDependency("meow.ModelReplacementAPI", BepInDependency.DependencyFlags.HardDependency)]
    public class Plugin : BaseUnityPlugin
    {

        private void Awake()
        {
            // Plugin startup logic

            ModelReplacementAPI.RegisterSuitModelReplacement("Green Suit", typeof(BodyReplacementGreywolf));
            ModelReplacementAPI.RegisterSuitModelReplacement("Default", typeof(BodyReplacementEruqyuna));
            ModelReplacementAPI.RegisterSuitModelReplacement("Orange suit", typeof(BodyReplacementEruqyuna));
            ModelReplacementAPI.RegisterSuitModelReplacement("Pajama suit", typeof(BodyReplacementGreywolf));

            //

            Harmony harmony = new Harmony("GreywolfModelReplacement");
            harmony.PatchAll();
            Logger.LogInfo($"Plugin {"GreywolfModelReplacement"} is loaded!");
        }



        [HarmonyPatch(typeof(PlayerControllerB))]
        public class PlayerControllerBPatch
        {

            [HarmonyPatch("Update")]
            [HarmonyPostfix]
            public static void UpdatePatch(ref PlayerControllerB __instance)
            {
                ModelReplacementAPI.SetPlayerModelReplacement(__instance, typeof(BodyReplacementEruqyuna));

            }

        }



    }

}