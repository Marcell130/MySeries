using MvvmCross.Core.ViewModels;
using MvvmCross.Platform.IoC;
using MySeries.Client.Core.ViewModels;

namespace MySeries.Client.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            //RegisterAppStart<ViewModels.MainViewModel>();
            RegisterNavigationServiceAppStart<MainViewModel>();
        }
    }
}
