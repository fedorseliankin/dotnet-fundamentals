using System;
using System.Reflection;

namespace Reflection
{
    class ConfigurationComponentBase
    {
        public void writeSettings(string settingName, string value)
        {
            if (settingName == null || value == null)
            {
                Console.WriteLine("Given setting or value is empty");
            }
            var type = typeof(ConfigurationManagerConfigurationProvider);
            var ctor = type.GetConstructor(new[] { typeof(string) });
            var confProvider = ctor.Invoke(new[] { "dull" });

            try
            {
                var prop = type.GetProperty(settingName);
                switch (settingName)
                {
                    case "stringProperty":
                        prop.SetValue(confProvider, value);
                        break;
                    case "intProperty":
                        prop.SetValue(confProvider, Int32.Parse(value));
                        break;
                    case "floatProperty":
                        prop.SetValue(confProvider, float.Parse(value));
                        break;
                    case "TimeSpanProperty":
                        prop.SetValue(confProvider, TimeSpan.Parse(value));
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Setting with given name is not exists");
            }
        }

        public void readSettings()
        {
            //var ctype = typeof(ConfigurationManagerConfigurationProvider);
            var asm = Assembly.GetExecutingAssembly();

            foreach (var type in asm.GetTypes())
            {
                if (type.BaseType == GetType())
                {
                    var ctor = type.GetConstructor(new[] { typeof(string) });

                    var confProvider = ctor.Invoke(new[] { "dull" });

                    var props = type.GetProperties().Where(
                        prop => Attribute.IsDefined(prop, typeof(ConfigurationItemAttribute)));


                    foreach (PropertyInfo prop in props)
                    {
                        Console.WriteLine("Setting Name: {0} ;  Setting value : {1}", prop.Name, prop.GetValue(confProvider));
                    }
                }
            }


        }

    }

}

