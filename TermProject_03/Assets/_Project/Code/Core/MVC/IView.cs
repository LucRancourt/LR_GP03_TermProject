namespace _Project.Code.Core.MVC
{
    public interface IView
    {
        void Initialize();
        void Show();
        void Hide();
        void UpdateDisplay();
        void Dispose();
    }

    public interface IView<T> : IView
    {
        void UpdateDisplay(T data);
        void UpdateDisplay(T data1, T data2);
    }

    public interface IView<T1, T2> : IView
    {
        void UpdateDisplay(T1 data1, T2 data2);
    }
}