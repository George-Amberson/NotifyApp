
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Notify.Models;
using System.Collections.ObjectModel;
using Google.Protobuf.WellKnownTypes;
using System.Windows.Input;
using System.Runtime.ConstrainedExecution;
using NotificationBackService;
namespace Notify.ViewModel
{
    public class NotesViewModel : INotifyPropertyChanged
    {
        IList<localNote> source;
        NotifyService.NotifyServiceClient notifyClient;
        BaseService backgroundService;
        public ObservableCollection<localNote> Notes { get; private set; }

        public ICommand DeleteCommand => new Command<localNote>(removeNote);
        public NotesViewModel( NotifyService.NotifyServiceClient _notifyClient, BaseService _backService)
        {
            notifyClient = _notifyClient;
            backgroundService = _backService;
            source = new List<localNote>();
            new Action(async () => await getAllNotes())();
        }
        String generateGRPCStringDate(DateTime _date, TimeSpan _time)
        {
            return _date.Add(_time).ToString("s");
        }
        async Task getAllNotes()
        {
            try
            {
                identificationResponse response = await notifyClient.getALlNotesAsync(new identificationRequest { Id = "GeorgeAmberson" });
 
                if (response != null)
                {
                    foreach (Note i in response.Notes)
                    {
                        source.Add(new localNote(i));
                    }
                    Notes = new ObservableCollection<localNote>(source);
                    OnPropertyChanged("filterText");
                    OnPropertyChanged("Notes");
                    backgroundService.Start(Notes[0]);
                }
            }
            catch(Exception ex){}
            
        }
        async void removeNote(localNote note)
        {
            if (Notes.Contains(note))
            {
                Notes.Remove(note);
                source.Remove(note);
            }
            Int32Value _id = new Int32Value();
            _id.Value= note.Id;
            try
            {
                await notifyClient.deleteNoteAsync(_id);
                Note topNote = await notifyClient.getTopNoteAsync(new Empty());
                if (topNote.TextInfo != "Sorry no new Notes")
                {
                    backgroundService.Start(new localNote(topNote));
                }
            }catch(Exception ex){}
        }
        public async void addNote(String _textInfo, DateTime _date, TimeSpan _time)
        {
            var rand = new Random();
            localNote note = new localNote(_textInfo, _date, _time);
            Notes.Add(note);
            source.Add(note);
            String date = generateGRPCStringDate(_date, _time);
            try 
            {
              BoolValue res = await notifyClient.addNewNoteAsync(new Note { TextInfo = _textInfo, DateAndTime = date, Id = rand.Next() });
              Note topNote = await notifyClient.getTopNoteAsync(new Empty());
              backgroundService.Start(new localNote(topNote));
            }
            catch(Exception ex){}
        }
        public void noteFilter(String filterText)
        {
            
            foreach( var i in source)
            {
                if(!i.textInfo.StartsWith(filterText))
                {
                    Notes.Remove(i);
                }
                else
                {
                    if (!Notes.Contains(i)) Notes.Add(i);
                }
            }
        }
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
