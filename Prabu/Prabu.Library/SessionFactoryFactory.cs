namespace Prabu.Library
{
    using System.Data;
    using NHibernate;
    using NHibernate.Cfg;
    using NHibernate.Dialect;
    using NHibernate.Driver;
    using NHibernate.Mapping.ByCode;

    public class SessionFactoryFactory
    {
        private static readonly object ThreadLock = new object();
        private static ISessionFactory sessionFactory;

        public static ISessionFactory CreateSessionFactory(string connectionString)
        {

            lock (ThreadLock)
            {
                if (sessionFactory == null)
                {
                    var config = new Configuration();

                    config.DataBaseIntegration(
                        db =>
                        {
                            db.Dialect<MsSql2008Dialect>();
                            db.Driver<Sql2008ClientDriver>();
                            db.ConnectionString = connectionString;
                            db.IsolationLevel = IsolationLevel.ReadCommitted;
                            db.LogSqlInConsole = true;
                            db.LogFormattedSql = true;
                            db.AutoCommentSql = true;
                        }
                        );

                    var mapper = new ModelMapper();
                    mapper.WithMappings(config);

                    sessionFactory = config.BuildSessionFactory();
                }
                
                return sessionFactory;
            }

        }
    }
}