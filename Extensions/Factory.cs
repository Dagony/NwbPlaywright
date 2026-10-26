using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TA.Playwright.Extensions
{
    public class Factory
    {
        public object CreateInstance(Type type)
        {
            return Activator.CreateInstance(type)
                ?? throw new InvalidOperationException($"Could not create an instance of {type.FullName}.");
        }
    }
}