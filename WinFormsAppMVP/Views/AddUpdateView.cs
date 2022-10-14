namespace WinFormsAppMVP.Views;

public partial class AddUpdateView : Form, IAddUpdateView
{
    public AddUpdateView()
    {
        InitializeComponent();
    }

    public string FirstName { get => txt_firstName.Text; set => txt_firstName.Text = value; }
    public string LastName { get => txt_lastName.Text; set => txt_lastName.Text = value; }
    public DateTime BirthOfDate { get => monthCalendar1.SelectionStart; set => monthCalendar1.SelectionStart = value; }
    public decimal Score { get => num_score.Value; set => num_score.Value = value; }


    public event EventHandler? SaveEvent;  
    public event EventHandler? CancelEvent;


    private void btn_save_Click(object sender, EventArgs e)
       => SaveEvent?.Invoke(sender, e);

    private void btn_cancel_Click(object sender, EventArgs e)
       => CancelEvent?.Invoke(sender, e);
}