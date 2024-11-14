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
    public class FMC : ICommand, IUsageProvider
    {
        public static bool InRoulette = false;
        public string Command { get; } = "FaciltyManagerCommand";
        public string[] Aliases { get; } = new[] { "FMC" };
        public string Description { get; } = "Átírja a játékos nevét, majd átállítja scientist role-ra, és megadja az itemeket(card, medkit, radio). Több ID is írható egyszerre";
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
                    string newname = $"Facility Manager {names.ElementAt(random.Next(0, names.Length - 1))} ({p.DisplayNickname})";
                    response = $"changed name of {p.Nickname}";
                    
                    p.ClearInventory();
                    p.ShowHint($"Facility manager {names.ElementAt(random.Next(0, names.Length - 1))} ({p.DisplayNickname})");
                    p.DisplayNickname = newname;
                    p.AddItem(ItemType.Radio);
                    p.AddItem(ItemType.Medkit);
                    p.AddItem(ItemType.KeycardFacilityManager);
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