using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notify.Models
{
    public class localNote
    {
        public localNote(String _textInfo, DateTime _date, TimeSpan _time)
        {

            textInfo = _textInfo;
            dateAndTime = _date.ToString("d") + " " + _time.ToString();
        }
        public localNote(Note _note)
        {
            textInfo = _note.TextInfo;
            dateAndTime = DateTime.Parse(_note.DateAndTime).ToString("d")+" "+ DateTime.Parse(_note.DateAndTime).TimeOfDay.ToString();
            Id = _note.Id;

        }
        public int Id { get; set; }
        public string textInfo { get; set; }
        public string dateAndTime { get; set; }
    }
}
