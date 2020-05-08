using System.Configuration;
using System.Collections.Generic;
using System.Windows.Forms;
using System;

namespace OfficeSearcher
{
    public static class MyConfig
    {
        public static string GetVersion()
        {
            return ConfigurationManager.AppSettings["Version"];
        }
        public static string[] GetRemoveText()
        {
            return ConfigurationManager.AppSettings["RemoveText"].Split('|');
        }
        public static string GetAppName()
        {
            return ConfigurationManager.AppSettings["AppName"];
        }
        public static int GetMaxFindResult()
        {
            return Convert.ToInt32(ConfigurationManager.AppSettings["MaxFindResult"]);
        }
        public static int GetShortHtml()
        {
            return Convert.ToInt32(ConfigurationManager.AppSettings["ShortHtml"]);
        }
        public static string GetIndexFolder()
        {
            return ConfigurationManager.AppSettings["IndexFolder"];
        }
        public static void SetIndexFolder(string sPath)
        {
            SaveSetting("IndexFolder", sPath);
        }
        public static void SaveSetting(string key, string value)
        {
            var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var settings = configFile.AppSettings.Settings;
            if (settings[key] == null)
            {
                settings.Add(key, value);
            }
            else
            {
                settings[key].Value = value;
            }
            configFile.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
        }
    }
}
