namespace Bridge.Navigation.Abstraction
{
    public interface INavigator
    {
        void InitNavigation();
        void Navigate(string pageId);
    }
}