using System;
using System.Threading.Tasks;

namespace S28Maker.Error
{
    public abstract class S28ErrorHandler
    {
        public static S28ErrorHandler Current { get; set; }

        public abstract Task ShowError(string message);
        public Task ShowError(Exception exception)
        {
            return ShowError(GetMessage(exception));
        }

        private string GetMessage(Exception exception)
        {
            if (exception.InnerException != null)
                return GetMessage(exception.InnerException) + Environment.NewLine + exception.Message;
            return exception.Message;
        }
    }
}
