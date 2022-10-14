namespace WinFormsAppMVP.Views;

public interface IAddUpdateView
{
    string FirstName { get; set; }
    string LastName { get; set; }
    DateTime BirthOfDate { get; set; }
    decimal Score { get; set; }

    DialogResult DialogResult { get; set; }
    DialogResult ShowDialog();


    event EventHandler SaveEvent;
    event EventHandler CancelEvent;
}
