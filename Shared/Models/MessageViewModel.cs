using System;

namespace TodoApi.Shared.Models
{
    public class MessageViewModel
    {
        public string message { get; set; }
        public DateTime currentDate { get; set; }
        public MessageViewModel(string message, DateTime currentDate)
        {
            this.message = message;
            this.currentDate = currentDate;
        }
        public MessageViewModel(){}
    }
}