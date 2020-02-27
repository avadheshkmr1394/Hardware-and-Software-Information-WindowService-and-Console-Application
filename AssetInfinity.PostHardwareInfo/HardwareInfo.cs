using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using GetHardwareInfo;
using System.Web.Script.Serialization;


namespace AssetInfinity.PostHardwareInfo
{
    class HardwareInfo
    {
        private string token { get; set; }
        public HardwareInfo(string token)
        {
            this.token = token;
        }

        public RequiredSystemInfoKeys GetJsonValue()
        {
            string Json = "{'Hardware':[{'KeyTableName':'Win32_ComputerSystemProduct','KeyTableParam':[{'InternalKeyName':'IdentifyingNumber','DisplayKeyName':'SerialNumber'},{'InternalKeyName':'Name','DisplayKeyName':'Model'},{'InternalKeyName':'Vendor','DisplayKeyName':'Brand'}]},{'KeyTableName':'Win32_OperatingSystem','KeyTableParam':[{'InternalKeyName':'Caption','DisplayKeyName':'OS'},{'InternalKeyName':'SerialNumber','DisplayKeyName':'OSKey'},{'InternalKeyName':'OSArchitecture','DisplayKeyName':'Architecture'},{'InternalKeyName':'SystemDrive','DisplayKeyName':'SystemDrive'},{'InternalKeyName':'SystemDirectory','DisplayKeyName':'SystemDirectory'},{'InternalKeyName':'LastBootUpTime','DisplayKeyName':'Uptime'}]},{'KeyTableName':'Win32_DiskDrive','KeyTableParam':[{'InternalKeyName':'Size','DisplayKeyName':'HDDSize'}]},{'KeyTableName':'Win32_ComputerSystem','KeyTableParam':[{'InternalKeyName':'TotalPhysicalMemory','DisplayKeyName':'RAMSize'},{'InternalKeyName':'UserName','DisplayKeyName':'Last LogIn User '},{'InternalKeyName':'Domain','DisplayKeyName':'Domain'},{'InternalKeyName':'Name','DisplayKeyName':'PCName'},{'InternalKeyName':'DNSHostName','DisplayKeyName':'HostName'},{'InternalKeyName':'CurrentTimeZone','DisplayKeyName':'CurrentTimeZone'}]},{'KeyTableName':'Win32_Processor','KeyTableParam':[{'InternalKeyName':'Name','DisplayKeyName':'Processor'}]},{'KeyTableName':'Win32_VideoController','KeyTableParam':[{'InternalKeyName':'Caption','DisplayKeyName':'GPU'}]},{'KeyTableName':'Win32_NetworkAdapterConfiguration','KeyTableParam':[{'InternalKeyName':'MACAddress','DisplayKeyName':'MACAddress'},{'InternalKeyName':'IPAddress','DisplayKeyName':'IPAddress'}]}],'Software':[{'InternalKeyName':'DisplayName','DisplayKeyName':'SoftwareName'},{'InternalKeyName':'Publisher','DisplayKeyName':'Brand'},{'InternalKeyName':'DisplayVersion','DisplayKeyName':'Version'},{'InternalKeyName':'InstallDate','DisplayKeyName':'InstallDate'}]}";
            return new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<RequiredSystemInfoKeys>(Json);
        }

        public void InsertInfo()
        {
            RequiredSystemInfoKeys listKeyTable = GetJsonValue();
            AssetDetailsViewModel obj_addAsset = new AssetDetailsViewModel();
            List<AssetDetailsViewModel> list = new List<AssetDetailsViewModel>();
            obj_addAsset.CategoryId = 101;
            obj_addAsset.StatusId = 3071;
            obj_addAsset.LocationId = 351;
            obj_addAsset.AppUserId = "3";
            foreach (Hardware KeyTable in listKeyTable.hardware)
            {
                ObjectQuery query = new ObjectQuery("select "+ Asset.KeyValue(KeyTable.KeyTableParam) +" from " + KeyTable.KeyTableName);
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
                foreach (ManagementObject share in searcher.Get())
                {
                    foreach (KeyTableParam KeyTableParam in KeyTable.KeyTableParam)
                    {
                        if (KeyTable.KeyTableName == "Win32_ComputerSystemProduct")
                        {
                            if (KeyTableParam.InternalKeyName == "IdentifyingNumber")
                            {
                                obj_addAsset.SerialNo = Convert.ToString(share[KeyTableParam.InternalKeyName]);
                            }
                            else if (KeyTableParam.InternalKeyName == "Name")
                            {
                                obj_addAsset.Model = Convert.ToString(share[KeyTableParam.InternalKeyName]);
                            }
                            else if (KeyTableParam.InternalKeyName == "Vendor")
                            {
                                obj_addAsset.Brand = Convert.ToString(share[KeyTableParam.InternalKeyName]);
                            }
                        }
                        else if (KeyTable.KeyTableName == "Win32_OperatingSystem")
                        {
                            if (KeyTableParam.InternalKeyName == "Caption")
                            {
                                obj_addAsset.OS = Convert.ToString(share[KeyTableParam.InternalKeyName]);
                            }
                            else if (KeyTableParam.InternalKeyName == "OSArchitecture")
                            {
                                obj_addAsset.Architecture = Convert.ToString(share[KeyTableParam.InternalKeyName]);
                            }
                            else if (KeyTableParam.InternalKeyName == "SystemDrive")
                            {
                                obj_addAsset.SystemDrive = Convert.ToString(share[KeyTableParam.InternalKeyName]);
                            }
                            else if (KeyTableParam.InternalKeyName == "SystemDirectory")
                            {
                                obj_addAsset.SystemDirectory = Convert.ToString(share[KeyTableParam.InternalKeyName]);
                            }
                            else if (KeyTableParam.InternalKeyName == "LastBootUpTime")
                            {
                                obj_addAsset.Uptime = Convert.ToString(share[KeyTableParam.InternalKeyName]);
                            }
                        }
                        else if (KeyTable.KeyTableName == "Win32_DiskDrive")
                        {
                            if (KeyTableParam.InternalKeyName == "Size")
                            {
                                obj_addAsset.HDD = Convert.ToString(Math.Round(Convert.ToDouble(share[KeyTableParam.InternalKeyName]) / (1048576 * 1024), 0)) + " GB";
                            }
                        }
                        else if (KeyTable.KeyTableName == "Win32_ComputerSystem")
                        {
                            if (KeyTableParam.InternalKeyName == "TotalPhysicalMemory")
                            {
                                obj_addAsset.RAM = Convert.ToString(Math.Round(Convert.ToDouble(share[KeyTableParam.InternalKeyName]) / (1048576 * 1024), 0)) + " GB";
                            }
                            else if (KeyTableParam.InternalKeyName == "UserName")
                            {
                                obj_addAsset.Username = Convert.ToString(share[KeyTableParam.InternalKeyName]);
                            }
                            else if (KeyTableParam.InternalKeyName == "Domain")
                            {
                                obj_addAsset.DomainName = Convert.ToString(share[KeyTableParam.InternalKeyName]);
                            }
                            else if (KeyTableParam.InternalKeyName == "DNSHostName")
                            {
                                obj_addAsset.HostName = Convert.ToString(share[KeyTableParam.InternalKeyName]);
                            }
                            else if (KeyTableParam.InternalKeyName == "Name")
                            {
                                obj_addAsset.PCName = Convert.ToString(share[KeyTableParam.InternalKeyName]);
                            }
                            else if (KeyTableParam.InternalKeyName == "CurrentTimeZone")
                            {
                                obj_addAsset.TimeZone = Convert.ToString(share[KeyTableParam.InternalKeyName]);
                            }
                        }
                        else if (KeyTable.KeyTableName == "Win32_Processor")
                        {
                            if (KeyTableParam.InternalKeyName == "Name")
                            {
                                obj_addAsset.CPU = Convert.ToString(share[KeyTableParam.InternalKeyName]);
                            }
                        }
                        else if (KeyTable.KeyTableName == "Win32_VideoController")
                        {
                            if (KeyTableParam.InternalKeyName == "Caption")
                            {
                                obj_addAsset.GPU = Convert.ToString(share[KeyTableParam.InternalKeyName]);
                            }
                        }
                        else if (KeyTable.KeyTableName == "Win32_NetworkAdapterConfiguration")
                        {
                            if (KeyTableParam.InternalKeyName == "MACAddress")
                            {
                                obj_addAsset.MACAddress = Convert.ToString(share[KeyTableParam.InternalKeyName]);
                            }
                            else if(KeyTableParam.InternalKeyName== "IPAddress")
                            {
                                string[] addresses = (string[])share["IPAddress"];
                                if ((addresses != null ? addresses[0] : null) != null)
                                {
                                    obj_addAsset.LANIPAddress = addresses[0];
                                }
                            }
                        }
                    }
                }
            }

            string registry_key = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            using (Microsoft.Win32.RegistryKey key = Registry.LocalMachine.OpenSubKey(registry_key))
            {
                foreach (string subkey_name in key.GetSubKeyNames())
                {
                    using (RegistryKey subkey = key.OpenSubKey(subkey_name))
                    {

                        AssetDetailsViewModel Softwareobj = new AssetDetailsViewModel();
                        if ((string)subkey.GetValue("DisplayName") != null)
                        {
                            obj_addAsset.SoftWareLists.Add(new SoftwareList
                            {
                                ProductName = (string)subkey.GetValue("DisplayName"),
                                Version = (string)subkey.GetValue("DisplayVersion"),
                                InstallDate = (string)subkey.GetValue("InstallDate"),
                                Publisher = (string)subkey.GetValue("Publisher")
                            });
                        }
                    }
                }
            }
            list.Add(obj_addAsset);
            string url = "https://api.assetinfinity.com/api/SaveITAsset?";
            Asset.AssetITSave(list, new System.Uri(url), token);
        }
    }
}





