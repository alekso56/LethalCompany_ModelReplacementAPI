using BepInEx;
using HarmonyLib;
using UnityEngine;
using GameNetcodeStuff;
using ModelReplacement;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine.Audio;

//using System.Numerics;

namespace GreyWolfModelReplacement
{


    [BepInPlugin("GreywolfModelReplacement", "Greywolf Model", "1.0")]
    [BepInDependency("meow.ModelReplacementAPI", BepInDependency.DependencyFlags.HardDependency)]
    public class Plugin : BaseUnityPlugin
    {
        static List<string> hasLoaded = new List<string>();
        private void Awake()
        {
            ModelReplacementAPI.RegisterModelReplacementOverride(typeof(BodyReplacementGreywolf));
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
                if (__instance.playerUsername != null) {
                    if (hasLoaded.Contains(__instance.playerUsername)) return;
                    hasLoaded.Add(__instance.playerUsername);
                }
                if (BodyReplacementGreywolf.hasBody(__instance.playerSteamId.ToString()) != null || (__instance.playerUsername != null && BodyReplacementGreywolf.hasBody(__instance.playerUsername)))
                {
                    ModelReplacementAPI.SetPlayerModelReplacement(__instance, typeof(BodyReplacementGreywolf));
                }
               
               
            }

        }

        


    }

}