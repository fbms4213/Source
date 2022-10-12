using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsAppMVP.Models;
using WinFormsAppMVP.Views;

namespace WinFormsAppMVP.Presenters;

public class MainPresenter
{
    private IMainView _view;
    private IAddView _addView;

    private BindingSource _bindingSource;
    private List<Student> _students;

    public MainPresenter(IMainView view, IAddView addView)
    {
        _view = view;
        _addView = addView;


        _students = new List<Student>(){
            new Student("Vasif", "Babazade", new DateOnly(2004, 2, 10), 9.6f),
            new Student("Emin", "Novruz", new DateOnly(2007, 9, 5), 10.5f),
            new Student("Resul", "Qasimov", new DateOnly(2006, 2, 23), 11.2f),
        };


        _bindingSource = new BindingSource();
        _bindingSource.DataSource = _students;
        _view.SetStudentListBindingSource(_bindingSource);


        _view.SearchEvent += _view_SearchEvent;
        _view.DeleteEvent += _view_DeleteEvent;
        _view.AddEvent += _view_AddEvent;
    }

    

    private void _view_SearchEvent(object? sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(_view.SearchValue))
            _bindingSource.DataSource = _students
                .Where(s => s.FirstName.ToUpper().Contains(_view.SearchValue.ToUpper())).ToList();
        else
            _bindingSource.DataSource = _students;


        _view.SetStudentListBindingSource(_bindingSource);
    }

    private void _view_DeleteEvent(object? sender, EventArgs e)
    {
        var current = _bindingSource.Current;

        if (current is not null)
        {
            _students.Remove((Student)current);
            _view.SetStudentListBindingSource(_bindingSource);
        }
    }

    private void _view_AddEvent(object? sender, EventArgs e)
    {
        var result = ((Form)_addView).ShowDialog();

        if (result == DialogResult.Cancel)
            return;


        var newStudent = new Student(_addView.FirstName, _addView.LastName, _addView.BirthOfDate, _addView.Score);
        _students.Add(newStudent);
        _view.SetStudentListBindingSource(_bindingSource);
    }
}
