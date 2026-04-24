using Data;
using Infrastructure.Interfaces;
using Infrastructure.Services;
using System;
using UnityEngine;
using UnityEngine.Events;
using VContainer.Unity;

namespace Gameplay.Player
{
    public class PlayerController : IPlayerController, IStartable, IDisposable
    {
        public event UnityAction<int> GoldChanged;
        public IDamageable Health { get; private set; }
        public int Gold { get; private set; }
        public int Timer { get; private set; }

        public int SkipWaveTimerGoldMultiplier { get; private set; } = 3;

        private AudioClip _onDamageSound;

        private IAudioService _audioService;

        public PlayerController(IAudioService audioService, PlayerData _data)
        {
            _audioService = audioService;

            Health = new Health();
            Health.Init(_data.MaxHeath);
            Gold = _data.StartGold;
            _onDamageSound = _data.HitSound;
        }

        public void Start()
        {
            Health.HealthChanged += playHitSound;
        }

        public void Dispose()
        {
            Health.HealthChanged -= playHitSound;
        }

        private void playHitSound(int _)
        {
            _audioService.PlaySound(_onDamageSound);
        }

        public bool TrySpendGold(int amount)
        {
            if (Gold < amount)
            {
                Debug.LogWarning("Not Enough Gold");
                return false;
            }

            Gold -= amount;
            GoldChanged?.Invoke(Gold);
            return true;

        }

        public void AddGold(int amount)
        {
            Gold += amount;
            GoldChanged?.Invoke(Gold);
        }

    }
}