using System.ComponentModel;
using System.Runtime.CompilerServices;
using micdah.LrControl.Annotations;

namespace micdah.LrControl.Mapping.Functions
{
    public abstract class Function : INotifyPropertyChanged
    {
        private Controller _controller;
        private bool _enabled;

        protected Function(LrControlApi.LrControlApi api)
        {
            Api = api;
        }

        protected LrControlApi.LrControlApi Api { get; }

        public Controller Controller
        {
            get { return _controller; }
            set
            {
                if (Equals(value, _controller)) return;

                if (_controller != null)
                {
                    _controller.ControllerChanged -= OnControllerChanged;
                }

                _controller = value;
                _controller.ControllerChanged += OnControllerChanged;
                OnPropertyChanged();
            }
        }

        public bool Enabled
        {
            get { return _enabled; }
            set
            {
                if (value == _enabled) return;
                _enabled = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void Enable()
        {
            Enabled = true;
        }

        public virtual void Disable()
        {
            Enabled = false;
        }

        private void OnControllerChanged(int controllerValue)
        {
            if (!Enabled) return;

            ControllerChanged(controllerValue);
        }

        protected abstract void ControllerChanged(int controllerValue);

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}