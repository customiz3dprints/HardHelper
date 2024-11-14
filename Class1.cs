using Exiled.API.Features;
using Exiled.Events.EventArgs;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.Handlers;
using HardHelper.NotCommands;
using System;
using player = Exiled.Events.Handlers.Player;


namespace plugin
{
    public class Class1 : Plugin<Config>
    {
        public static Class1 Instance;
        public override string Name => "HardHelper";
        public override string Author => "DDani6";
        public override void OnEnabled()
        {
            player.Died += ReName.OnDied;
            base.OnEnabled();
        }
        public override void OnDisabled()
        {
            Instance = null;
            player.Died -= ReName.OnDied;
            base.OnDisabled();
        }

        public override void OnReloaded()
        {

            base.OnReloaded();
        }
    }

}
