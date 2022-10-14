using WinFormsAppMVP.Presenters;
using WinFormsAppMVP.Repositories;
using WinFormsAppMVP.Views;

namespace WinFormsAppMVP;

static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        // Dependency Injection (Unity, Ninject, Autofac)

        IMainView view = new MainView();
        IAddUpdateView addView = new AddUpdateView();
        IStudentRepository repository = new EFStudentRepository();


        new AddUpdatePresenter(addView);
        new MainPresenter(view, addView, repository);

        Application.Run((Form)view);
    }
}