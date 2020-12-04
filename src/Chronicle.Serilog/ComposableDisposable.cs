using System;

namespace Chronicle.Serilog
{
    public class ComposableDisposable : IDisposable
    {
        readonly IDisposable _disposable;
        readonly IDisposable _parent;

        public ComposableDisposable(IDisposable disposable, IDisposable parent = null)
        {
            _disposable = disposable;
            _parent = parent;
        }

        public void Dispose()
        {
            _disposable?.Dispose();
            _parent?.Dispose();
        }

        public ComposableDisposable Compose(IDisposable disposable)
        {
            return new ComposableDisposable(disposable, this);
        }
    }
}