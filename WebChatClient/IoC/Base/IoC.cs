using Ninject;

namespace WebChatClient
{
    public class IoC
    {
        public static IKernel Kernel { get; private set; } = new StandardKernel();

        // Ярлык для доступа к <see cref="IUIManager"/>
        public static IUIManager UI => IoC.Get<IUIManager>();

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
        }
    }
}
