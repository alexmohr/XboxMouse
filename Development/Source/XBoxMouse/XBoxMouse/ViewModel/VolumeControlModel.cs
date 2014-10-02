#region

using System;
using System.Drawing;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Actions.Win32Helper;
using CoreAudioApi;
using XBoxMouse.Properties;
using Timer = System.Timers.Timer;

#endregion

namespace XBoxMouse.ViewModel
{
    internal class VolumeControlModel : BaseViewModel
    {
        readonly MMDevice _audioDevice;
        private readonly Timer _hideTimer = new Timer();
        private bool _shown;
        private Image _image;

        public VolumeControlModel()
        {
            try
            {
                _hideTimer.Elapsed += (sender, args) => Fade(FadeDirection.Hide);
                _hideTimer.Interval = 3*1000;


                MMDeviceEnumerator devEnum = new MMDeviceEnumerator();
                _audioDevice = devEnum.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eMultimedia);
                _audioDevice.AudioEndpointVolume.OnVolumeNotification += delegate
                {
                    Fade(FadeDirection.Show);
                    _hideTimer.Stop();
                    _hideTimer.Start();
                    OnPropertyChanged("Volume");
                };
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to start volume window");
                throw;
            }
        }

        

        public Window View { get; set; }

        public enum FadeDirection
        {
            Show, 
            Hide
        }

        public Image Image
        {
            get { return _image; }
            set
            {
                if (Equals(value, _image)) return;
                _image = value;
                OnPropertyChanged();
            }
        }

        private Timer _showTime;

        public void Fade(FadeDirection direction)
        {
            if (_showTime != null)
            {
                _showTime.Stop();
            }
             _showTime = new Timer();
            _showTime.Interval = 50;
             _showTime.Elapsed += (sender, args) => View.Dispatcher.Invoke(
                delegate
                {
                    if (direction == FadeDirection.Show)
                    {
                        if (_shown == false)
                        {
                            _shown = true;
                            View.Opacity = 0;
                            View.Show();
                        }
                        if (View.Opacity < 1)
                        {
                            View.Opacity += 0.1;
                        }
                        else
                        {
                            _showTime.Stop();
                        }
                    }
                    else
                    {
                        if (View.Opacity > 0)
                        {
                            View.Opacity -= 0.1;
                        }
                        else
                        {
                            _showTime.Stop();
                            View.Hide();
                            _shown = false;
                        }
                    }
                });
             _showTime.Start();
        }

        public int Volume
        {
            get
            {
                int volume = (int)Math.Round(_audioDevice.AudioEndpointVolume.MasterVolumeLevelScalar * 100);
                if (volume > 75)
                {
                    Image = Resources.Volume100;
                }
                else if (volume > 50)
                {
                    Image = Resources.Volume50;
                }
                else if (volume > 0)
                {
                    Image = Resources.Volume25;
                }
                else
                {
                    Image = Resources.Volume0;
                }



                return volume;
            }
        }
    }
}