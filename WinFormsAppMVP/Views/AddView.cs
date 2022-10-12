namespace WinFormsAppMVP.Views;

public partial class AddView : Form, IAddView
{
    public AddView()
    {
        InitializeComponent();
    }

    public string FirstName { get => txt_firstName.Text; }
    public string LastName { get => txt_lastName.Text; }
    public DateOnly BirthOfDate { get => DateOnly.Parse(monthCalendar1.SelectionStart.ToShortDateString()); }
    public float Score { get => (float)num_score.Value; }


    public event EventHandler? SaveEvent;  
    public event EventHandler? CancelEvent;


    // private void btn_save_Click(object sender, EventArgs e) 
    //     => DialogResult = DialogResult.OK;
    // 
    // private void btn_cancel_Click(object sender, EventArgs e)
    //     => DialogResult = DialogResult.Cancel;


    private void btn_save_Click(object sender, EventArgs e)
       => SaveEvent?.Invoke(sender, e);

    private void btn_cancel_Click(object sender, EventArgs e)
       => CancelEvent?.Invoke(sender, e);
}