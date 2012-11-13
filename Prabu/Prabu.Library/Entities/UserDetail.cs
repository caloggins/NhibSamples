namespace Prabu.Library.Entities
{
    using NHibernate.Mapping.ByCode.Conformist;

    public class UserDetail
    {
        public virtual long UserId { get; set; }
        public virtual string DriversLicense { get; set; }
        public virtual bool IsDonor { get; set; }
        public virtual User ParentUser { get; set; }

        public virtual void AddUser(User user)
        {
            ParentUser = user;
            UserId = user.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            var a = obj as UserDetail;
            if (a == null)
                return false;

            return a.UserId == UserId;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 21;

                hash = hash*37 + UserId.GetHashCode();
                
                return hash;
            }
        }
    }

    public class UserDetailMap : ClassMapping<UserDetail>
    {
        public UserDetailMap()
        {
            Table("UserDetail");

            ComposedId(mapper => mapper.ManyToOne(o => o.ParentUser, x =>
                                                                     {
                                                                         x.Column("UserId");
                                                                         x.ForeignKey("FK_User_Id");
                                                                     }));

            Property(detail => detail.DriversLicense);
            Property(detail => detail.IsDonor);
        }
    }
}