using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Newtonsoft.Json;
using System.Linq;
using System.Xml;
using System.IO;
using GameNetcodeStuff;
using UnityEngine.PlayerLoop;
using System.Reflection;
using ModelReplacement;
using BepInEx;

namespace GreyWolfModelReplacement
{
    public class BodyReplacementGreywolf : BodyReplacementBase
    {
        protected override GameObject LoadAssetsAndReturnModel(string steamid)
        {
            ModelReplacementAPI.Instance.Logger.LogInfo("Body found for " + steamid);
            string dir1 = Path.Combine(Paths.PluginPath, "Bundles", steamid);
            LC_API.BundleAPI.LoadedAssetBundle bundle = LC_API.BundleAPI.BundleLoader.LoadAssetBundle(dir1);
            GameObject body = bundle.GetAsset<GameObject>("assets/modelreplacementapi/assetstobuild/" + steamid + ".prefab");
            ModelReplacementAPI.Instance.Logger.LogInfo("PostLoad " + steamid);
            return body;
        }

        protected override bool PreloadModelForSteamID(string steamid)
        {
            ModelReplacementAPI.Instance.Logger.LogInfo("Start download: " + steamid);
            return API.getFileBySteamIDAsync(steamid);
        }

        protected override void AddModelScripts()
        {

            var HairBones = replacementModel.GetComponentsInChildren<Transform>().Where(x => x.name.Contains("FrontHair") || x.name.Contains("SideHair"));
            HairBones.ToList().ForEach(bone =>
            {
                DynamicBone dynBone = bone.gameObject.AddComponent<DynamicBone>();
                dynBone.m_Root = bone;
                dynBone.m_UpdateRate = 60;
                dynBone.m_Damping = 0.254f;
                dynBone.m_Elasticity = 0.08f;
                dynBone.m_Stiffness = 0.5f;
                dynBone.m_Inert = 0.274f;
                dynBone.m_Radius = 0.05f;
                dynBone.m_Gravity = new Vector3(0, -0.01f, 0);
            });
        }
    }
}
