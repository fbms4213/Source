using WinFormsAppMVP.Models;
using WinFormsAppMVP.Views;



namespace WinFormsAppMVP.Presenters;

public class MainPresenter
{
    private IMainView _view;
    private IAddUpdateView _addUpdateView;

    private BindingSource _bindingSource;
    private List<Student> _students;

    public MainPresenter(IMainView view, IAddUpdateView addView)
    {
        _view = view;
        _addUpdateView = addView;


        _students = new List<Student>(){
            new Student("Vasif", "Babazade", new DateTime(2004, 2, 10), 9.6f),
            new Student("Emin", "Novruz", new DateTime(2002, 9, 5), 10.5f),
            new Student("Resul", "Qasimov", new DateTime(2003, 2, 23), 11.2f),
        };


        _bindingSource = new BindingSource();
        _bindingSource.DataSource = _students;
        _view.SetStudentListBindingSource(_bindingSource);


        _view.SearchEvent += View_SearchEvent;
        _view.DeleteEvent += View_DeleteEvent;
        _view.AddEvent += View_AddEvent;
        _view.UpdateEvent += View_UpdateEvent;
    }



    private void View_SearchEvent(object? sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(_view.SearchValue))
            _bindingSource.DataSource = _students
                .Where(s => s.FirstName.Contains(_view.SearchValue, StringComparison.OrdinalIgnoreCase))
                .ToList();
        else
            _bindingSource.DataSource = _students;
    }


    private void View_DeleteEvent(object? sender, EventArgs e)
    {
        var current = _bindingSource.Current;

        if (current is null)
        {
            MessageBox.Show("An error occured", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }


        _bindingSource.RemoveCurrent();
    }


    private void View_AddEvent(object? sender, EventArgs e)
    {
        var result = _addUpdateView.ShowDialog();

        if (result == DialogResult.Cancel)
            return;


        var newStudent = new Student(_addUpdateView.FirstName, _addUpdateView.LastName, _addUpdateView.BirthOfDate, (float)_addUpdateView.Score);
       
        // _students.Add(newStudent);
        _bindingSource.Add(newStudent);
        MessageBox.Show("Successfully added", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }


    private void View_UpdateEvent(object? sender, EventArgs e)
    {

        var current = _bindingSource.Current as Student;

        if (current is null)
        {
            MessageBox.Show("An error occured", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }



        _addUpdateView.FirstName = current.FirstName;
        _addUpdateView.LastName = current.LastName;
        _addUpdateView.BirthOfDate = current.BirthOfDate;
        _addUpdateView.Score = (decimal)current.Score;


        if (_addUpdateView.ShowDialog() == DialogResult.Cancel)
            return;


        current.FirstName = _addUpdateView.FirstName;
        current.LastName = _addUpdateView.LastName;
        current.BirthOfDate = _addUpdateView.BirthOfDate;
        current.Score = (float)_addUpdateView.Score;


        _bindingSource.ResetCurrentItem();
        MessageBox.Show("Successfully updated", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}
