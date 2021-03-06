﻿using AutoUpdateLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoUpdateServer
{
    internal class OausService : MarshalByRefObject, IOausService
    {
        private UpdateConfiguration fileConfig;
        public OausService(UpdateConfiguration _fileConfig)
        {
            this.fileConfig = _fileConfig;
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }

        public int GetLatestVersion()
        {
            return this.fileConfig.ClientVersion;
        }
    }
}
