using Assets.CodeBase.Infastructure.Services;
using Cysharp.Threading.Tasks;
using Data;
using Gameplay.UI;
using Infrastructure.Factories;
using Infrastructure.Services;
using Infrastructure.Services.Bootstrappers;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Infrastructure.DI
{
    public class RootLifeTimeScope : LifetimeScope
    {
        [SerializeField] private ProjectRegistry _projectRegistry;
        [SerializeField] private GameplayRegistry _gameplayRegistry;
        [SerializeField] private SFXRegistry _SFXRegistry;
        [SerializeField] private UIRegistry _UIRegistry;
        [SerializeField] private SkillRegistry _skillRegistry;
        [SerializeField] private PlayerData _playerData;

        [SerializeField] private Fader _faderPrefab;

        protected override void Configure(IContainerBuilder builder)
        {

            builder.RegisterInstance(this.GetCancellationTokenOnDestroy());

            builder.RegisterInstance(_playerData);

            builder.RegisterInstance(_faderPrefab);

            builder.Register<IUIFactory, UIFactory>(Lifetime.Singleton);
            builder.Register<ISkillFactory, SkillFactory>(Lifetime.Singleton);

            builder.Register<AssetProviderService>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<SceneController>(Lifetime.Singleton).AsSelf();

            builder.Register<AudioService>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<SettingsService>(Lifetime.Singleton).AsImplementedInterfaces();

            builder.Register<EventBus>(Lifetime.Singleton).AsImplementedInterfaces();

            builder.RegisterInstance(_gameplayRegistry);
            builder.RegisterInstance(_projectRegistry);
            builder.RegisterInstance(_SFXRegistry);
            builder.RegisterInstance(_UIRegistry);
            builder.RegisterInstance(_skillRegistry);

            builder.RegisterEntryPoint<GameBootsTrapper>();

            builder.RegisterBuildCallback(InitSkills);

        }

        private void InitSkills(IObjectResolver container)
        {
            IEventBus eventBus = container.Resolve<IEventBus>();
            ISkillFactory skillFactory = container.Resolve<ISkillFactory>();

            foreach (SkillData skill in _skillRegistry.SkillDatas)
            {
                skill.Init(eventBus, skillFactory);
            }
        }


    }
}