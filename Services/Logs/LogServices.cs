using DataAccess.Interfaces;
using Entities;
using Services.Helpers;
using Services.Region;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Logs
{
    public class LogService
    {
        private static IRepository<Log> repository;
        public LogService(IRepository<Log> repository)
        { LogService.repository = repository; }

        public static void Add(string action)
        {
            var log = new Log() { Action = action, CreatedAt = RegionServices.CurrentDateTime() };

            repository.Insert(log);

        }

        public static void Add(string action, Guid userId)
        {
            var Id = SharedServices.ConvertGuid(userId);

            var log = new Log() { Action = string.Format("{0} For {1}", action, Id), CreatedAt = RegionServices.CurrentDateTime() };

            repository.Insert(log);

        }


    }
}
