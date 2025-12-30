using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace UnityAircraft.Game
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private GameRuntimeSettings _runtimeSettings;

        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            builder.RegisterInstance(_runtimeSettings);

            builder.Register<ActorFactory>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<ActorContainer>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<ActorViewHierarchy>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<AssetContainer>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<AircraftInputAdapter>(Lifetime.Singleton).AsImplementedInterfaces();

            builder.Register<ActorPresenter>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<AircraftPresenter>(Lifetime.Singleton).AsSelf();

            builder.RegisterEntryPoint<GameEntryPoint>();
        }
    }
}
