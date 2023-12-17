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

namespace GreyWolfModelReplacement
{
    public class BodyReplacementGreywolf : BodyReplacementBase
    {
        protected override GameObject LoadAssetsAndReturnModel(string username,string steamid)
        {
            string avatar_name = "";
            if (hasBody(steamid) != null)
            {
                avatar_name = steamid;
            }
            else if (username != null && hasBody(username) != null)
            {
                avatar_name = username;
            }
            if (avatar_name.Length == 0) {
                Debug.Log("No body found for " + steamid + " : " + username);
                return hasBody("76561198005992881");
            }
            GameObject asset = hasBody(avatar_name);
            Debug.Log(asset);
            Debug.Log("assets/modelreplacementapi/assetstobuild/" + avatar_name + ".prefab");
            return asset;
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

        public static GameObject hasBody(string toload)
        {
           return LC_API.BundleAPI.BundleLoader.GetLoadedAsset<GameObject>("assets/modelreplacementapi/assetstobuild/" + toload + ".prefab");
        }
    }
}
