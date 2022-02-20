using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;

namespace S28Maker.Error
{
    internal class S28ToastErrorHandler : S28ErrorHandler
    {
        public override Task ShowError(string message)
        {
            return Shell.Current.DisplayToastAsync(message);
        }
    }
}
