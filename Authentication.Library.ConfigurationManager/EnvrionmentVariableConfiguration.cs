using Authentication.Library.ConfigurationManager.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Library.ConfigurationManager
{
    public class EnvrionmentVariableConfiguration : IConfigurationManager
    {
        public string GetConfiguration(string key)
        {
            return Environment.GetEnvironmentVariable(key);
        }
    }
}
