using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FusionCacheTools.BackOffice.Models
{
    public class FusionCachedObject
    {
        public string Key { get; set; }
        public DateTime Expiration { get; set; }
    }
}
