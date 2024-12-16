using JeanStation.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeanStation.Repository
{
    internal interface ICustomerRepository
    {
        Customer GetCustomerByCustomerId(string customerId);


        // Update an existing customer
        Customer UpdateCustomer(Customer customer);


        // Delete a customer
        bool DeleteCustomer(string customerId);


        // Browse jeans available in the store
        //IEnumerable<Jeans> BrowseJeans();

        //IEnumerable<OrderItem> ViewCart(string customerId);
        

    }
}
