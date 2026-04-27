using Data;
using Infrastructure.Events;
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
        public int SkipWaveTimerGoldMultiplier { get; private set; } = 3;

        private AudioClip _onDamageSound;

        private IEventBus _eventBus;

        public PlayerController(IEventBus eventBus, PlayerData _data)
        {
            _eventBus = eventBus;

            Health = new Health();
            Health.Init(_data.MaxHeath);
            Gold = _data.StartGold;
            _onDamageSound = _data.HitSound;
        }

        public void Start()
        {
            Health.HealthChanged += OnHealthChanged;
            Health.Death += OnDeath;
        }

        public void Dispose()
        {
            Health.HealthChanged -= OnHealthChanged;
            Health.Death -= OnDeath;
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

        private void OnHealthChanged(int _)
        {
            _eventBus.Publish(new InvokeSFX(_onDamageSound));
        }

        private void OnDeath(IDamageable _)
        {
            _eventBus.Publish(new PlayerDiedEvent());
        }

    }
}