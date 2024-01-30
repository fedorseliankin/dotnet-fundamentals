namespace Reflection
{
    [AttributeUsage(AttributeTargets.Property)]
    internal class ConfigurationItemAttribute : Attribute
    {

        public string SettingName
        {
            get
            {
                return SettingName;
            }
            set => SettingName = value;
        }

        public string File
        {

            get
            {
                return File;
            }
            set => File = value;
        }



    }

}

