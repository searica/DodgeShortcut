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
#if DEBUG
                Log.LogInfo("Local player is null.");
#endif
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

            if (Input.GetKeyDown(PluginConfig.DodgeShortut.Value))
            {
                Vector3 dodgeDir = Player.m_localPlayer.m_moveDir;
                dodgeDir.y = 0f;
                if (dodgeDir.magnitude < 0.1f)
                {
                    dodgeDir = Player.m_localPlayer.m_lookDir;
                    dodgeDir.y = 0f;
                }
                dodgeDir.Normalize();
#if DEBUG
                Log.LogInfo($"Dodge Vector: {dodgeDir}");
#endif
                Player.m_localPlayer.Dodge(dodgeDir);
            }
        }
    }
}