using System.Collections.Generic;
using System.Linq;

namespace SchoolMoney.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();
        public List<Bill> Bills { get; set; } = new List<Bill>();
        public Student(string name)
        {
            Name = name;
        }
   
        public int BillsTotal(TypeOfPayment type)
        {
            return Bills.FindAll(x => x.Type == type).Sum(x => x.Cost);
        }
        public int TransactionsTotal(TypeOfPayment type)
        {
            return Transactions.FindAll(x => x.Type == type).Sum(x => x.Cost);
        }
        public int Total(TypeOfPayment type)
        {
            return BillsTotal(type) - TransactionsTotal(type);
        }
        public List<Transaction> GetTranscations(TypeOfPayment type)
        {
            return Transactions.FindAll(x => x.Type == type);
        }
        public List<Bill> GetBills(TypeOfPayment type)
        {
            return Bills.FindAll(x => x.Type == type);
        }
    }
}
