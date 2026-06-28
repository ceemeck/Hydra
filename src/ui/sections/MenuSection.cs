using System;
using UnityEngine;

namespace HydraMenu.ui.sections
{
	internal class MenuSection : ISection
	{
		public MenuSection() : base("Menu") { }

		private bool isWaitingForKey = false;

		public override void Render()
		{
			// GUILayout.Label($"Texture 2D memory usage: {Texture2D.currentTextureMemory}");
			Hydra.notifications.DisableNotifications = GUILayout.Toggle(Hydra.notifications.DisableNotifications, "Disable Notifications");

			GUILayout.Label("Menu Toggle Key:");
			if(GUILayout.Button(isWaitingForKey ? "Press any key..." : Hydra.OpenMenuKey.Value.ToString()))
			{
				isWaitingForKey = true;
			}

			if (isWaitingForKey)
			{
				Event e = Event.current;
				if (e.type == EventType.KeyDown && e.keyCode != KeyCode.None)
				{
					Hydra.OpenMenuKey.Value = e.keyCode;
					// BepInEx Config auto-saves by default when Value is set, but we can force save if needed, though this is usually sufficient.
					isWaitingForKey = false;
				}
			}

			GUILayout.Label($"Primary Color: {Styles.primaryColor}");
			Styles.primaryColor = (Styles.UIColors)GUILayout.HorizontalSlider((float)Styles.primaryColor, 0, Styles.ColorValues.Count - 1);

			GUILayout.Label($"Menu Opacity: {Styles.menuOpacity * 100:F0}%");
			Styles.menuOpacity = (float)Math.Round(GUILayout.HorizontalSlider(Styles.menuOpacity, 0, 1), 4);

			GUILayout.Label($"UI Scale: {MainUI.scale:F2}x");
			MainUI.scale = (float)Math.Round(GUILayout.HorizontalSlider(MainUI.scale, 0.5f, 2.0f), 2);

			if(GUILayout.Button("Apply Changes"))
			{
				Styles.ClearCache();
			}
		}
	}
}