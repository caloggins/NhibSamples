namespace Prabu.Library.Entities
{
    using NHibernate.Mapping.ByCode;
    using NHibernate.Mapping.ByCode.Conformist;

    public class CustomerAccount
    {
        public virtual long CustomerId { get; set; }
        public virtual long AccountId { get; set; }
        public virtual string Ownership { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Account Account { get; set; }

        public virtual void AddParent(Account account)
        {
            Account = account;
            AccountId = account.Id;
        }

        public virtual void AddParent(Customer customer)
        {
            Customer = customer;
            CustomerId = customer.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            var a = obj as CustomerAccount;
            if (a == null)
                return false;

            return a.CustomerId == CustomerId && a.AccountId == AccountId;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 21;

                hash = hash * 37 + CustomerId.GetHashCode();
                hash = hash * 37 + AccountId.GetHashCode();

                return hash;
            }
        }
    }

    public class CustomerAccountMap : ClassMapping<CustomerAccount>
    {
        public CustomerAccountMap()
        {
            Table("CustomerAccount");

            ComposedId(mapper =>
                       {
                           mapper.ManyToOne(o => o.Account, x =>
                                                             {
                                                                 x.ForeignKey("FK_Account_Id");
                                                                 x.Column("AccountId");
                                                                 x.Cascade(Cascade.All);
                                                             });
                           mapper.ManyToOne(o => o.Customer, x =>
                                                             {
                                                                 x.ForeignKey("FK_Customer_Id");
                                                                 x.Column("CustomerId");
                                                                 x.Cascade(Cascade.All);
                                                             });
                       });

            Property(account => account.Ownership);
        }
    }
}