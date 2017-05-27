using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PikaGames.Games.Core.Utils;

namespace PikaGames.Games.Core.UI
{
    public static class UiTheme
    {
        public static MaterialDesignTheme PrimaryTheme = MaterialDesignColors.LightBlue;
        public static MaterialDesignTheme AccentTheme = MaterialDesignColors.Yellow;
        public static MaterialDesignTheme BackgroundTheme = MaterialDesignColors.Grey;


        public static Color TitleColor = AccentTheme.GetVariant(MaterialThemeVariant.Base);
        public static Color TitleShadowColor = AccentTheme.GetVariant(MaterialThemeVariant.Hue900);
        public static int TitleShadowSize = 6;

        public static Color TextColor = PrimaryTheme.GetVariant(MaterialThemeVariant.Base);
        public static Color TextShadowColor = PrimaryTheme.GetVariant(MaterialThemeVariant.Hue900);
        public static int TextShadowSize = 2;

        public static Color TextDisabledColor = BackgroundTheme.GetVariant(MaterialThemeVariant.Base);
        public static Color TextDisabledShadowColor = BackgroundTheme.GetVariant(MaterialThemeVariant.Hue900);
        public static int TextDisabledShadowSize = 2;


        public static Color MenuTextColor = PrimaryTheme.GetVariant(MaterialThemeVariant.Base);
        public static Color MenuTextShadowColor = PrimaryTheme.GetVariant(MaterialThemeVariant.Hue900);
        public static int MenuTextShadowSize = 4;

        public static Color MenuActiveTextColor = AccentTheme.GetVariant(MaterialThemeVariant.Base);
        public static Color MenuActiveTextShadowColor = AccentTheme.GetVariant(MaterialThemeVariant.Hue900);
        public static int MenuActiveTextShadowSize = 2;
        
        
        public static Color ControlBackgroundColor = BackgroundTheme.GetVariant(MaterialThemeVariant.Hue700);
        public static Color ControlBackgroundShadowColor = BackgroundTheme.GetVariant(MaterialThemeVariant.Hue800);
        public static Color ControlForegroundColor = PrimaryTheme.GetVariant(MaterialThemeVariant.Base);
        public static Color ControlForegroundShadowColor = PrimaryTheme.GetVariant(MaterialThemeVariant.Hue900);
        public static int ControlBackgroundShadowSize = 4;
        public static int ControlForegroundShadowSize = 4;
        
        public static Color ControlActiveBackgroundColor = BackgroundTheme.GetVariant(MaterialThemeVariant.Hue600);
        public static Color ControlActiveBackgroundShadowColor = BackgroundTheme.GetVariant(MaterialThemeVariant.Hue800);
        public static Color ControlActiveForegroundColor = AccentTheme.GetVariant(MaterialThemeVariant.Base);
        public static Color ControlActiveForegroundShadowColor = AccentTheme.GetVariant(MaterialThemeVariant.Hue900);
        public static int ControlActiveBackgroundShadowSize = 4;
        public static int ControlActiveForegroundShadowSize = 4;

        public static Color DialogBackgroundColor = BackgroundTheme.GetVariant(MaterialThemeVariant.Hue600);
        public static Color DialogBackgroundShadowColor = BackgroundTheme.GetVariant(MaterialThemeVariant.Hue800);
        public static int DialogBackgroundShadowSize = 4;

        public static Color DialogTitleColor = AccentTheme.GetVariant(MaterialThemeVariant.Base);
        public static Color DialogTitleShadowColor = AccentTheme.GetVariant(MaterialThemeVariant.Hue900);
        public static int DialogTitleShadowSize = 6;

        public static Color SceneTransitionBackgroundColor = BackgroundTheme.GetVariant(MaterialThemeVariant.Hue900);

    }

    public static class UiThemeResources
    {
        
        public static Texture2D ControlBackground = TextureUtils.CreateRectangle(1, 1, UiTheme.ControlBackgroundColor);
        public static Texture2D ControlBackgroundShadow = TextureUtils.CreateRectangle(1, 1, UiTheme.ControlBackgroundShadowColor);

        public static Texture2D ControlActiveBackground = TextureUtils.CreateRectangle(1, 1, UiTheme.ControlActiveBackgroundColor);
        public static Texture2D ControlActiveBackgroundShadow = TextureUtils.CreateRectangle(1, 1, UiTheme.ControlActiveBackgroundShadowColor);

        public static Texture2D DialogBackground = TextureUtils.CreateRectangle(1, 1, UiTheme.DialogBackgroundColor);
        public static Texture2D DialogBackgroundShadow = TextureUtils.CreateRectangle(1, 1, UiTheme.DialogBackgroundShadowColor);

        public static Texture2D ControlInputCursor = TextureUtils.CreateRectangle(1, 1, UiTheme.ControlActiveForegroundColor);

    }
}
