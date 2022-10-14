using WinFormsAppMVP.Presenters;
using WinFormsAppMVP.Views;

namespace WinFormsAppMVP;

static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        // Dependency Injection (Unity, Ninject, Autofac)

        IMainView _view = new MainView();
        IAddUpdateView _addView = new AddUpdateView();


        new AddUpdatePresenter(_addView);
        new MainPresenter(_view, _addView);

        Application.Run((Form)_view);
    }
}