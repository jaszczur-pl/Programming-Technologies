using System;
using System.Collections.Generic;
using System.Text;
using Zadanie1.Data;

namespace Zadanie1.MainLogic
{
    public class DataRepository
    {
        private DataContext data;
        private DataFiller filler;

        // Constructor injection
        public DataRepository(DataFiller filler, DataContext data) {
            this.filler = filler;
            this.data = data;

            filler.Fill(data);
        }

        //Add object to collection
        public void AddCustomer(Customer customer) {
            data.customers.Add(customer);
        }

        public void AddCD(CD cd) {
            if (!data.cds.ContainsKey(cd.id)) {
                data.cds.Add(cd.id, cd);
            }
        }

        public void AddEvent(Event evt) {
            data.events.Add(evt);
        }

        public void AddCDState(CDState cdState) {
            data.cdStates.Add(cdState);
        }

        //Get single object from collection
        public Customer GetCustomer(int index) {
            if (data.customers.Count > index) {
                return data.customers[index];
            }
            else {
                return null;
            } 
        }

        public CD GetCD(int key) {
            if (data.cds.ContainsKey(key)) {
                return data.cds[key];
            }
            else {
                return null;
            }
        }

        public Event GetEvent(int index) {
            if (data.events.Count > index) {
                return data.events[index];
            }
            else {
                return null;
            }
        }

        public CDState GetCDStates(int index) {
            if (data.cdStates.Count > index) {
                return data.cdStates[index];
            }
            else {
                return null;
            }
        }

        //Get all objects from collection
        public IEnumerable<Customer> GetAllCustomers() {
            return data.customers;
        }

        public IEnumerable<CD> GetAllCDs() {
            return data.cds.Values;
        }

        public IEnumerable<Event> GetAllEvents() {
            return data.events;
        }

        public IEnumerable<CDState> GetAllCDStates() {
            return data.cdStates;
        }

        //Update object in collection for the received position
        public void UpdateCustomer(int index, Customer newCustomer) {
            if (data.customers.Count > index) {
                Customer customer = data.customers[index];

                customer.age = newCustomer.age;
                customer.emailAddress = newCustomer.emailAddress;
                customer.name = newCustomer.name;
                customer.surname = newCustomer.surname;
            }
        }

        public void UpdateCD(int id, CD newCD) {
            if (data.cds.ContainsKey(id)) {
                data.cds[id].group = newCD.group;
                data.cds[id].title = newCD.title;
            }
        }

        public void UpdateEvent(int index, Event newEvent){
            if (data.events.Count > index) {
                Event evt = data.events[index];

                evt.cdState = newEvent.cdState;
                evt.customer = newEvent.customer;
            }
        }

        public void UpdateCDState(int index, CDState newCDState) {
            if (data.cdStates.Count > index) {
                CDState cdState = data.cdStates[index];

                cdState.cd = newCDState.cd;
                cdState.dateOfPurchase = newCDState.dateOfPurchase;
            }
        }

        //Remove object from collection for the received position
        public void DeleteCustomer(int index) {
            if (data.customers.Count > index)
            {
                data.customers.RemoveAt(index);
            }
        }

        public void DeleteCD(int id, CD newCD)
        {
            if (data.cds.ContainsKey(id))
            {
                data.cds.Remove(id);
            }
        }

        public void DeleteEvent(int index, Event newEvent)
        {
            if (data.events.Count > index)
            {
                data.customers.RemoveAt(index);
            }
        }

        public void DeleteCDState(int index, CDState newCDState)
        {
            if (data.cdStates.Count > index)
            {
                data.cdStates.RemoveAt(index);
            }
        }

    }

}
