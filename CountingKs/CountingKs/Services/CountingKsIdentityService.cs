using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountingKs.Services
{
    public class CountingKsIdentityService : ICountingKsIdentityService
    {
        public string CurrentUser
        {
            get
            {
                return "shawnwildermuth";
            }
        }
    }
}
