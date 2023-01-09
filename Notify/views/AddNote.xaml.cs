using Notify.ViewModel;

namespace Notify.views;

public partial class AddNote : ContentPage
{
    String textInfo;
    DateTime date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 1);
    TimeSpan time = new TimeSpan(8, 0, 0);
	public AddNote()
	{
		InitializeComponent();
        datePicker.Date = date;
        timePicker.Time = time;
	}
    

    private void addNewNote(object sender, EventArgs e)
    {
        ((NotesViewModel)this.BindingContext).addNote(textInfo, date, time);
        TextInfo.Text = "";
    }
    
    

    private void onDateSelected(object sender, DateChangedEventArgs e)
    {
        date = e.NewDate;
    }

    private void onTimePicker(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if(e.PropertyName == "Time")
        {
           time = timePicker.Time;
        }
    }

    private void ontextChanged(object sender, TextChangedEventArgs e)
    {
        textInfo = e.NewTextValue;
    }
}
