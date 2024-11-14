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
    public class O5C : ICommand, IUsageProvider
    {
        public static bool InRoulette = false;
        public string Command { get; } = "O5Command";
        public string[] Aliases { get; } = new[] { "O5C" };
        public string Description { get; } = "auto O5";
        public string[] Usage { get; } = new string[] { "playerID" };
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            IEnumerable<Player> players = Player.GetProcessedData(arguments);

            if (players.Any())
            {
                foreach (Player p in players)
                {
                    string newname = $"O5 ({p.DisplayNickname})";
                    response = $"changed name of {p.Nickname}";
                    p.ClearInventory();
                    p.DisplayNickname = newname;
                    p.AddItem(ItemType.Radio);
                    p.AddItem(ItemType.GunRevolver);
                    p.AddItem(ItemType.Ammo44cal, 3);
                    p.AddItem(ItemType.ArmorCombat);
                    p.AddItem(ItemType.Medkit);
                    p.AddItem(ItemType.KeycardO5);
                    p.Role.Set(PlayerRoles.RoleTypeId.Scientist, PlayerRoles.RoleSpawnFlags.None);

                }
                response = "Change finished";
                return true;
            }
            response = "invalid usage";
            return false;
        }
    }
}