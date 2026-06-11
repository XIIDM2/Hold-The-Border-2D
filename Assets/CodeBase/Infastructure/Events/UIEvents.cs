using Gameplay.UI;
using Infrastructure.Interfaces;

namespace Infrastructure.Events
{
    public readonly struct UIStateChanged : IEvent
    {
        public readonly UIStates CurrentState;

        public UIStateChanged(UIStates CurrentState)
        {
            this.CurrentState = CurrentState;
        }

    }
}
