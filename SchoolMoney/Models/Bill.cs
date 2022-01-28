using System;

namespace SchoolMoney.Models
{
    public class Bill
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Cost { get; set; }
        public DateTime Date { get; set; }
        public TypeOfPayment Type { get; set; }
        public Bill(string title, int cost, TypeOfPayment type)
        {
            Title = title;
            Cost = cost;
            Type = type;
            Date = DateTime.Now;
        }
    }
}
