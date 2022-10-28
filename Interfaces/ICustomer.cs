using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mpilo1.Models;

namespace Mpilo1.Interfaces
{
    interface ICustomer
    {
        public List<Customer> ReadAll();
        public void Update(Customer customer);

        public Customer ReadById(string id);
        public void  Create(Customer customer);
        public void Delete(string id);
    }
}
