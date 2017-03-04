namespace Bridge.Navigation.Abstraction
{
    public interface IAmLoadable
    {
        /// <summary>
        /// Called when navigate to this controller
        /// </summary>
        void OnLoad();
    }
}