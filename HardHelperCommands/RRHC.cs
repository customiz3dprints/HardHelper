using System;
using System.Collections.Generic;
using System.Linq;
using CommandSystem;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.API.Features.Pickups;
using Exiled.Events.EventArgs.Player;
using InventorySystem;
using PluginAPI.Core.Items;
using UnityEngine;

namespace HardHelper.HardHelperCommands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class RRHC : ICommand
    {
        public static bool InRoulette = false;
        public string Command { get; } = "RedRightHandCommand";
        public string[] Aliases { get; } = new[] { "RRHC" };
        public string Description { get; } = "auto RedRightHand";
        public string[] Usage { get; } = new[] { "%playerID%" };
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            IEnumerable<Player> players = Player.GetProcessedData(arguments);
            Player player = players.FirstOrDefault();
            string newname = "Red Right Hand";
            Log.Warn(arguments);

            if (players.Any())
            {
                response = $"{player.Nickname}'s name got changed";
                player.ClearInventory();
                player.DisplayNickname = newname;
                player.AddItem(ItemType.GunE11SR);
                player.AddItem(ItemType.Radio);
                player.AddItem(ItemType.Ammo556x45, 4);
                player.AddItem(ItemType.ArmorHeavy);
                player.AddItem(ItemType.Medkit);
                player.AddItem(ItemType.KeycardMTFCaptain);
                player.Role.Set(PlayerRoles.RoleTypeId.Tutorial, PlayerRoles.RoleSpawnFlags.None);
            }
            else response = "invalid ID";
            return true;
        }
    }
}