using UnityAircraft.Game;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace UnityAircraft.Test
{
    public class InputTestLifetimeScope : LifetimeScope
    {
        [SerializeField] private InputTestBehaviour _inputTestBehaviour;

        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            builder.RegisterInstance(_inputTestBehaviour);

            builder.Register<IAircraftInputObservable, ITickable, AircraftInputAdapter>(Lifetime.Singleton);

            builder.RegisterEntryPoint<InputTestEntryPoint>();
        }
    }
}
