namespace Prabu.Library.Entities
{
    using System;
    using System.Collections.Generic;
    using NHibernate.Mapping.ByCode;
    using NHibernate.Mapping.ByCode.Conformist;

    public class Customer
    {
        public virtual long Id { get; set; }
        public virtual string Name { get; set; }
        public virtual DateTime DateOfBirth { get; set; }
        private IList<CustomerAccount> customerAccounts = new List<CustomerAccount>();
        public virtual IList<CustomerAccount> CustomerAccounts
        {
            set { customerAccounts = value; }
            get { return customerAccounts; }
        }

        public virtual void AddCustomerAccount(CustomerAccount customerAccount)
        {
            customerAccounts.Add(customerAccount);
            customerAccount.AddParent(this);
        }
    }

    public class CustomerMapping : ClassMapping<Customer>
    {
        public CustomerMapping()
        {
            Table("Customer");

            Id(customer => customer.Id, mapper => mapper.Generator(Generators.Native));

            Property(customer => customer.Name);
            Property(customer => customer.DateOfBirth);

            Bag(customer => customer.CustomerAccounts,
                mapper =>
                {
                    mapper.Key(keyMapping =>
                               {
                                   keyMapping.ForeignKey("FK_Customer_Id");
                                   keyMapping.Columns(columnMapper => columnMapper.Name("CustomerId"));
                               });
                    mapper.Inverse(true);
                    mapper.Cascade(Cascade.All);
                }, relation => relation.OneToMany());
        }
    }
}