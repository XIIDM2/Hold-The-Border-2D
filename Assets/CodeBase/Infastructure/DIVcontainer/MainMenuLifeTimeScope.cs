using Gameplay.UI;
using VContainer;
using VContainer.Unity;

namespace Infrastructure.DI
{
    public class MainMenuLifeTimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<MainMenuView>();
            builder.RegisterComponentInHierarchy<SettingsView>();

            builder.Register<MainMenuPresenter>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<SettingsPresenter>(Lifetime.Scoped).AsImplementedInterfaces();
        }
    }
}