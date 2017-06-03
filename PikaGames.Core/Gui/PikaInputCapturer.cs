using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended.Input.InputListeners;
using MonoGame.Extended.NuclexGui;
using MonoGame.Extended.NuclexGui.Input;

namespace PikaGames.Games.Core.Gui
{
    public class PikaInputCapturer : DefaultInputCapturer, IInputCapturer
    {
        public IInputReceiver InputReceiver { get; set; }

        private readonly TouchListener _touchListener;

        public PikaInputCapturer(IServiceProvider serviceProvider)
            : this(GetInputService(serviceProvider))
        {
        }

        public PikaInputCapturer(IGuiInputService inputService) : base(inputService)
        {
            _touchListener = inputService.TouchListener;
            SubscribePikaInputDevices();
        }

        protected override void Dispose(bool disposing)
        {
            UnsubscribePikaInputDevices();
            base.Dispose(disposing);
        }

        private void SubscribePikaInputDevices()
        {
            _touchListener.TouchStarted += TouchListenerOnTouchStarted;
            _touchListener.TouchEnded += TouchListenerOnTouchEnded;
            _touchListener.TouchCancelled += TouchListenerOnTouchCancelled;
            _touchListener.TouchMoved += TouchListenerOnTouchMoved;
        }

        private void UnsubscribePikaInputDevices()
        {
            _touchListener.TouchStarted -= TouchListenerOnTouchStarted;
            _touchListener.TouchEnded -= TouchListenerOnTouchEnded;
            _touchListener.TouchCancelled -= TouchListenerOnTouchCancelled;
            _touchListener.TouchMoved -= TouchListenerOnTouchMoved;
        }

        private void TouchListenerOnTouchMoved(object sender, TouchEventArgs touchEventArgs)
        {
            InputReceiver.InjectMouseMove(touchEventArgs.Position.X, touchEventArgs.Position.Y);
        }

        private void TouchListenerOnTouchCancelled(object sender, TouchEventArgs touchEventArgs)
        {
            InputReceiver.InjectMouseMove(touchEventArgs.Position.X, touchEventArgs.Position.Y);
            InputReceiver.InjectMouseRelease(MouseButton.Left);
        }

        private void TouchListenerOnTouchEnded(object sender, TouchEventArgs touchEventArgs)
        {
            InputReceiver.InjectMouseMove(touchEventArgs.Position.X, touchEventArgs.Position.Y);
            InputReceiver.InjectMouseRelease(MouseButton.Left);
        }

        private void TouchListenerOnTouchStarted(object sender, TouchEventArgs touchEventArgs)
        {
            InputReceiver.InjectMouseMove(touchEventArgs.Position.X, touchEventArgs.Position.Y);
            InputReceiver.InjectMousePress(MouseButton.Left);
        }


        private static IGuiInputService GetInputService(IServiceProvider serviceProvider)
        {
            IGuiInputService service = (IGuiInputService)serviceProvider.GetService(typeof(IGuiInputService));
            if (service != null)
                return service;
            throw new InvalidOperationException("Using the GUI with the DefaultInputCapturer requires the IInputService. Please either add the IInputService to Game.Services by using the Nuclex.Input.InputManager in your game or provide a custom IInputCapturer implementation for the GUI and assign it before GuiManager.Initialize() is called.");
        }
    }
}
