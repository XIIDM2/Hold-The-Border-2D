using Infrastructure.Services;
using System;
using VContainer.Unity;

namespace Gameplay.UI
{
    public class WavesPresenter : IStartable, IDisposable
    {
        private readonly WavesView _view;
        private readonly IWaveControllerService _service;

        public WavesPresenter(WavesView view, IWaveControllerService service)
        {
            _view = view;
            _service = service;
        }

        public void Start()
        {
            _view.Init(_service.CurrentWaveIndex, _service.WavesLength);

            _service.NextWaveStarted += _view.OnNextWaveStarted;
        }

        public void Dispose()
        {
            _service.NextWaveStarted -= _view.OnNextWaveStarted;
        }

    }
}