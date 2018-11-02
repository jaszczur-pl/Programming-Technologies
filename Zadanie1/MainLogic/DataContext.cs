using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Zadanie1.Data;

namespace Zadanie1.MainLogic
{
    class DataContext
    {
        public List<Customer> customers;
        public Dictionary<int, CD> cds;
        public ObservableCollection<Event> events;
        public List<CDState> cdStates;
    }
}
