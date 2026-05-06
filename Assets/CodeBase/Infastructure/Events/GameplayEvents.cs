using Infrastructure.Interfaces;

namespace Infrastructure.Events
{
    public readonly struct PlayerDiedEvent : IEvent { }
    public readonly struct AllEnemiesKilledEvent : IEvent { }
}