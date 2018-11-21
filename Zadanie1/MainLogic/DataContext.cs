using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Zadanie1.Data;

namespace Zadanie1.MainLogic
{
    public partial class DataContext
    {
        public List<Customer> customers;
        public Dictionary<int, CD> cds;
        public ObservableCollection<Event> events;
        public List<CDState> cdStates;

        public DataContext() {
            customers = new List<Customer>();
            cds = new Dictionary<int, CD>();
            events = new ObservableCollection<Event>();
            cdStates = new List<CDState>();
        }
    }
}
