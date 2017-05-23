using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PikaGames.Games.Core.UI.Controls
{
    public class UiButton : UiControl
    {
        public event Action OnPress;

        public UiButton(UiContainer container, int x, int y, string label, Action onPress) : this(container, x, y,
            label)
        {
            OnPress += onPress;
        }

        public UiButton(UiContainer container, int x, int y, string label) : base(container, x, y,
            label)
        {

        }

        public override void Focus()
        {
            base.Focus();
            OnPress?.Invoke();
        }
    }
}
