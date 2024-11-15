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
    public class CSC : ICommand, IUsageProvider
    {
        public static bool InRoulette = false;
        public string Command { get; } = "RedRightHandCommand";
        public string[] Aliases { get; } = new[] { "CSC" };
        public string Description { get; } = "Átírja a játékos nevét, majd átállítja Chaos Conscript role-ra, és megadja az itemeket. Több ID is írható egyszerre";
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
                    string newname = $"Chaos spy {p.DisplayNickname})";
                    response = $"changed name of {p.Nickname}";

                    p.ClearInventory();
                    p.ShowHint($"{p.DisplayNickname}");
                    p.AddItem(ItemType.GunCOM18);
                    p.AddItem(ItemType.Radio);
                    p.AddItem(ItemType.Ammo9x19, 4);
                    p.AddItem(ItemType.ArmorCombat);
                    p.AddItem(ItemType.Medkit);
                    p.AddItem(ItemType.KeycardChaosInsurgency);
                    p.Role.Set(PlayerRoles.RoleTypeId.ChaosConscript, PlayerRoles.RoleSpawnFlags.None);
                    p.DisplayNickname = newname;

                }
                response = "Change finished";
                return true;
            }
            response = "invalid usage";
            return false;
        }
    }
}