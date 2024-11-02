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
    public class O5C : ICommand
    {
        public static bool InRoulette = false;
        public string Command { get; } = "O5Command";
        public string[] Aliases { get; } = new[] { "O5C" };
        public string Description { get; } = "auto O5";
        public string[] Usage { get; } = new[] { "%playerID%" };
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            IEnumerable<Player> players = Player.GetProcessedData(arguments);
            Player player = players.FirstOrDefault();
            string newname = "O5";
            Log.Warn(arguments);

            if (players.Any())
            {
                response = $"{player.Nickname}'s name got changed";
                player.ClearInventory();
                player.DisplayNickname = newname;
                player.AddItem(ItemType.GunRevolver);
                player.AddItem(ItemType.Radio);
                player.AddItem(ItemType.Ammo44cal, 4);
                player.AddItem(ItemType.ArmorLight);
                player.AddItem(ItemType.Medkit);
                player.AddItem(ItemType.KeycardO5);
                player.Role.Set(PlayerRoles.RoleTypeId.Scientist, PlayerRoles.RoleSpawnFlags.None);
            }
            else response = "invalid ID";
            return true;
        }
    }
}