﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedWood.Interface.SessionLogger
{
    public interface ISessionLogger
    {
        void LogObject(ISessionDto dto);

    }
}
