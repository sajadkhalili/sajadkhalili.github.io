using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SSF.Infrastructure.Abstractions.AuditLogs;
using SSF.Infrastructure.Abstractions.CurrentUsers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSF.EFCore.AuditLogs
{
    //    builder.Services.AddZaminChageDatalogDalSql(c =>
    //{
    //    c.ConnectionString = cnnString;

    //});
    //builder.Services.AddZaminHamsterChageDatalog(c =>
    //{
    //    c.BusinessIdFieldName="Id";
    //});
    //builder.Services.AddZaminWebUserInfoService(c =>
    //{
    //    c.DefaultUserId="1";
    //}, useFake: true);
    //builder.Services.AddDbContext<HamsterTestContext>(c => c.UseSqlServer(cnnString).AddInterceptors(new ExternalAuditLogInterceptor()));
    //// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle



    public class ExternalAuditLogInterceptor : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            SaveEntityChangeLogs(eventData);
            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            SaveEntityChangeLogs(eventData);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private void SaveEntityChangeLogs(DbContextEventData eventData)
        {
            if (eventData.Context is null)
                throw new ArgumentNullException(nameof(eventData.Context));

            var userInfoService = eventData.Context.GetService<IUserInfoService>();
            var externalAuditLogRepository = eventData.Context.GetService<IExternalAuditLogRepository>();
            var options = eventData.Context.GetService<IOptions<ExternalAuditLogOptions>>().Value;

            var changedEntities = GetChangedEntities(eventData);
            var transactionId = Guid.NewGuid().ToString();
            var dateOfAccured = DateTime.Now;


            var entityChageInterceptorItems = new List<EntityAuditLog>();

            foreach (var entity in changedEntities)
            {
                var entityChageInterceptorItem = new EntityAuditLog
                {
                    Id = Guid.NewGuid(),
                    TransactionId = transactionId,
                    UserId = userInfoService.UserId().ToString(),
                    Ip = userInfoService.GetUserIp(),
                    EntityType = entity.Entity.GetType().FullName,
                    EntityId = entity.Property(options.BusinessIdFieldName).CurrentValue.ToString(),
                    DateOfOccurrence = dateOfAccured,
                    ChangeType = entity.State.ToString(),
                    ContextName = GetType().FullName
                };

                foreach (var property in entity.Properties.Where(c => options.propertyForReject.All(d => d != c.Metadata.Name)))
                {
                    if (entity.State == EntityState.Added || property.IsModified)
                    {

                        var v = property.CurrentValue?.ToString();
                        entityChageInterceptorItem.PropertyChangeLogItems.Add(new PropertyChangeLog
                        {
                            EntityAuditLogId = entityChageInterceptorItem.Id,
                            PropertyName = property.Metadata.Name,
                            Value = property.CurrentValue?.ToString(),
                        });
                    }
                }
                entityChageInterceptorItems.Add(entityChageInterceptorItem);
            }
            externalAuditLogRepository.Save(entityChageInterceptorItems);


        }

        private List<EntityEntry> GetChangedEntities(DbContextEventData eventData) =>
           eventData.Context.ChangeTracker.Entries()
               .Where(x => x.State == EntityState.Modified
               || x.State == EntityState.Added
               || x.State == EntityState.Deleted).ToList();
    }

}
