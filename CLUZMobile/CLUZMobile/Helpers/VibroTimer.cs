using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace CLUZMobile.Helpers
{
    static class VibroTimer
    {
        private static bool _timerInProgress = false;
        public static async Task StartAsync(CancellationToken token)
        {
            if (!_timerInProgress)
            {
                _timerInProgress = true;

                for (int i = 0; i < 20; i++)
                {
                    if (token.IsCancellationRequested)
                    {
                        break;
                    }
                    await Task.Delay(1000);
                }

                while (!token.IsCancellationRequested)
                {
                    VibrateAsync();
                    await Task.Delay(3000);
                }

                _timerInProgress = false;
            }
        }

        private static async Task VibrateAsync()
        {
            try
            {
                Vibration.Vibrate(50);
                await Task.Delay(100);
                Vibration.Vibrate(50);
                await Task.Delay(100);
            }
            catch (FeatureNotSupportedException ex)
            {
                // Feature not supported on device
            }
            catch (Exception ex)
            {
                // Other error has occurred.
            }
        }
    }
}
