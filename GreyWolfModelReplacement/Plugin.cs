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
            // Plugin startup logic

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
                if (__instance.playerUsername != null) {
                    if (hasLoaded.Contains(__instance.playerUsername)) return;
                    hasLoaded.Add(__instance.playerUsername);
                }
                
                BodyReplacementGreywolf? body = createNewBody(__instance.playerSteamId + "");
                if(body == null) {
                    body = createNewBody(__instance.playerUsername + "");
                }

                if(body != null) {
                    ModelReplacementAPI.SetPlayerModelReplacement(__instance, body);
                }

                Debug.Log("No body found for " + __instance.playerSteamId + " : " + __instance.playerUsername);
            }

        }

        public static BodyReplacementGreywolf? createNewBody(string toload) {
            if (LC_API.BundleAPI.BundleLoader.GetLoadedAsset<GameObject>("Assets/ModelReplacementAPI/AssetsToBuild/" + toload + ".prefab") != null) {
               return new BodyReplacementGreywolf(toload);
            }
            return null;
        }


    }

}