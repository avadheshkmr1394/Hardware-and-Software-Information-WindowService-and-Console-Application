﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using static GetHardwareInfo.AssetModel;
using RestSharp;
using System.Net;
//using RestSharp.Extensions.MonoHttp;
//using System.Windows.Forms;
using System.IO;
using AssetInfinity.PostHardwareInfo;
using AssetInfinity.HardwareInformationService;
using System.Configuration;

namespace GetHardwareInfo
{
    class Asset
    {
        static string URL = ConfigurationSettings.AppSettings["URL"];
        static string userName = ConfigurationSettings.AppSettings["UserName"];
        static string password = ConfigurationSettings.AppSettings["Password"];
        static string clientId = ConfigurationSettings.AppSettings["ClientId"];

        public static AccessTokenModel GetToken()
        {
            AccessTokenModel obj = new AccessTokenModel();
            var client = new RestClient(URL + "/Token");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", "grant_Type=password&username="+userName+"&Password="+password+"&Client_Id="+clientId+"", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            obj = JsonConvert.DeserializeObject<AccessTokenModel>(response.Content);
            return obj;
        }

        public static object AssetITSave(object data, Uri webApiUrl, string token)
        {
            string serialisedData = JsonConvert.SerializeObject(data);
            var client = new RestClient(webApiUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", token);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", serialisedData, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            var content = response.Content;
            using (var stringReader = new StringReader(content))
            {
                using (var jsonTextReader = new JsonTextReader(stringReader))
                {
                    return JsonConvert.SerializeObject(jsonTextReader);
                }
            }
        }

        public static string KeyValue(List<KeyTableParam> key)
        {
            string concatKey = string.Empty;
            foreach (KeyTableParam KeyTableParam in key)
            {
                concatKey = concatKey == string.Empty ? string.Join("", KeyTableParam.InternalKeyName) : concatKey + "," + string.Join("", KeyTableParam.InternalKeyName);
            }
            return concatKey;
        }
    }
}
