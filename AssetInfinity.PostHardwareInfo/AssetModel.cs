using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GetHardwareInfo
{
    class AssetModel
    {
        public class TokenData
        {
            public string grant_Type { get; set; }
            public string username { get; set; }
            public string Password { get; set; }
            public string Client_Id { get; set; }
            public string scope { get; set; }
            public int tag { get; set; }
        }
      
    }
    public class AssetDetailsViewModel
    {
        public AssetDetailsViewModel()
        {
            SoftWareLists = new List<SoftwareList>();
            SoftWareWindowUpdateLists = new List<SoftwareWindowsUpdateList>(); /*#CC01 Added*/
        }
        public string AppUserId { get; set; }
        public int? CategoryId { get; set; }
        public int? LocationId { get; set; }
        public int? StatusId { get; set; }
        public string OS { get; set; }
        public string Architecture { get; set; }
        public string SerialNo { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public string CPU { get; set; }
        public string RAM { get; set; }
        public string HDD { get; set; }
        public string GPU { get; set; }
        public string Username { get; set; }
        public string PCName { get; set; }
        public string DomainName { get; set; }
        public string HostName { get; set; }
        public string SystemDrive { get; set; }
        public string SystemDirectory { get; set; }
        public string Uptime { get; set; }
        public string MACAddress { get; set; }
        public string LANIPAddress { get; set; }
        public string WANIPAddress { get; set; }
        public string Antivirus { get; set; }
        public string Firewall { get; set; }
        public string TimeZone { get; set; }
        public string Country { get; set; }
        public string ISP { get; set; }
        public List<SoftwareList> SoftWareLists { get; set; }
        public List<SoftwareWindowsUpdateList> SoftWareWindowUpdateLists { get; set; }
    }

    public class SoftwareList
    {
        public string ProductName { get; set; }
        public string Publisher { get; set; }
        public string Version { get; set; }
        public string InstallDate { get; set; }
    }

    public class SoftwareWindowsUpdateList
    {
        public string Title { get; set; }
        public string ClientApplicationID { get; set; }
        public string Date { get; set; }
        public string SupportUrl { get; set; }
    }
    public class AccessTokenModel
    {

        public string access_token { get; set; }
        public string token_type { get; set; }
    }
    public class KeyTableParam
    {
        public string InternalKeyName { get; set; }
        public string DisplayKeyName { get; set; }
    }
    public class RequiredSystemInfoKeys
    {
        public List<KeyTableParam> software { get; set; }
        public List<Hardware> hardware { get; set; }
    }
    public class Hardware
    {
        public string KeyTableName { get; set; }
        public List<KeyTableParam> KeyTableParam { get; set; }
    }

}
