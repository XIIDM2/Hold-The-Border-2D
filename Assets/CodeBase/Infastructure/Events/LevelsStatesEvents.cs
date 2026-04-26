using Infrastructure.Interfaces;
using UnityEngine;

namespace Infrastructure.Events
{
    public record LevelStartedEvent(AudioClip Music, AudioClip Ambience) : IEvent;

}