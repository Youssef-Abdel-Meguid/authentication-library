using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Library.ConfigurationManager.Contract
{
    public interface IConfigurationManager
    {
        string GetConfiguration(string key);
    }
}
