﻿using System;
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
    public class RRHC : ICommand, IUsageProvider
    {
        public static bool InRoulette = false;
        public string Command { get; } = "RedRightHandCommand";
        public string[] Aliases { get; } = new[] { "RRHC" };
        public string Description { get; } = "auto RedRightHand";
        public string[] Usage { get; } = new string[] { "playerID(s)" };
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            IEnumerable<Player> players = Player.GetProcessedData(arguments);

            if (players.Any())
            {
                foreach (Player p in players)
                {
                    string newname = $"Red Right Hand ({p.DisplayNickname})";
                    response = $"changed name of {p.Nickname}";
                    p.ClearInventory();
                    p.DisplayNickname = newname;
                    p.AddItem(ItemType.GunE11SR);
                    p.AddItem(ItemType.Radio);
                    p.AddItem(ItemType.Ammo556x45, 4);
                    p.AddItem(ItemType.ArmorHeavy);
                    p.AddItem(ItemType.Medkit);
                    p.AddItem(ItemType.KeycardMTFCaptain);
                    p.Role.Set(PlayerRoles.RoleTypeId.Tutorial, PlayerRoles.RoleSpawnFlags.None);
                    
                }
                response = "Change finished";
                return true;
            }
            response = "invalid usage";
            return false;
        }
    }
}