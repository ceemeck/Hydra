using System;
using System.Collections.Generic;
using System.Reflection;
using BepInEx.Configuration;

namespace HydraMenu
{
	public static class SettingsManager
	{
		private static List<Action> updateActions = new List<Action>();

		public static void Init(ConfigFile config)
		{
			foreach(Type type in Assembly.GetExecutingAssembly().GetTypes())
			{
				if(type.Namespace == null) continue;
				if(!type.Namespace.StartsWith("HydraMenu.features") && !type.Namespace.StartsWith("HydraMenu.routines") && !type.Namespace.StartsWith("HydraMenu.ui")) continue;
				if(type.Name == "MainUI") continue;

				string sectionName = type.FullName.Replace('+', '.');

				foreach(FieldInfo field in type.GetFields(BindingFlags.Public | BindingFlags.Static))
				{
					if(field.FieldType == typeof(bool))
					{
						var entry = config.Bind(sectionName, field.Name, (bool)field.GetValue(null));
						field.SetValue(null, entry.Value);
						updateActions.Add(() => {
							if(entry.Value != (bool)field.GetValue(null))
								entry.Value = (bool)field.GetValue(null);
						});
					}
				}

				foreach(PropertyInfo prop in type.GetProperties(BindingFlags.Public | BindingFlags.Static))
				{
					if(prop.PropertyType == typeof(bool) && prop.CanWrite && prop.CanRead)
					{
						var entry = config.Bind(sectionName, prop.Name, (bool)prop.GetValue(null));
						prop.SetValue(null, entry.Value);
						updateActions.Add(() => {
							if(entry.Value != (bool)prop.GetValue(null))
								entry.Value = (bool)prop.GetValue(null);
						});
					}
				}
			}
		}

		public static void Update()
		{
			foreach(Action updateAction in updateActions)
			{
				updateAction();
			}
		}
	}
}
