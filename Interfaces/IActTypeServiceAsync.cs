﻿using Advocate.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Advocate.Interfaces
{
    public interface IActTypeServiceAsync : IGenericServiceAsync<ActTypeEntity>
    {
        public bool isActExist(string actName);
    }
}
