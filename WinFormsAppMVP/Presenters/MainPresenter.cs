using WinFormsAppMVP.Models;
using WinFormsAppMVP.Repositories;
using WinFormsAppMVP.Views;



namespace WinFormsAppMVP.Presenters;

public class MainPresenter
{
    private readonly IMainView _view;
    private readonly IAddUpdateView _addUpdateView;
    private readonly IStudentRepository _repository;


    private readonly BindingSource _bindingSource;



    public MainPresenter(IMainView view, IAddUpdateView addView, IStudentRepository repository)
    {
        _view = view;
        _addUpdateView = addView;
        _repository = repository;



        _bindingSource = new BindingSource();
        _bindingSource.DataSource = _repository.GetList();
        _view.SetStudentListBindingSource(_bindingSource);


        _view.SearchEvent += View_SearchEvent;
        _view.DeleteEvent += View_DeleteEvent;
        _view.AddEvent += View_AddEvent;
        _view.UpdateEvent += View_UpdateEvent;
    }



    private void View_SearchEvent(object? sender, EventArgs e)
    {

        bool isNullOrWhiteSpace = string.IsNullOrWhiteSpace(_view.SearchValue);

        _bindingSource.DataSource = isNullOrWhiteSpace switch
        {
            true => _bindingSource.DataSource = _repository.GetList(),

            false => _repository.GetList(s =>
            s.FirstName.Contains(_view.SearchValue, StringComparison.OrdinalIgnoreCase)
            ||
            s.LastName.Contains(_view.SearchValue, StringComparison.OrdinalIgnoreCase))
        };
    }


    private void View_DeleteEvent(object? sender, EventArgs e)
    {
        var current = _bindingSource.Current as Student;

        if (current is null)
        {
            MessageBox.Show("An error occured", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }


        _repository.Remove(current);
        _bindingSource.RemoveCurrent();
    }


    private void View_AddEvent(object? sender, EventArgs e)
    {
        var result = _addUpdateView.ShowDialog();

        if (result == DialogResult.Cancel)
            return;


        var newStudent = new Student(_addUpdateView.FirstName, _addUpdateView.LastName, _addUpdateView.BirthOfDate, (float)_addUpdateView.Score);

        _repository.Add(newStudent);
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


        _repository.Update(current);
        _bindingSource.ResetCurrentItem();
        MessageBox.Show("Successfully updated", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}
