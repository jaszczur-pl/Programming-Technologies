using System;
using System.Collections.Generic;
using System.Text;
using Zadanie1.Data;
using System.Linq;

namespace Zadanie1.MainLogic
{
    public class DataService
    {
        private DataRepository repository;

        public DataService(DataRepository repository) {
            this.repository = repository;
        }

        public string showAllCustomers(IEnumerable<Customer> customers) {

            string data = "";

            foreach (Customer customer in customers) {
                data += customer.name + " " + customer.surname + " " + customer.age + " " + customer.emailAddress + "\n";
            }

            return data;
        }

        public string showAllCDs(IEnumerable<CD> cds) {

            string data="";

            foreach (CD position in cds) {
                data += position.id + " " + position.group + " " + position.title + "\n";
            }

            return data;
        }

        public string showAllEvents(IEnumerable<Event> events) {
            string data = "";

            foreach (Event evt in events) {
                data += evt.customer.name + " " + evt.customer.surname + " " + evt.cdState.cd.id + " " + evt.cdState.dateOfPurchase + "\n";
            }

            return data;
        }

        public string showAllCDStates(IEnumerable<CDState> cdStates) {
            string data = "";

            foreach (CDState cdState in cdStates) {
                data += cdState.cd.id + " " + cdState.dateOfPurchase + "\n";
            }

            return data;
        }

        public string showAllInformations(IEnumerable<Event> events) {

            string data = "";

            foreach (Event evt in events) {
                data += evt.customer.name + " " + evt.customer.surname + " " + evt.customer.age + " " + evt.customer.emailAddress + " " +
                       evt.cdState.dateOfPurchase + " " + evt.cdState.cd.id + " " + evt.cdState.cd.group + " - " + evt.cdState.cd.title + "\n" +
                       "#########################################################" + "\n";
            }

            return data;
        }

        public void AddEvent(Customer customer, CDState cdState) {
            bool isEventExist = repository.GetAllEvents().Any(e => e.cdState.Equals(cdState) && e.customer.Equals(customer));

            if (!isEventExist) {
                Event evt = new Event() { customer = customer, cdState = cdState };
                repository.AddEvent(evt);
            }
        }

        public void AddCustomer(Customer customer) {
            IEnumerable<Customer> currentCustomers = repository.GetAllCustomers();

            if (!currentCustomers.Contains(customer)) {
                repository.AddCustomer(customer);
            }
        }

        public void AddCD(CD cd) {
            bool isCDExist = repository.GetAllCDs().Where(x => x.id == cd.id).Any();

            if (!isCDExist) {
                repository.AddCD(cd);
            }
        }

        public void AddCDState(CD cd) {
            bool isCDExist = repository.GetAllCDs().Where(x => x.id == cd.id).Any();
            DateTimeOffset currentDate = DateTimeOffset.Now;

            if (isCDExist) {
                repository.AddCDState(new CDState() { cd = cd, dateOfPurchase = currentDate});
            }
        }

        public void UpdateCustomer(int index, Customer customer) {
            int numberOfElements = repository.GetAllCustomers().Count();

            if (numberOfElements > index) {
                repository.UpdateCustomer(index, customer);
            }  
        }

        public void UpdateCD(int id, CD cd) {
            bool isCDExist = repository.GetAllCDs().Where(x => x.id == id).Any();

            if (isCDExist) {
                repository.UpdateCD(id, cd);
            }
        }

        public void UpdateEvent(int index, Event evt) {
            int numberOfElements = repository.GetAllEvents().Count();

            if (numberOfElements > index) {
                repository.UpdateEvent(index, evt);
            }
        }

        public void UpdateCDState(int index, CDState cdState) {
            int numberOfElements = repository.GetAllCDStates().Count();

            if (numberOfElements > index) {
                repository.UpdateCDState(index, cdState);
            }
        }

        public void DeleteCustomer(int index) {
            int numberOfElements = repository.GetAllCustomers().Count();

            if (numberOfElements > index) {
                repository.DeleteCustomer(index);
            }
        }

        public void DeleteCD(int id) {
            bool isCDExist = repository.GetAllCDs().Where(x => x.id == id).Any();

            if (isCDExist) {
                repository.DeleteCD(id);
            }
        }

        public void DeleteEvent(int index) {
            int numberOfElements = repository.GetAllEvents().Count();

            if (numberOfElements > index) {
                repository.DeleteEvent(index);
            }
        }

        public void DeleteCDState(int index) {
            int numberOfElements = repository.GetAllCDStates().Count();

            if (numberOfElements > index) {
                repository.DeleteCDState(index);
            }
        }

        public List<Event> GetEventByDate(DateTimeOffset startDate, DateTimeOffset endDate) {
            return repository.GetAllEvents().Where(e => e.cdState.dateOfPurchase >= startDate && e.cdState.dateOfPurchase <= endDate).ToList();
        }

        public List<Customer> GetCustomersBySurname(string surname) {
            return repository.GetAllCustomers().Where(c => c.surname == surname).ToList();
        }

        public Dictionary<int, CD> GetCDByID(int id) {
            return repository.GetAllCDs().Where(d => d.id == id).ToDictionary(d => d.id);
        }


    }
}
