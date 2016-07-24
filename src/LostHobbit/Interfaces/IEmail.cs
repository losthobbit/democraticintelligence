using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LostHobbit.Interfaces
{
    public interface IEmail
    {
        void Send(string subject, string html, string toAddress, string fromAddress = null);
    }
}
