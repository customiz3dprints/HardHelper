﻿using System;
using System.Collections.Generic;
using System.Linq;
using CommandSystem;
using CommandSystem.Commands.RemoteAdmin.ServerEvent;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.API.Features.Pickups;
using Exiled.Events.EventArgs.Player;
using InventorySystem;
using PluginAPI.Core.Items;
using PluginAPI.Events;
using Respawning;
using UnityEngine;

namespace HardHelper.HardHelperCommands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class SHC : ICommand, IUsageProvider
    {
        public static bool InRoulette = false;
        public string Command { get; } = "SerpentsHandCommand";
        public string[] Aliases { get; } = new[] { "SHC" };
        public string Description { get; } = "Átírja a játékos nevét, majd átállítja tutorial role-ra, és megadja az itemeket. Több ID is írható egyszerre";
        public string[] Usage { get; } = new string[] { "playerID(s)" };
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            IEnumerable<Player> players = Player.GetProcessedData(arguments);

            if (players.Any())
            {
                foreach (Player p in players)
                {
                    string[] names = { "Ádám", "Bence", "Dávid", "Leó", "Máté", "Márk", "Péter", "Soma", "Zsolt", "Gábor" };
                    Random random = new Random();
                    p.DisplayNickname = null;
                    string newname = $"Serpent's hand {names.ElementAt(random.Next(0, names.Length - 1))} ({p.DisplayNickname})";
                    response = $"changed name of {p.Nickname}";

                    p.ClearInventory();
                    p.AddItem(ItemType.GunA7);
                    p.AddItem(ItemType.Radio);
                    p.AddItem(ItemType.Ammo762x39, 3);
                    p.AddItem(ItemType.ArmorHeavy);
                    p.AddItem(ItemType.Medkit);
                    p.AddItem(ItemType.KeycardChaosInsurgency);
                    p.Role.Set(PlayerRoles.RoleTypeId.Tutorial, PlayerRoles.RoleSpawnFlags.None);
                    p.DisplayNickname = newname;
                    p.ShowHint($"{p.DisplayNickname}");
                }
                response = "Change finished";
                return true;
            }
            response = "invalid usage";
            return false;
        }
    }
}