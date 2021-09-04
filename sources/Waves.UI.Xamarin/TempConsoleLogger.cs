using Waves.Core.Base.Attributes;
using Waves.Core.Plugins.Services.Interfaces;

namespace Waves.UI.Xamarin
{
    [WavesService(typeof(IWavesLogService))]
    public class TempConsoleLogger : Waves.Core.Plugins.Services.Log.Console.Service
    {
    }
}