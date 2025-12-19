using System;
using System.Diagnostics;
using UnityEngine;

namespace UnityAircraft.Game.Attribute
{
    [Conditional("UNITY_EDITOR")]
    [AttributeUsage(AttributeTargets.Field)]
    public class ButtonAttribute : PropertyAttribute
    {
        public ButtonAttribute(string title = null)
        {
            Title = title;
        }

        public string Title { get; }
    }
}
