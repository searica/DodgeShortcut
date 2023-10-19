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
            
            if (
                Player.m_localPlayer.IsTeleporting() 
                || Player.m_localPlayer.IsAttachedToShip() 
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

            if (Input.GetKeyDown(PluginConfig.DodgeKey.Value.MainKey))
            {
                Vector3 dodgeDir = Player.m_localPlayer.m_moveDir;
                dodgeDir.y = 0f;
                dodgeDir = dodgeDir.normalized;
                Player.m_localPlayer.Dodge(dodgeDir);
            }
        }
    }
}
