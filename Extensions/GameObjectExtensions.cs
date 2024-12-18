using UnityEngine;

namespace DodgeShortcut.Extensions
{
    internal static class GameObjectExtensions
    {
        /// <summary>
        ///     Breadth first search.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="name"></param>
        /// <param name="child"></param>
        /// <returns></returns>
        internal static bool TryGetChild(this GameObject parent, string name, out Transform child, bool breadth = true)
        {
            return parent.transform.TryGetChild(name, out child, breadth);
        }
    }
}
