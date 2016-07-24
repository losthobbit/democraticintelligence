using LostHobbit.Interfaces.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LostHobbit.Services.Events
{
    public class UserActionEvent : IUserActionEvent
    {
        public virtual void Action(int userId)
        {
        }
    }
}
