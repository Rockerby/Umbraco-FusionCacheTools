using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.Migrations;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core;
using Umbraco.Cms.Infrastructure.Migrations.Upgrade;
using Umbraco.Cms.Infrastructure.Scoping;
using Umbraco.Cms.Infrastructure.Migrations;
using NPoco.DatabaseTypes;
using Umbraco.Cms.Core.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FusionCacheTools.BackOffice.Migrations
{
    public class SqlCachingComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            builder.AddComponent<SqlCachingComponent>();
        }
    }

    public class SqlCachingComponent : IComponent
    {
        private readonly IScopeProvider _scopeProvider;
        private readonly IMigrationPlanExecutor _migrationPlanExecutor;
        private readonly IKeyValueService _keyValueService;
        private readonly IRuntimeState _runtimeState;
        private readonly ILogger<SqlCachingComponent> _logger;

        public SqlCachingComponent(IScopeProvider scopeProvider, IMigrationPlanExecutor migrationPlanExecutor,
            IKeyValueService keyValueService, ILogger<SqlCachingComponent> logger, IRuntimeState runtimeState)
        {
            _scopeProvider = scopeProvider;
            _migrationPlanExecutor = migrationPlanExecutor;
            _keyValueService = keyValueService;
            _logger = logger;
            _runtimeState = runtimeState;
        }

        public void Initialize()
        {
            if (_runtimeState.Level < RuntimeLevel.Run)
            {
                return;
            }

            Upgrader upgrader = new Upgrader(new SqlCacheMigration());
            upgrader.Execute(_migrationPlanExecutor, _scopeProvider, _keyValueService);
        }

        public void Terminate()
        {
        }
    }

    public class SqlCacheMigration : MigrationPlan
    {
        public SqlCacheMigration() : base("SqlCacheMigration")
        {
            From(String.Empty).To<SqlCacheMigrationCreateTables>("sql-caching");
        }
    }

    public class SqlCacheMigrationCreateTables : MigrationBase
    {
        private readonly IMigrationContext _context;

        public SqlCacheMigrationCreateTables(IMigrationContext context)
            : base(context) => _context = context;

        protected override void Migrate()
        {
            if (!TableExists("CustomCache"))
            {
                // Custom cache doesn't work with SQL Lite
                if (_context.Database.DatabaseType is SQLiteDatabaseType)
                {
                    return;
                }

                Execute.Sql(@"SET ANSI_NULLS ON
                          GO

                            SET QUOTED_IDENTIFIER ON
                            GO

                            CREATE TABLE [dbo].[CustomCache](
	                            [Id] [nvarchar](449) NOT NULL,
	                            [Value] [varbinary](max) NOT NULL,
	                            [ExpiresAtTime] [datetimeoffset](7) NOT NULL,
	                            [SlidingExpirationInSeconds] [bigint] NULL,
	                            [AbsoluteExpiration] [datetimeoffset](7) NULL,
                            PRIMARY KEY CLUSTERED 
                            (
	                            [Id] ASC
                            )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, 
	                            IGNORE_DUP_KEY = OFF, 
	                            ALLOW_ROW_LOCKS = ON, 
	                            ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
                            ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
                            GO").Do();

            }

        }
    }

}
