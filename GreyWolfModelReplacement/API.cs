using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using LethalAPI.Models;
using System.IO;
using BepInEx;
using System.Security.Policy;
using System.Threading.Tasks;

namespace GreyWolfModelReplacement
{
    internal class API
    {
        public static bool getFileBySteamIDAsync(string steamID) {
           AFile file = getBySteamID(steamID);
           if (file != null) {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/octet-stream"));
                Uri uri = new Uri(file.link);
                Task<HttpResponseMessage> rhandle = client.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead);
                rhandle.Wait();
                Task<Stream> shandle = rhandle.Result.Content.ReadAsStreamAsync();
                shandle.Wait();
                using (var s = shandle.Result)
                {
                    string dir1 = Path.Combine(Paths.PluginPath, "Bundles", steamID);
                    using (var fs = new FileStream(dir1, FileMode.OpenOrCreate))
                    {
                        s.CopyTo(fs);
                        client.Dispose();
                        rhandle.Dispose();
                        return true;
                    }
                }
            }
           return false;
        }
        static AFile getBySteamID(string id)
        {

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", "LethalCompany");
            try
            {
                var jsonr = client.GetStringAsync("https://lethal.cax.no/FileAPI/" + id);
                jsonr.Wait();
                var resjson = JsonConvert.DeserializeObject<AFile>(jsonr.Result);
                Console.Write(resjson);
                client.Dispose();
                return resjson;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
