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

            builder.Register<MainMenuPresenter>(Lifetime.Scoped).AsImplementedInterfaces();
        }
    }
}