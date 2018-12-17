using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Zadanie1.Data;
using Zadanie1.MainLogic;
using System.IO;
using System.Linq;

namespace UnitTests
{
    public class FillingFromFile: DataFiller
    {
        public override void Fill(DataContext context) {

            List<Customer> customers = context.customers;
            Dictionary<int, CD> cds = context.cds;
            ObservableCollection<Event> events = context.events;
            List<CDState> cdStates = context.cdStates;

            try {
                List<string[]> dataList = File.ReadLines("data.txt").Select(line => line.Split(';')).ToList();

                foreach (string[] line in dataList) {
                    Customer cust = new Customer { name = line[0], surname = line[1], emailAddress =line[2], age=Int16.Parse(line[3]) };
                    CD cd = new CD { id = Int16.Parse(line[4]), title = line[5], group = line[6] };
                    CDState cdState = new CDState { cd = cd, dateOfPurchase = new DateTimeOffset(new DateTime(Int32.Parse(line[7]), Int32.Parse(line[8]), Int32.Parse(line[9]))) };
                    Event evt = new Event { cdState = cdState, customer = cust };

                    customers.Add(cust);
                    cds.Add(cd.id, cd);
                    cdStates.Add(cdState);
                    events.Add(evt);
                }
                Console.Write(customers.Count);
            }
            catch (FileNotFoundException exception) {
                Console.Write(exception.Message);
            }
            catch (IOException exception) {
                Console.Write(exception.StackTrace);
            }


        }

    }
        
}
