﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YambApp.Model;

namespace YambApp.FileHandler
{
    public interface IPlayerFile
    {
        List<User> Read();

        void Save(List<User> players);
    }
}
