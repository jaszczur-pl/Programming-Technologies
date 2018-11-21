using System;
using System.Collections.Generic;
using System.Text;

namespace Zadanie1.MainLogic
{
    class DataService
    {
        private DataRepository repository;

        public DataService(DataRepository repository) {
            this.repository = repository;
        }

    }
}
