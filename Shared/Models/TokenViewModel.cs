using System;

namespace TodoApi.Shared.Models
{
    public class TokenViewModel
    {
        public string token { get; set; }
        public string message { get; set; }
        public DateTime currentDate { get; set; }
        public TokenViewModel(string token, string message, DateTime currentDate)
        {
            this.token = token;
            this.message = message;
            this.currentDate = currentDate;
        }
        public TokenViewModel(){}
    }
}