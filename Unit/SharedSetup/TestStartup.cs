using Application.Common.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit.SharedSetup
{
    public class TestStartup
    {
        private static bool _configured = false;

        public TestStartup()
        {
            if (_configured) return;

            MapsterConfig.RegisterMappings();
            _configured = true;
        }
    }
}