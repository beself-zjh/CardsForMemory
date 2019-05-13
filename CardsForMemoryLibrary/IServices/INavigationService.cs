namespace CardsForMemoryLibrary.IServices {
    public interface INavigationService {

        /// <summary>
        /// <para>"home":frame.Navigate(typeof(HomePage));</para>
        /// <para>"package":frame.Navigate(typeof(PackagePage));</para>
        /// <para>"remember":frame.Navigate(typeof(RememberPage));</para>
        /// <para>"music":frame.Navigate(typeof(MusicPage));</para>
        /// <para>"setting":frame.Navigate(typeof(SettingsPage));</para>
        /// <para>"package info":new PackageInfo();</para>
        /// <para>"cards":frame.Navigate(typeof(EditPackagePage));</para>
        /// <para>"card":new CardInfo();</para>
        /// <para>"default:throw new NotImplementedException();</para>
        /// </summary>
        /// <param name="Tag">代表着目的地的字符串</param>
        void Navigate(string Tag);

        void Init(object Frame, object NavigationViewItem);
        void Set_RememberPageName(string append);
    }
}
