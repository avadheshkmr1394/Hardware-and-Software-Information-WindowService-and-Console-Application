using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssetInfinity.PostHardwareInfo;
using GetHardwareInfo;
using static System.Net.Mime.MediaTypeNames;

namespace AssetInfinity.PostHardwareInfo
{
    class Program
    {
        public static string APIURL = "https://api.assetinfinity.com/";
        public static string token;
        static void Main(string[] args)
        {
            Asset assetObj = new Asset();
            AccessTokenModel obj_token = Asset.GetToken();
            token = obj_token.token_type + " " + obj_token.access_token;
            HardwareInfo obj = new HardwareInfo(token);
            obj.InsertInfo();
        }
    }
}
