using System.Text;
using WinFormsAppMVP.Views;

namespace WinFormsAppMVP.Presenters;

public class AddUpdatePresenter
{
    private IAddUpdateView _view;

    public AddUpdatePresenter(IAddUpdateView view)
    {
        _view = view;

        _view.SaveEvent += View_SaveEvent;
        _view.CancelEvent += View_CancelEvent; ;
    }


    private void View_SaveEvent(object? sender, EventArgs e)
    {
        StringBuilder sb = new();

        if (_view.FirstName.Length < 3)
            sb.Append($"{nameof(_view.FirstName)} is wrong.\n");


        if (_view.LastName.Length < 3)
            sb.Append($"{nameof(_view.LastName)} is wrong.\n");


        if (DateTime.Now.Year - _view.BirthOfDate.Year < 18)
            sb.Append($"{nameof(_view.BirthOfDate)} is wrong.\n");



        if (sb.Length > 0)
        {
            MessageBox.Show(sb.ToString(), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }



        _view.DialogResult = DialogResult.OK;
    }

    private void View_CancelEvent(object? sender, EventArgs e)
    {
        _view.DialogResult = DialogResult.Cancel;
    }
}
