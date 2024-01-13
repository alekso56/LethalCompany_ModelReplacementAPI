using System;
using System.Collections.Generic;
using System.Text;

namespace LethalAPI.Models
{
    public class AFile
    {
        public AFile()
        {
            name = "";
            mimeType = "";
            link = "";
        }
        public string id { get; set; }
        public string owner_id { get; set; }

        public string name { get; set; }

        public string mimeType { get; set; }

        public long created_at { get; set; }

        public long updated_at { get; set; }

        public long size { get; set; }

        public string link { get; set; }

        internal void update(AFile data)
        {
            name = data.name;
            mimeType = data.mimeType;
            TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
            updated_at = (int)t.TotalSeconds;
            size = data.size;
        }

        internal void create()
        {
            TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
            updated_at = (int)t.TotalSeconds;
            created_at = (int)t.TotalSeconds;
        }

        internal bool is_valid()
        {
            return size != 0 && created_at != 0 && name != "";
        }

        public override string ToString()
        {

            return id + " name: " + name + " MimeType: " + mimeType + " created_at: " + created_at + " updated_at:" + updated_at;
        }

        //Could track versions, but we dont really care.
    }
}

