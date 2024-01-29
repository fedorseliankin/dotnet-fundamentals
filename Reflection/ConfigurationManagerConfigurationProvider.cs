using System;
using System.Configuration;

namespace Reflection
{
    class ConfigurationManagerConfigurationProvider : ConfigurationComponentBase
    {
        public ConfigurationManagerConfigurationProvider(string StringProperty)
        {
            this.stringProperty = StringProperty;
        }

        [ConfigurationItem(SettingName = "string Property", File = "app.config")]
        public string stringProperty
        {
            get
            {
                return ReadSetting("SettingString");
            }

            set
            {
                AddUpdateAppSettings("SettingString", value);
            }
        }

        [ConfigurationItem(SettingName = "int Property", File = "app.config")]
        public int intProperty
        {
            get
            {
                return Int32.Parse(ReadSetting("SettingInt"));
            }

            set
            {
                AddUpdateAppSettings("SettingInt", value.ToString());
            }
        }

        [ConfigurationItem(SettingName = "float Property", File = "app.config")]
        public float floatProperty
        {
            get
            {
                return float.Parse(ReadSetting("SettingFloat"));
            }

            set
            {
                AddUpdateAppSettings("SettingFloat", value.ToString());
            }
        }

        [ConfigurationItem(SettingName = "timeSpan Property", File = "app.config")]
        public TimeSpan TimeSpanProperty
        {
            get
            {
                return TimeSpan.Parse(ReadSetting("SettingTimeSpan"));
            }

            set
            {
                AddUpdateAppSettings("SettingTimeSpan", value.ToString());
            }
        }


        public string ReadSetting(string key)
        {
            string result = String.Empty;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                result = appSettings[key] ?? "Not Found";
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error reading app settings");
            }

            return result;
        }


        public void ReadSettings()
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;

                if (appSettings.Count == 0)
                {
                    Console.WriteLine("AppSettings is empty.");
                }
                else
                {
                    foreach (var key in appSettings.AllKeys)
                    {
                        Console.WriteLine("Key: {0} Value: {1}", key, appSettings[key]);
                    }
                }
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error reading app settings");
            }
        }



        void AddUpdateAppSettings(string key, string value)
        {
            try
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
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error writing app settings");
            }
        }

    }

}

