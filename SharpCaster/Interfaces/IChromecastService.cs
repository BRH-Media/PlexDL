using SharpCaster.Models;
using System.Threading.Tasks;

namespace SharpCaster.Services
{
    public interface IChromecastService
    {
        DeviceLocator DeviceLocator { get; }
        ChromeCastClient ChromeCastClient { get; }
        Chromecast ConnectedChromecast { get; set; }

        void ConnectToChromecast(Chromecast chromecast);

        void StopLocatingDevices();

        Task StartLocatingDevices();

        ChromecastService Current { get; }
    }
}