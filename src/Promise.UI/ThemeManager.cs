using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.Logging;
using Promise.Domain.Enums;
using System;
using System.Collections.Generic;

namespace Promise.UI
{
    public class ThemeManager
    {
        private readonly ILogger<ThemeManager> _logger;

        private static Dictionary<ThemeMode, Uri> _themesUri = new Dictionary<ThemeMode, Uri>()
        {
            { ThemeMode.Light, new Uri("avares://Promise.UI/Themes/LightTheme.axaml") },
            { ThemeMode.Dark, new Uri("avares://Promise.UI/Themes/DarkTheme.axaml") }
        };

        public ThemeManager(ILogger<ThemeManager> logger)
        {
            _logger = logger;
        }

        public void ChangeTheme(ThemeMode theme)
        {
            if (Avalonia.Application.Current == null) return;

            if (_themesUri.TryGetValue(theme, out Uri? uri))
            {
                try
                {
                    var newTheme = AvaloniaXamlLoader.Load(uri) as ResourceDictionary;
                    if (newTheme != null)
                    {
                        Avalonia.Application.Current.Resources.MergedDictionaries.Clear();
                        Avalonia.Application.Current.Resources.MergedDictionaries.Add(newTheme);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
            }
            else return;
        }
    }
}
