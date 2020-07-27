using OrchestradeCommon.Contracts;
using OrchestradeCommon.RefData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itecx.Util
{
    class Util
    {
        public static string GetPartyName(long entityId)
        {
            Party party = Env.Current.StaticData.GetPartyById(entityId);
            return party.Name;
        }

        public static IList<ProductCode> GetAllProductCodes()
        {
            return Env.Current.StaticData.GetAllProductCodes();
        }

    }
}
