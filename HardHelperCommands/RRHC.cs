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
    public class RRHC : ICommand, IUsageProvider
    {
        public static bool InRoulette = false;
        public string Command { get; } = "RedRightHandCommand";
        public string[] Aliases { get; } = new[] { "RRHC" };
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
                    string newname = $"Red Right Hand {names.ElementAt(random.Next(0, names.Length - 1))} ({p.DisplayNickname})";
                    response = $"changed name of {p.Nickname}";
                    
                    p.ClearInventory();
                    p.DisplayNickname = newname;
                    p.ShowHint($"Red Right Hand {names.ElementAt(random.Next(0, names.Length - 1))} ({p.DisplayNickname})");
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