using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using TextChanger.ViewModel;

namespace TextChanger
{
    internal class ServiceFactory
    {
        private ServiceFactory() { }

        internal static T GetInstance<T>() => ServiceLocator.Current.GetInstance<T>();

        internal static void InitializeServices()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            // ViewModels registration
            SimpleIoc.Default.Register<MainViewModel>();

        }
    }
}
