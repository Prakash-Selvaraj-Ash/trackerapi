﻿using BusTrackerApi.DbConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BusTrackerApi.Services.Entity
{
    public class EntityService : IEntityService
    {
        private readonly BusTrackContext _context;

        public EntityService(BusTrackContext context)
        {
            _context = context;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void SaveAsync(CancellationToken cancellationToken)
        {
            _context.SaveChangesAsync(cancellationToken);
        }
    }
}
