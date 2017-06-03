using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended.NuclexGui;
using MonoGame.Extended.NuclexGui.Controls;
using MonoGame.Extended.NuclexGui.Controls.Desktop;

namespace ClientLauncher.UI
{
    class SettingsWindow : GuiWindowControl
    {


        public SettingsWindow()
        {
            Name = "settingsWindow";
            Bounds = new UniRectangle(new UniVector(new UniScalar(0f, 50), new UniScalar(0f, 50)),
                new UniVector(new UniScalar(1f, -100), new UniScalar(1f, -100)));
            Title = "Settings";

            var closeButton = new GuiCloseWindowButtonControl()
            {
                Bounds = new UniRectangle(new UniScalar(1f, -30), new UniScalar(0f, 6), new UniScalar(0f, 25),
                    new UniScalar(0f, 25)),

            };
            closeButton.Pressed += (sender, args) =>
            {
                Close();
            };
            Children.Add(closeButton);
            
            var volLabel = new GuiLabelControl("Music Volume")
            {
                Bounds = new UniRectangle(new UniScalar(0f, 25), new UniScalar(0f, 50), new UniScalar(200), new UniScalar(30))
            };

            var volSlider = new GuiHorizontalSliderControl()
            {
                ThumbSize = 0.1f,
                ThumbPosition = 0.5f,
                Bounds = new UniRectangle(new UniScalar(0f, 225), new UniScalar(0f, 50), new UniScalar(200), new UniScalar(30))
            };


            Children.Add(volLabel);
            Children.Add(volSlider);
        }
    }
}
