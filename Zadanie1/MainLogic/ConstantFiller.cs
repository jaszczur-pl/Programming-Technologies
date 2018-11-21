using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Zadanie1.Data;

namespace Zadanie1.MainLogic
{
    public class ConstantFiller : DataFiller
    {
        public override void Fill(DataContext context)
        {
            List<Customer> customers = context.customers;
            Dictionary<int, CD> cds = context.cds;
            ObservableCollection<Event> events = context.events;
            List<CDState> cdStates = context.cdStates;


            Customer c1 = new Customer()
            {
                name = "Kowalski",
                surname = "Jan",
                emailAddress = "kowaskij@gmail.com",
                age = 30
            };
            Customer c2 = new Customer()
            {
                name = "Nowak",
                surname = "Katarzyna",
                emailAddress = "nowakk@o2.pl",
                age = 35
            };
            Customer c3 = new Customer()
            {
                name = "Miłoch",
                surname = "Krzysztof",
                emailAddress = "milochk@onet.pl",
                age = 24
            };



            CD cd1 = new CD() {
                id=1526,
                title = "Let It Be",
                group = "The Beatles",
            };
            CD cd2 = new CD() {
                id=2548,
                title = "Bed",
                group = "Michael Jackson",
            };
            CD cd3 = new CD()
            {
                id = 3789,
                title = "Undercover",
                group = "The Rolling Stones",
            };


            CDState cdState1 = new CDState() {
                cd = cd1,
                dateOfPurchase = new DateTime(2014,10,21)
            };
            CDState cdState2 = new CDState()
            {
                cd = cd2,
                dateOfPurchase = new DateTime(2015, 06, 13)
            };
            CDState cdState3 = new CDState()
            {
                cd = cd3,
                dateOfPurchase = new DateTime(2018, 02, 11)
            };


            Event e1 = new Event() {
                cdState = cdState1,
                customer = c1
            };
            Event e2 = new Event() {
                cdState= cdState2,
                customer =c2
            };
            Event e3 = new Event()
            {
                cdState = cdState3,
                customer = c3
            };

            customers.Add(c1);
            customers.Add(c2);
            customers.Add(c3);
            cds.Add(cd1.id, cd1);
            cds.Add(cd2.id, cd2);
            cds.Add(cd3.id, cd3);
            cdStates.Add(cdState1);
            cdStates.Add(cdState2);
            cdStates.Add(cdState3);
            events.Add(e1);
            events.Add(e2);
            events.Add(e3);
        }
    }
}
