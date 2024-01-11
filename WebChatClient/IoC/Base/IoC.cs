using Ninject;

namespace WebChatClient
{
    public class IoC
    {
        public static IKernel Kernel { get; private set; } = new StandardKernel();

        // Ярлык для доступа к <see cref="IUIManager"/>
        public static IUIManager UI => IoC.Get<IUIManager>();

        // Ярлык для доступа к <see cref="AppVM"/>
        public static AppVM Application => IoC.Get<AppVM>();

        // Ярлык для доступа к <see cref="SettingsViewModel"/>
        public static SettingsVM Settings => IoC.Get<SettingsVM>();

        public static T Get<T>()
        {
            return Kernel.Get<T>();
        }

        public static void Setup()
        {
            BindViewModels();
        }

        private static void BindViewModels()
        {
            Kernel.Bind<AppVM>().ToConstant(new AppVM());

            // Привязка к одному экземпляру модели представления настроек
            Kernel.Bind<SettingsVM>().ToConstant(new SettingsVM());
        }
    }
}
