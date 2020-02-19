using CLUZMobile;
using CLUZMobile.Interfaces;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CLUZ.Services
{
    public class PlayersHub
    {   
        public static HubConnection Connection { get; } = new HubConnectionBuilder()
            .WithUrl($"https://{Globals.Host}/PlayersHub")
            //.AddMessagePackProtocol()
            //.ConfigureLogging(logging =>
            //{
            //    // Log to the Output Window
            //    logging.AddDebug();

            //    // This will set ALL logging to Debug level
            //    logging.SetMinimumLevel(LogLevel.Debug);
            //})
            .Build();

        public static async Task Connect()
        {
            if (Connection.State == HubConnectionState.Disconnected)
            {
                await Connection.StartAsync();

                Connection.Closed += async (error) =>
                {
                    await Task.Delay(new Random().Next(0, 5) * 5000);
                    await Connection.StartAsync();
                };
            }
        }

        public static async Task Disonnect()
        {
            if(Connection.State == HubConnectionState.Connected)
            {
                await Connection.StopAsync();
            }
        }
    }
}
