using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Promise.Domain.Enums;
using System;
using System.Collections.Generic;

namespace Promise.UI
{
    public class ThemeManager
    {
        private Dictionary<ThemeMode, Uri> themesUri = new Dictionary<ThemeMode, Uri>()
        {
            { ThemeMode.Light, new Uri("avares://Promise.UI/Themes/LightTheme.axaml") },
            { ThemeMode.Dark, new Uri("avares://Promise.UI/Themes/DarkTheme.axaml") }
        };

        public void Select(ThemeMode theme)
        {
            Uri uri = themesUri[theme];
            object obj = AvaloniaXamlLoader.Load(uri);

            if (obj is ResourceDictionary resourceDictionary)
            {
                if (Avalonia.Application.Current != null)
                {
                    foreach (var pair in resourceDictionary)
                    {
                        Avalonia.Application.Current.Resources.Add(pair);
                    }
                }
            }
        }
    }
}
