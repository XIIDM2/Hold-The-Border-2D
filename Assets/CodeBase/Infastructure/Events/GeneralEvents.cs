using Infrastructure.Interfaces;
using UnityEngine;

namespace Infrastructure.Events
{
    public readonly struct InvokeSFX : IEvent
    {
        public readonly AudioClip Sound;

        public InvokeSFX(AudioClip Sound)
        { 
            this.Sound = Sound;
        }
    }
    
}