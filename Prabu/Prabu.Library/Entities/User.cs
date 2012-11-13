namespace Prabu.Library.Entities
{
    using NHibernate.Mapping.ByCode;
    using NHibernate.Mapping.ByCode.Conformist;

    public class User
    {
        public virtual long Id { get; set; }
        public virtual string Name { get; set; }
        public virtual UserDetail Detail { get; set; }

        public virtual void AddDetail(UserDetail userDetail)
        {
            Detail = userDetail;
            Detail.AddUser(this);
        }
    }

    public class UserMap : ClassMapping<User>
    {
        public UserMap()
        {
            Table("Users");

            Id(user => user.Id, mapper => mapper.Generator(Generators.Identity));

            Property(user => user.Name);

            OneToOne(user => user.Detail,
                     mapper =>
                     {
                         mapper.ForeignKey("FK_User_Id");
                         mapper.Cascade(Cascade.All);
                     });
        }
    }
}