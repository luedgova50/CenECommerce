namespace CenECommerce.Classes
{

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CenECommerce.Models;

    public class ComboBoxHelpers : IDisposable
    {
        private static CenECommerceContext db = 
            new CenECommerceContext();

        public void Dispose()
        {
            db.Dispose();
        }

        public static List<State> GetStates()
        {
            var states = db.States.ToList();

            states.Add(new State
            {
                StateId = 0,
                NameState = "[---Select a State...---]"
            });

            return states =
                     states.
                     OrderBy(
                     d => d.NameState).
                     ToList();
        }

        public static List<City> GetCities()
        {
            var cities = db.Cities.ToList();

            cities.Add(new City
            {
                CityId = 0,
                NameCity = "[---Select a City...---]"
            });

            return cities =
                     cities.
                     OrderBy(
                     c => c.NameCity).
                     ToList();
        }

        public static List<Company> GetCompanies()
        {
            var companies = db.Companies.ToList();

            companies.Add(new Company
            {
                CompanyId = 0,
                NameCompany = "[---Select a Company...---]"
            });

            return companies =
                     companies.
                     OrderBy(
                     c => c.NameCompany).
                     ToList();
        }

        public static List<Category> GetCategories(int companyId)
        {
            var categories = 
                db.Categories.
                Where(ct => ct.CompanyId == companyId)
                .ToList();

            categories.Add(new Category
            {
                CategoryId = 0,
                Description = "[---Select a Category...---]"
            });

            return categories =
                     categories.
                     OrderBy(
                     ct => ct.Description).
                     ToList();
        }

        public static List<Tax> GetTaxes(int companyId)
        {
            var taxes =
        db.Taxes.
            Where(ct => ct.CompanyId == companyId).
            ToList();

            taxes.Add(new Tax
            {
                TaxId = 0,
                Description = "[---Select a Rate...---]"
            });

            return taxes =
                     taxes.
                     OrderBy(
                     tx => tx.Description).
                     ToList();
        }

        public static List<Customer> GetCustomers(int companyId)
        {
            var customers =
                db.Customers.
                    Where(ct => ct.CompanyId == companyId).
                    ToList();

            customers.Add(new Customer
            {
                CustomerID = 0,
                FirstName = "[---Select a Customer...---]"
            });

            return customers.
                     OrderBy(
                     tx => tx.FirstName).
                     ThenBy(
                         tx => tx.LastName).
                     ToList();
        }

        public static List<Product> GetProducts(int companyId)
        {
            var products =
                db.Products.
                    Where(ct => ct.CompanyId == companyId).
                    ToList();

            products.Add(new Product
            {
                ProductID = 0,
                Description = "[---Select a Product...---]"
            });

            return products.
                     OrderBy(
                     tx => tx.Description).
                     ToList();
        }

    }
}