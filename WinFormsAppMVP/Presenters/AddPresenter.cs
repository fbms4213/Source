using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsAppMVP.Views;

namespace WinFormsAppMVP.Presenters;

public class AddPresenter
{
    private IAddView _view;

    public AddPresenter(IAddView view)
    {
        _view = view;

        _view.SaveEvent += _view_SaveEvent;
        _view.CancelEvent += _view_CancelEvent; ;
    }

 
    private void _view_SaveEvent(object? sender, EventArgs e)
    {
        if(_view.FirstName.Length > 3)
        {
            ((Form)_view).DialogResult = DialogResult.OK;
            return;
        }

        MessageBox.Show("FirstName", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }

    private void _view_CancelEvent(object? sender, EventArgs e)
    {
        ((Form)_view).DialogResult = DialogResult.Cancel;
    }
}
