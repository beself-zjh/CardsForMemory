using CardsForMemoryLibrary.IServices;
using CardsForMemoryLibrary.Services;
using CardsForMemoryLibrary.ViewModels;
using GalaSoft.MvvmLight.Ioc;

namespace CardsForMemory.Locator {
    /// <summary>
    ///     定位器
    /// </summary>
    public class ViewModelLocator {
        /// <summary>
        ///     ViewModel定位器单件
        /// </summary>
        public static readonly ViewModelLocator Instance = new ViewModelLocator();

        public CardsPageViewModel CardsPageViewModel => SimpleIoc.Default.GetInstance<CardsPageViewModel>();

        public RememberPageViewModel RememberPageViewModel => SimpleIoc.Default.GetInstance<RememberPageViewModel>();

        /// <summary>
        ///     私有构造
        /// </summary>
        private ViewModelLocator() {
            SimpleIoc.Default.Register<ICardService, CardService>();
            SimpleIoc.Default.Register<IPackageService, PackageService>();
            SimpleIoc.Default.Register<ISqliteConnectionService, SqliteConnectionService>();
            SimpleIoc.Default.Register<IFeedbackService, FeedbackService>();
            SimpleIoc.Default.Register<IReviseService, ReviseService>();
            SimpleIoc.Default.Register<CardsPageViewModel>();
            SimpleIoc.Default.Register<RememberPageViewModel>();

        }
    }
}
