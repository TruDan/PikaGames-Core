using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace PikaGames.Games.Core.Utils
{
    public static class MaterialDesignColors
    {

        private static IReadOnlyDictionary<MaterialTheme, MaterialDesignTheme> _themes;

        static MaterialDesignColors()
        {
            _themes = new ReadOnlyDictionary<MaterialTheme, MaterialDesignTheme>(
                new Dictionary<MaterialTheme, MaterialDesignTheme>()
                {
                    {MaterialTheme.Red, Red},
                    {MaterialTheme.Pink, Pink},
                    {MaterialTheme.Purple, Purple},
                    {MaterialTheme.DeepPurple, DeepPurple},
                    {MaterialTheme.Indigo, Indigo},
                    {MaterialTheme.Blue, Blue},
                    {MaterialTheme.LightBlue, LightBlue},
                    {MaterialTheme.Cyan, Cyan},
                    {MaterialTheme.Teal, Teal},
                    {MaterialTheme.Green, Green},
                    {MaterialTheme.LightGreen, LightGreen},
                    {MaterialTheme.Lime, Lime},
                    {MaterialTheme.Yellow, Yellow},
                    {MaterialTheme.Amber, Amber},
                    {MaterialTheme.Orange, Orange},
                    {MaterialTheme.DeepOrange, DeepOrange},
                    {MaterialTheme.Brown, Brown},
                    {MaterialTheme.Grey, Grey},
                    {MaterialTheme.BlueGrey, BlueGrey}
                });
        }

        public static MaterialDesignTheme GetTheme(Color color)
        {
            foreach (var theme in _themes.Values)
            {
                if (theme.ContainsColor(color))
                    return theme;
            }

            return null;
        }

        public static Color GetVariant(Color color, MaterialThemeVariant variant)
        {
            var theme = GetTheme(color);
            return theme?.GetVariant(variant) ?? Color.Transparent;
        }

        public static readonly MaterialDesignTheme Red = new MaterialDesignTheme(MaterialTheme.Red,
            new Dictionary<MaterialThemeVariant, Color>()
            {
                {MaterialThemeVariant.Hue50, new Color(255, 235, 238)},
                {MaterialThemeVariant.Hue100, new Color(255, 205, 210)},
                {MaterialThemeVariant.Hue200, new Color(239, 154, 154)},
                {MaterialThemeVariant.Hue300, new Color(229, 115, 115)},
                {MaterialThemeVariant.Hue400, new Color(239, 83, 80)},
                {MaterialThemeVariant.Hue500, new Color(244, 67, 54)},
                {MaterialThemeVariant.Hue600, new Color(229, 57, 53)},
                {MaterialThemeVariant.Hue700, new Color(211, 47, 47)},
                {MaterialThemeVariant.Hue800, new Color(198, 40, 40)},
                {MaterialThemeVariant.Hue900, new Color(183, 28, 28)},
                {MaterialThemeVariant.HueA100, new Color(255, 138, 128)},
                {MaterialThemeVariant.HueA200, new Color(255, 82, 82)},
                {MaterialThemeVariant.HueA400, new Color(255, 23, 68)},
                {MaterialThemeVariant.HueA700, new Color(213, 0, 0)}
            });

        public static readonly MaterialDesignTheme Pink = new MaterialDesignTheme(MaterialTheme.Pink,
            new Dictionary<MaterialThemeVariant, Color>()
            {
                {MaterialThemeVariant.Hue50, new Color(252, 228, 236)},
                {MaterialThemeVariant.Hue100, new Color(248, 187, 208)},
                {MaterialThemeVariant.Hue200, new Color(244, 143, 177)},
                {MaterialThemeVariant.Hue300, new Color(240, 98, 146)},
                {MaterialThemeVariant.Hue400, new Color(236, 64, 122)},
                {MaterialThemeVariant.Hue500, new Color(233, 30, 99)},
                {MaterialThemeVariant.Hue600, new Color(216, 27, 96)},
                {MaterialThemeVariant.Hue700, new Color(194, 24, 91)},
                {MaterialThemeVariant.Hue800, new Color(173, 20, 87)},
                {MaterialThemeVariant.Hue900, new Color(136, 14, 79)},
                {MaterialThemeVariant.HueA100, new Color(255, 128, 171)},
                {MaterialThemeVariant.HueA200, new Color(255, 64, 129)},
                {MaterialThemeVariant.HueA400, new Color(245, 0, 87)},
                {MaterialThemeVariant.HueA700, new Color(197, 17, 98)}
            });

        public static readonly MaterialDesignTheme Purple = new MaterialDesignTheme(MaterialTheme.Purple,
            new Dictionary<MaterialThemeVariant, Color>()
            {
                {MaterialThemeVariant.Hue50, new Color(243, 229, 245)},
                {MaterialThemeVariant.Hue100, new Color(225, 190, 231)},
                {MaterialThemeVariant.Hue200, new Color(206, 147, 216)},
                {MaterialThemeVariant.Hue300, new Color(186, 104, 200)},
                {MaterialThemeVariant.Hue400, new Color(171, 71, 188)},
                {MaterialThemeVariant.Hue500, new Color(156, 39, 176)},
                {MaterialThemeVariant.Hue600, new Color(142, 36, 170)},
                {MaterialThemeVariant.Hue700, new Color(123, 31, 162)},
                {MaterialThemeVariant.Hue800, new Color(106, 27, 154)},
                {MaterialThemeVariant.Hue900, new Color(74, 20, 140)},
                {MaterialThemeVariant.HueA100, new Color(234, 128, 252)},
                {MaterialThemeVariant.HueA200, new Color(224, 64, 251)},
                {MaterialThemeVariant.HueA400, new Color(213, 0, 249)},
                {MaterialThemeVariant.HueA700, new Color(170, 0, 255)}
            });

        public static readonly MaterialDesignTheme DeepPurple = new MaterialDesignTheme(MaterialTheme.DeepPurple,
            new Dictionary<MaterialThemeVariant, Color>()
            {
                {MaterialThemeVariant.Hue50, new Color(237, 231, 246)},
                {MaterialThemeVariant.Hue100, new Color(209, 196, 233)},
                {MaterialThemeVariant.Hue200, new Color(179, 157, 219)},
                {MaterialThemeVariant.Hue300, new Color(149, 117, 205)},
                {MaterialThemeVariant.Hue400, new Color(126, 87, 194)},
                {MaterialThemeVariant.Hue500, new Color(103, 58, 183)},
                {MaterialThemeVariant.Hue600, new Color(94, 53, 177)},
                {MaterialThemeVariant.Hue700, new Color(81, 45, 168)},
                {MaterialThemeVariant.Hue800, new Color(69, 39, 160)},
                {MaterialThemeVariant.Hue900, new Color(49, 27, 146)},
                {MaterialThemeVariant.HueA100, new Color(179, 136, 255)},
                {MaterialThemeVariant.HueA200, new Color(124, 77, 255)},
                {MaterialThemeVariant.HueA400, new Color(101, 31, 255)},
                {MaterialThemeVariant.HueA700, new Color(98, 0, 234)}
            });

        public static readonly MaterialDesignTheme Indigo = new MaterialDesignTheme(MaterialTheme.Indigo,
            new Dictionary<MaterialThemeVariant, Color>()
            {
                {MaterialThemeVariant.Hue50, new Color(232, 234, 246)},
                {MaterialThemeVariant.Hue100, new Color(197, 202, 233)},
                {MaterialThemeVariant.Hue200, new Color(159, 168, 218)},
                {MaterialThemeVariant.Hue300, new Color(121, 134, 203)},
                {MaterialThemeVariant.Hue400, new Color(92, 107, 192)},
                {MaterialThemeVariant.Hue500, new Color(63, 81, 181)},
                {MaterialThemeVariant.Hue600, new Color(57, 73, 171)},
                {MaterialThemeVariant.Hue700, new Color(48, 63, 159)},
                {MaterialThemeVariant.Hue800, new Color(40, 53, 147)},
                {MaterialThemeVariant.Hue900, new Color(26, 35, 126)},
                {MaterialThemeVariant.HueA100, new Color(140, 158, 255)},
                {MaterialThemeVariant.HueA200, new Color(83, 109, 254)},
                {MaterialThemeVariant.HueA400, new Color(61, 90, 254)},
                {MaterialThemeVariant.HueA700, new Color(48, 79, 254)}
            });

        public static readonly MaterialDesignTheme Blue = new MaterialDesignTheme(MaterialTheme.Blue,
            new Dictionary<MaterialThemeVariant, Color>()
            {
                {MaterialThemeVariant.Hue50, new Color(227, 242, 253)},
                {MaterialThemeVariant.Hue100, new Color(187, 222, 251)},
                {MaterialThemeVariant.Hue200, new Color(144, 202, 249)},
                {MaterialThemeVariant.Hue300, new Color(100, 181, 246)},
                {MaterialThemeVariant.Hue400, new Color(66, 165, 245)},
                {MaterialThemeVariant.Hue500, new Color(33, 150, 243)},
                {MaterialThemeVariant.Hue600, new Color(30, 136, 229)},
                {MaterialThemeVariant.Hue700, new Color(25, 118, 210)},
                {MaterialThemeVariant.Hue800, new Color(21, 101, 192)},
                {MaterialThemeVariant.Hue900, new Color(13, 71, 161)},
                {MaterialThemeVariant.HueA100, new Color(130, 177, 255)},
                {MaterialThemeVariant.HueA200, new Color(68, 138, 255)},
                {MaterialThemeVariant.HueA400, new Color(41, 121, 255)},
                {MaterialThemeVariant.HueA700, new Color(41, 98, 255)}
            });

        public static readonly MaterialDesignTheme LightBlue = new MaterialDesignTheme(MaterialTheme.LightBlue,
            new Dictionary<MaterialThemeVariant, Color>()
            {
                {MaterialThemeVariant.Hue50, new Color(225, 245, 254)},
                {MaterialThemeVariant.Hue100, new Color(179, 229, 252)},
                {MaterialThemeVariant.Hue200, new Color(129, 212, 250)},
                {MaterialThemeVariant.Hue300, new Color(79, 195, 247)},
                {MaterialThemeVariant.Hue400, new Color(41, 182, 246)},
                {MaterialThemeVariant.Hue500, new Color(3, 169, 244)},
                {MaterialThemeVariant.Hue600, new Color(3, 155, 229)},
                {MaterialThemeVariant.Hue700, new Color(2, 136, 209)},
                {MaterialThemeVariant.Hue800, new Color(2, 119, 189)},
                {MaterialThemeVariant.Hue900, new Color(1, 87, 155)},
                {MaterialThemeVariant.HueA100, new Color(128, 216, 255)},
                {MaterialThemeVariant.HueA200, new Color(64, 196, 255)},
                {MaterialThemeVariant.HueA400, new Color(0, 176, 255)},
                {MaterialThemeVariant.HueA700, new Color(0, 145, 234)}
            });

        public static readonly MaterialDesignTheme Cyan = new MaterialDesignTheme(MaterialTheme.Cyan,
            new Dictionary<MaterialThemeVariant, Color>()
            {
                {MaterialThemeVariant.Hue50, new Color(224, 247, 250)},
                {MaterialThemeVariant.Hue100, new Color(178, 235, 242)},
                {MaterialThemeVariant.Hue200, new Color(128, 222, 234)},
                {MaterialThemeVariant.Hue300, new Color(77, 208, 225)},
                {MaterialThemeVariant.Hue400, new Color(38, 198, 218)},
                {MaterialThemeVariant.Hue500, new Color(0, 188, 212)},
                {MaterialThemeVariant.Hue600, new Color(0, 172, 193)},
                {MaterialThemeVariant.Hue700, new Color(0, 151, 167)},
                {MaterialThemeVariant.Hue800, new Color(0, 131, 143)},
                {MaterialThemeVariant.Hue900, new Color(0, 96, 100)},
                {MaterialThemeVariant.HueA100, new Color(132, 255, 255)},
                {MaterialThemeVariant.HueA200, new Color(24, 255, 255)},
                {MaterialThemeVariant.HueA400, new Color(0, 229, 255)},
                {MaterialThemeVariant.HueA700, new Color(0, 184, 212)}
            });

        public static readonly MaterialDesignTheme Teal = new MaterialDesignTheme(MaterialTheme.Teal,
            new Dictionary<MaterialThemeVariant, Color>()
            {
                {MaterialThemeVariant.Hue50, new Color(224, 242, 241)},
                {MaterialThemeVariant.Hue100, new Color(178, 223, 219)},
                {MaterialThemeVariant.Hue200, new Color(128, 203, 196)},
                {MaterialThemeVariant.Hue300, new Color(77, 182, 172)},
                {MaterialThemeVariant.Hue400, new Color(38, 166, 154)},
                {MaterialThemeVariant.Hue500, new Color(0, 150, 136)},
                {MaterialThemeVariant.Hue600, new Color(0, 137, 123)},
                {MaterialThemeVariant.Hue700, new Color(0, 121, 107)},
                {MaterialThemeVariant.Hue800, new Color(0, 105, 92)},
                {MaterialThemeVariant.Hue900, new Color(0, 77, 64)},
                {MaterialThemeVariant.HueA100, new Color(167, 255, 235)},
                {MaterialThemeVariant.HueA200, new Color(100, 255, 218)},
                {MaterialThemeVariant.HueA400, new Color(29, 233, 182)},
                {MaterialThemeVariant.HueA700, new Color(0, 191, 165)}
            });

        public static readonly MaterialDesignTheme Green = new MaterialDesignTheme(MaterialTheme.Green,
            new Dictionary<MaterialThemeVariant, Color>()
            {
                {MaterialThemeVariant.Hue50, new Color(232, 245, 233)},
                {MaterialThemeVariant.Hue100, new Color(200, 230, 201)},
                {MaterialThemeVariant.Hue200, new Color(165, 214, 167)},
                {MaterialThemeVariant.Hue300, new Color(129, 199, 132)},
                {MaterialThemeVariant.Hue400, new Color(102, 187, 106)},
                {MaterialThemeVariant.Hue500, new Color(76, 175, 80)},
                {MaterialThemeVariant.Hue600, new Color(67, 160, 71)},
                {MaterialThemeVariant.Hue700, new Color(56, 142, 60)},
                {MaterialThemeVariant.Hue800, new Color(46, 125, 50)},
                {MaterialThemeVariant.Hue900, new Color(27, 94, 32)},
                {MaterialThemeVariant.HueA100, new Color(185, 246, 202)},
                {MaterialThemeVariant.HueA200, new Color(105, 240, 174)},
                {MaterialThemeVariant.HueA400, new Color(0, 230, 118)},
                {MaterialThemeVariant.HueA700, new Color(0, 200, 83)}
            });

        public static readonly MaterialDesignTheme LightGreen = new MaterialDesignTheme(MaterialTheme.LightGreen,
            new Dictionary<MaterialThemeVariant, Color>()
            {
                {MaterialThemeVariant.Hue50, new Color(241, 248, 233)},
                {MaterialThemeVariant.Hue100, new Color(220, 237, 200)},
                {MaterialThemeVariant.Hue200, new Color(197, 225, 165)},
                {MaterialThemeVariant.Hue300, new Color(174, 213, 129)},
                {MaterialThemeVariant.Hue400, new Color(156, 204, 101)},
                {MaterialThemeVariant.Hue500, new Color(139, 195, 74)},
                {MaterialThemeVariant.Hue600, new Color(124, 179, 66)},
                {MaterialThemeVariant.Hue700, new Color(104, 159, 56)},
                {MaterialThemeVariant.Hue800, new Color(85, 139, 47)},
                {MaterialThemeVariant.Hue900, new Color(51, 105, 30)},
                {MaterialThemeVariant.HueA100, new Color(204, 255, 144)},
                {MaterialThemeVariant.HueA200, new Color(178, 255, 89)},
                {MaterialThemeVariant.HueA400, new Color(118, 255, 3)},
                {MaterialThemeVariant.HueA700, new Color(100, 221, 23)}
            });

        public static readonly MaterialDesignTheme Lime = new MaterialDesignTheme(MaterialTheme.Lime,
            new Dictionary<MaterialThemeVariant, Color>()
            {
                {MaterialThemeVariant.Hue50, new Color(249, 251, 231)},
                {MaterialThemeVariant.Hue100, new Color(240, 244, 195)},
                {MaterialThemeVariant.Hue200, new Color(230, 238, 156)},
                {MaterialThemeVariant.Hue300, new Color(220, 231, 117)},
                {MaterialThemeVariant.Hue400, new Color(212, 225, 87)},
                {MaterialThemeVariant.Hue500, new Color(205, 220, 57)},
                {MaterialThemeVariant.Hue600, new Color(192, 202, 51)},
                {MaterialThemeVariant.Hue700, new Color(175, 180, 43)},
                {MaterialThemeVariant.Hue800, new Color(158, 157, 36)},
                {MaterialThemeVariant.Hue900, new Color(130, 119, 23)},
                {MaterialThemeVariant.HueA100, new Color(244, 255, 129)},
                {MaterialThemeVariant.HueA200, new Color(238, 255, 65)},
                {MaterialThemeVariant.HueA400, new Color(198, 255, 0)},
                {MaterialThemeVariant.HueA700, new Color(174, 234, 0)}
            });

        public static readonly MaterialDesignTheme Yellow = new MaterialDesignTheme(MaterialTheme.Yellow,
            new Dictionary<MaterialThemeVariant, Color>()
            {
                {MaterialThemeVariant.Hue50, new Color(255, 253, 231)},
                {MaterialThemeVariant.Hue100, new Color(255, 249, 196)},
                {MaterialThemeVariant.Hue200, new Color(255, 245, 157)},
                {MaterialThemeVariant.Hue300, new Color(255, 241, 118)},
                {MaterialThemeVariant.Hue400, new Color(255, 238, 88)},
                {MaterialThemeVariant.Hue500, new Color(255, 235, 59)},
                {MaterialThemeVariant.Hue600, new Color(253, 216, 53)},
                {MaterialThemeVariant.Hue700, new Color(251, 192, 45)},
                {MaterialThemeVariant.Hue800, new Color(249, 168, 37)},
                {MaterialThemeVariant.Hue900, new Color(245, 127, 23)},
                {MaterialThemeVariant.HueA100, new Color(255, 255, 141)},
                {MaterialThemeVariant.HueA200, new Color(255, 255, 0)},
                {MaterialThemeVariant.HueA400, new Color(255, 234, 0)},
                {MaterialThemeVariant.HueA700, new Color(255, 214, 0)}
            });

        public static readonly MaterialDesignTheme Amber = new MaterialDesignTheme(MaterialTheme.Amber,
            new Dictionary<MaterialThemeVariant, Color>()
            {
                {MaterialThemeVariant.Hue50, new Color(255, 248, 225)},
                {MaterialThemeVariant.Hue100, new Color(255, 236, 179)},
                {MaterialThemeVariant.Hue200, new Color(255, 224, 130)},
                {MaterialThemeVariant.Hue300, new Color(255, 213, 79)},
                {MaterialThemeVariant.Hue400, new Color(255, 202, 40)},
                {MaterialThemeVariant.Hue500, new Color(255, 193, 7)},
                {MaterialThemeVariant.Hue600, new Color(255, 179, 0)},
                {MaterialThemeVariant.Hue700, new Color(255, 160, 0)},
                {MaterialThemeVariant.Hue800, new Color(255, 143, 0)},
                {MaterialThemeVariant.Hue900, new Color(255, 111, 0)},
                {MaterialThemeVariant.HueA100, new Color(255, 229, 127)},
                {MaterialThemeVariant.HueA200, new Color(255, 215, 64)},
                {MaterialThemeVariant.HueA400, new Color(255, 196, 0)},
                {MaterialThemeVariant.HueA700, new Color(255, 171, 0)}
            });

        public static readonly MaterialDesignTheme Orange = new MaterialDesignTheme(MaterialTheme.Orange,
            new Dictionary<MaterialThemeVariant, Color>()
            {
                {MaterialThemeVariant.Hue50, new Color(255, 243, 224)},
                {MaterialThemeVariant.Hue100, new Color(255, 224, 178)},
                {MaterialThemeVariant.Hue200, new Color(255, 204, 128)},
                {MaterialThemeVariant.Hue300, new Color(255, 183, 77)},
                {MaterialThemeVariant.Hue400, new Color(255, 167, 38)},
                {MaterialThemeVariant.Hue500, new Color(255, 152, 0)},
                {MaterialThemeVariant.Hue600, new Color(251, 140, 0)},
                {MaterialThemeVariant.Hue700, new Color(245, 124, 0)},
                {MaterialThemeVariant.Hue800, new Color(239, 108, 0)},
                {MaterialThemeVariant.Hue900, new Color(230, 81, 0)},
                {MaterialThemeVariant.HueA100, new Color(255, 209, 128)},
                {MaterialThemeVariant.HueA200, new Color(255, 171, 64)},
                {MaterialThemeVariant.HueA400, new Color(255, 145, 0)},
                {MaterialThemeVariant.HueA700, new Color(255, 109, 0)}
            });

        public static readonly MaterialDesignTheme DeepOrange = new MaterialDesignTheme(MaterialTheme.DeepOrange,
            new Dictionary<MaterialThemeVariant, Color>()
            {
                {MaterialThemeVariant.Hue50, new Color(251, 233, 231)},
                {MaterialThemeVariant.Hue100, new Color(255, 204, 188)},
                {MaterialThemeVariant.Hue200, new Color(255, 171, 145)},
                {MaterialThemeVariant.Hue300, new Color(255, 138, 101)},
                {MaterialThemeVariant.Hue400, new Color(255, 112, 67)},
                {MaterialThemeVariant.Hue500, new Color(255, 87, 34)},
                {MaterialThemeVariant.Hue600, new Color(244, 81, 30)},
                {MaterialThemeVariant.Hue700, new Color(230, 74, 25)},
                {MaterialThemeVariant.Hue800, new Color(216, 67, 21)},
                {MaterialThemeVariant.Hue900, new Color(191, 54, 12)},
                {MaterialThemeVariant.HueA100, new Color(255, 158, 128)},
                {MaterialThemeVariant.HueA200, new Color(255, 110, 64)},
                {MaterialThemeVariant.HueA400, new Color(255, 61, 0)},
                {MaterialThemeVariant.HueA700, new Color(221, 44, 0)}
            });

        public static readonly MaterialDesignTheme Brown = new MaterialDesignTheme(MaterialTheme.Brown,
            new Dictionary<MaterialThemeVariant, Color>()
            {
                {MaterialThemeVariant.Hue50, new Color(239, 235, 233)},
                {MaterialThemeVariant.Hue100, new Color(215, 204, 200)},
                {MaterialThemeVariant.Hue200, new Color(188, 170, 164)},
                {MaterialThemeVariant.Hue300, new Color(161, 136, 127)},
                {MaterialThemeVariant.Hue400, new Color(141, 110, 99)},
                {MaterialThemeVariant.Hue500, new Color(121, 85, 72)},
                {MaterialThemeVariant.Hue600, new Color(109, 76, 65)},
                {MaterialThemeVariant.Hue700, new Color(93, 64, 55)},
                {MaterialThemeVariant.Hue800, new Color(78, 52, 46)},
                {MaterialThemeVariant.Hue900, new Color(62, 39, 35)}
            });

        public static readonly MaterialDesignTheme Grey = new MaterialDesignTheme(MaterialTheme.Grey,
            new Dictionary<MaterialThemeVariant, Color>()
            {
                {MaterialThemeVariant.Hue50, new Color(250, 250, 250)},
                {MaterialThemeVariant.Hue100, new Color(245, 245, 245)},
                {MaterialThemeVariant.Hue200, new Color(238, 238, 238)},
                {MaterialThemeVariant.Hue300, new Color(224, 224, 224)},
                {MaterialThemeVariant.Hue400, new Color(189, 189, 189)},
                {MaterialThemeVariant.Hue500, new Color(158, 158, 158)},
                {MaterialThemeVariant.Hue600, new Color(117, 117, 117)},
                {MaterialThemeVariant.Hue700, new Color(97, 97, 97)},
                {MaterialThemeVariant.Hue800, new Color(66, 66, 66)},
                {MaterialThemeVariant.Hue900, new Color(33, 33, 33)}
            });

        public static readonly MaterialDesignTheme BlueGrey = new MaterialDesignTheme(MaterialTheme.BlueGrey,
            new Dictionary<MaterialThemeVariant, Color>()
            {
                {MaterialThemeVariant.Hue50, new Color(236, 239, 241)},
                {MaterialThemeVariant.Hue100, new Color(207, 216, 220)},
                {MaterialThemeVariant.Hue200, new Color(176, 190, 197)},
                {MaterialThemeVariant.Hue300, new Color(144, 164, 174)},
                {MaterialThemeVariant.Hue400, new Color(120, 144, 156)},
                {MaterialThemeVariant.Hue500, new Color(96, 125, 139)},
                {MaterialThemeVariant.Hue600, new Color(84, 110, 122)},
                {MaterialThemeVariant.Hue700, new Color(69, 90, 100)},
                {MaterialThemeVariant.Hue800, new Color(55, 71, 79)},
                {MaterialThemeVariant.Hue900, new Color(38, 50, 56)},

            });
    }

    public enum MaterialTheme
    {
        Red,
        Pink,
        Purple,
        DeepPurple,
        Indigo,
        Blue,
        LightBlue,
        Cyan,
        Teal,
        Green,
        LightGreen,
        Lime,
        Yellow,
        Amber,
        Orange,
        DeepOrange,
        Brown,
        Grey,
        BlueGrey
    }

    public enum MaterialThemeVariant
    {
        Base,
        Hue50,
        Hue100,
        Hue200,
        Hue300,
        Hue400,
        Hue500,
        Hue600,
        Hue700,
        Hue800,
        Hue900,
        HueA100,
        HueA200,
        HueA400,
        HueA700
    }

    public class MaterialDesignTheme
    {
        public MaterialTheme Theme { get; }

        private readonly IReadOnlyDictionary<MaterialThemeVariant, Color> _variants;

        internal MaterialDesignTheme(MaterialTheme theme, IDictionary<MaterialThemeVariant, Color> variants)
        {
            Theme = theme;

            variants.Add(MaterialThemeVariant.Base, variants[MaterialThemeVariant.Hue500]);

            _variants = new ReadOnlyDictionary<MaterialThemeVariant, Color>(variants);
        }

        public Color GetVariant(MaterialThemeVariant variant)
        {
            Color color = _variants[MaterialThemeVariant.Base];
            _variants.TryGetValue(variant, out color);
            return color;
        }

        internal bool ContainsColor(Color color)
        {
            foreach (var c in _variants.Values)
            {
                if (color == c) return true;
            }
            return false;
        }
    }
}
