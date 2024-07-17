using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace DataBaseDictionary.helpers;

public class config
{
    private const string CONFIG_FILE = "config.json";
    public string server { get; set; }
    public string user { get; set; }
    public string password { get; set; }
    public string logo_path {  get; set; }

    public config() 
    {
        server = "";
        user = "";
        password = "";
        logo_path = "";
    }

    public void SaveToJson()
    {  
        string jsonString = JsonConvert.SerializeObject(this, Formatting.Indented);
        File.WriteAllText(CONFIG_FILE, jsonString);
    }

    public static config LoadFromJson()
    {
        if (!File.Exists(CONFIG_FILE))
        {
            return new config();
        }

        string jsonString = File.ReadAllText(CONFIG_FILE);
        return JsonConvert.DeserializeObject<config>(jsonString);
    }
}

