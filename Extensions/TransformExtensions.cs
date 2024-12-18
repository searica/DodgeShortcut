
using UnityEngine;

namespace DodgeShortcut.Extensions
{
    internal static class TransformExtensions
    {
        /// <summary>
        ///     Breadth first search.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="name"></param>
        /// <param name="child"></param>
        /// <returns></returns>
        internal static bool TryGetChild(this Transform parent, string name, out Transform child, bool breadth = true)
        {
            Utils.IterativeSearchType searchMode = breadth ? Utils.IterativeSearchType.BreadthFirst : Utils.IterativeSearchType.DepthFirst;
            child = Utils.FindChild(parent.transform, name, searchMode);
            if (!child)
            {
                Log.LogWarning($"{parent.name} does not have a child named ${name}! Could not patch dodge key hint.");
                return false;
            }
            return true;
        }
    }
}
