using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsAppMVP.Views;

public interface IMainView
{
    string SearchValue { get; }

    event EventHandler SearchEvent;
    event EventHandler AddEvent;
    event EventHandler UpdateEvent;
    event EventHandler DeleteEvent;

    void SetStudentListBindingSource(BindingSource source);
}
