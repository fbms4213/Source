namespace WinFormsAppMVP.Views;

public interface IAddView
{
    string FirstName { get; }
    string LastName { get; }
    DateOnly BirthOfDate { get; }
    float Score { get; }

    event EventHandler SaveEvent;
    event EventHandler CancelEvent;
}
