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
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

namespace HardHelper.HardHelperCommands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class JC : ICommand, IUsageProvider
    {
        public static bool InRoulette = false;
        public string Command { get; } = "JanitorCommand";
        public string[] Aliases { get; } = new[] { "JC" };
        public string Description { get; } = "Átírja a játékos nevét, majd átállítja D-class role-ra, és megadja az itemeket. Több ID is írható egyszerre";
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
                    string newname = $"Janitor {names.ElementAt(random.Next(0, names.Length - 1))} ({p.DisplayNickname})";
                    response = $"changed name of {p.Nickname}";
                    
                    p.ClearInventory();
                    p.AddItem(ItemType.KeycardJanitor);
                    p.Role.Set(PlayerRoles.RoleTypeId.ClassD, PlayerRoles.RoleSpawnFlags.None);
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