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
    public class BodyReplacementSmolvin : BodyReplacementBase
    {
        public override string boneMapFileName => "smolvin.json";

        public override GameObject LoadAssetsAndReturnModel()
        {
            return LC_API.BundleAPI.BundleLoader.GetLoadedAsset<GameObject>("assets/smolvin/egg_girl_atlased.prefab");
        }

        public override void AddModelScripts()
        {
        }

 
    }
}
