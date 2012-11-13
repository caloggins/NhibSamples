namespace Prabu.Library.Entities
{
    using System.Collections.Generic;
    using NHibernate.Mapping.ByCode;
    using NHibernate.Mapping.ByCode.Conformist;

    public class Account
    {
        public virtual long Id { get; set; }
        public virtual string AccountNumber { get; set; }
        private IList<CustomerAccount> customerAccounts = new List<CustomerAccount>();
        public virtual IList<CustomerAccount> CustomerAccounts
        {
            get { return customerAccounts; }
            set { customerAccounts = value; }
        }

        public virtual void AddCustomerAccount(CustomerAccount customerAccount)
        {
            customerAccounts.Add(customerAccount);
            customerAccount.AddParent(this);
        }
    }

    public class AccountMap : ClassMapping<Account>
    {
        public AccountMap()
        {
            Table("Account");

            Id(account => account.Id, mapper => mapper.Generator(Generators.Native));

            Property(account => account.AccountNumber);

            Bag(customer => customer.CustomerAccounts,
                mapper =>
                {
                    mapper.Key(keyMapping =>
                               {
                                   keyMapping.ForeignKey("FK_Account_Id");
                                   keyMapping.Columns(columnMapper => columnMapper.Name("AccountId"));
                               });
                    mapper.Inverse(true);
                    mapper.Cascade(Cascade.All);
                }, relation => relation.OneToMany());
        }
    }
}