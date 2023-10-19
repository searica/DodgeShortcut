using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;

namespace DodgeButton
{
    [HarmonyPatch(typeof(Player))]
    internal class PlayerPatch
    {

        [HarmonyPrefix]
        [HarmonyPatch(nameof(Player.Update))]
        private static void DodgePatch()
        {
            if (Player.m_localPlayer == null)
            {
                Log.LogWarning("Local player is null.");
                return;
            }
            
            // want to make sure we don't dodge if sitting in a chair?
            // piloting a ship, or holding the mast of the ship?
            if (
                Player.m_localPlayer.IsTeleporting() 
                || Player.m_localPlayer.IsAttachedToShip()  // I think this is if you're piloting?
                || Player.m_localPlayer.InPlaceMode() 
                || Console.instance.m_chatWindow.gameObject.activeInHierarchy
                || Chat.instance.m_chatWindow.gameObject.activeInHierarchy 
                || TextInput.IsVisible() 
                || StoreGui.IsVisible() 
                || InventoryGui.IsVisible() 
                || Menu.IsVisible()
            )
            {
                return;
            }
            
            if (PluginConfig.DodgeShortut.Value.IsDown())
            {
                Vector3 dodgeDir = Player.m_localPlayer.m_moveDir;
                dodgeDir.y = 0f;
                dodgeDir = dodgeDir.normalized;
                Player.m_localPlayer.Dodge(dodgeDir);
            }
        }
    }
}
