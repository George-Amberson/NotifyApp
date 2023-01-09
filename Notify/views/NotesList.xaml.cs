namespace Notify.views;
using Notify.ViewModel;
public partial class NotesList : ContentPage
{
	public NotesList()
	{
		InitializeComponent();
	}

  

    private void onSearch(object sender, TextChangedEventArgs e)
    {
		((NotesViewModel)this.BindingContext).noteFilter(e.NewTextValue);
    }
}