using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit.SharedSetup
{
    [CollectionDefinition("SharedTestCollection")]
    public class SharedTestCollection : ICollectionFixture<TestStartup>
    {
        // خالیه — فقط برای اتصال
    }
}