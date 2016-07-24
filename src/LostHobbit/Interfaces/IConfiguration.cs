using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LostHobbit.Interfaces
{
    public interface IConfiguration
    {
        string GetValue(string key);
    }
}
