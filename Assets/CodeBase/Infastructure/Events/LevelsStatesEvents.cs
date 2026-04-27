using Infrastructure.Interfaces;
using UnityEngine;

namespace Infrastructure.Events
{
    public readonly struct LevelStartedEvent : IEvent
    {
        public readonly AudioClip Music;
        public readonly AudioClip Ambience;

        public LevelStartedEvent(AudioClip Music = null, AudioClip Ambience = null)
        {
            this.Music = Music;
            this.Ambience = Ambience;
        }

    }


}