﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promise
{
    public interface IAwaitable
    {
        IAwaiter GetAwaiter();
    }
}
