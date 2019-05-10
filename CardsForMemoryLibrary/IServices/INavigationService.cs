namespace CardsForMemoryLibrary.IServices {
    public interface INavigationService {
        void Navigate(string Tag);
        void Init(object Frame, object NavigationViewItem);
        void Set_RememberPageName(string append);
    }
}
