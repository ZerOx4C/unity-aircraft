using System;
using UnityEngine;

namespace UnityAircraft.Editor
{
    public class GUIDisposable : IDisposable
    {
        private readonly Color _oldColor = GUI.color;
        private readonly Color _oldBackgroundColor = GUI.backgroundColor;
        private readonly Color _oldContentColor = GUI.contentColor;

        public void Dispose()
        {
            GUI.color = _oldColor;
            GUI.backgroundColor = _oldBackgroundColor;
            GUI.contentColor = _oldContentColor;
        }
    }
}
