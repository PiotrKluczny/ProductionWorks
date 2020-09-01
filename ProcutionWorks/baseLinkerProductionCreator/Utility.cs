using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseLinkerProductionCreator
{
    class Utility
    {
        public static string GetConnectionString(string key)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");
            var configuration = builder.Build();
            var configValue = configuration[key];
            return configValue;

        }
    }
}
