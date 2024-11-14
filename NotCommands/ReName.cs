using Exiled.Events.EventArgs.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardHelper.NotCommands
{
    public static class ReName
    {
        public static void OnDied(DiedEventArgs ev)
        {
            ev.Player.DisplayNickname = null;
        }
    }
}
