// Helpers/Settings.cs
using Newtonsoft.Json;
using SnapNPack.Models;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System.Collections.Generic;

namespace SnapNPack.Helpers
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// </summary>
    /// 

    /*Hi, to store and save preferences in the app i'm using Xamarin Settings Plugin. Its a good nuget which makes everything 
     much easier.
     **/
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants

        private const string SettingsKey = "settings_key";
        private static readonly string SettingsDefault = string.Empty;

        private const string ContainersKey = "containers_key";
        private static string ContainersDefault
        {
            get
            {
                List<ContainerModel> list = new List<ContainerModel>
                {
                    new ContainerModel
                    {
                        Id = App.MISCELLANEOUS_ITEM_BOX,
                        Destination = "Miscellaneous Stuff"
                    }
                };
                return JsonConvert.SerializeObject(list);
            }
        }

        #endregion 


        public static string GeneralSettings
        {
            get
            {
                return AppSettings.GetValueOrDefault(SettingsKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(SettingsKey, value);
            }
        }

        public static string ContainerSetting
        {
            get
            {
                return AppSettings.GetValueOrDefault(ContainersKey, ContainersDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(ContainersKey, value);
            }
        }
    }
}