using System;
using System.Diagnostics;
using UnityEngine;

namespace UnityAircraft.Game
{
    [Conditional("UNITY_EDITOR")]
    [AttributeUsage(AttributeTargets.Field)]
    public class ButtonAttribute : PropertyAttribute
    {
        public ButtonAttribute(string title = null, bool showValueByColor = false)
        {
            Title = title;
            ShowValueByColor = showValueByColor;
        }

        public string Title { get; }
        public bool ShowValueByColor { get; }
    }
}
