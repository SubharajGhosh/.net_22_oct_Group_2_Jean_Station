using JeanStation.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JeanStation.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        JeanStationContext _context;
        public CustomerRepository()
        {
            _context = new JeanStationContext();
        }
        public Customer GetCustomerByCustomerId(string customerId)
        {
            return _context.Customers
                .Include("UserNavigation") // Include UserNavigation for User details
                .FirstOrDefault(c => c.CustomerId == customerId);
        }

        // Update an existing customer
        // Update an existing customer
        public Customer UpdateCustomer(Customer customer)
        {
            var existingCustomer = _context.Customers.FirstOrDefault(c => c.CustomerId == customer.CustomerId);
            if (existingCustomer == null)
            {
                throw new InvalidOperationException("Customer not found.");
            }

            // Update properties
            existingCustomer.CustomerName = customer.CustomerName;
            existingCustomer.Email = customer.Email;
            existingCustomer.PhoneNumber = customer.PhoneNumber;
            existingCustomer.Address = customer.Address;

            _context.SaveChanges(); // Commit changes
            return existingCustomer;
        }


        // Delete a customer
        public bool DeleteCustomer(string customerId)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.CustomerId == customerId);
            if (customer == null)
            {
                return false;
            }

            _context.Customers.Remove(customer);
            _context.SaveChanges(); // Commit the deletion to the database
            return true;
        }

        // Browse jeans available in the store
        //public IEnumerable<Jeans> BrowseJeans()
        //{
        //    return _context.Jeans.ToList();
        //}


    }
}