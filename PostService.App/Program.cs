using System.Diagnostics;
using System.Reflection;

namespace PostService.App
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.ThrowException);
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }

        static void OnUnhandledException(object sender, UnhandledExceptionEventArgs args)
        {
            Exception exception = (Exception)args.ExceptionObject;

            if (exception is TargetInvocationException targetEx && targetEx.InnerException != null)
            {
                exception = targetEx.InnerException;
            }

            string details = $"Час: {DateTime.Now}{Environment.NewLine}" +
                             $"Тип: {exception.GetType().FullName}{Environment.NewLine}" +
                             $"Повідомлення: {exception.Message}{Environment.NewLine}" +
                             $"Стек: {exception.StackTrace}";

            Debug.WriteLine(details);

            MessageBox.Show(
                "Виникла непередбачена помилка. Програму буде закрито.",
                "Помилка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }
    }
}
