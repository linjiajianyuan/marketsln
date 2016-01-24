using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceMdl
{
    public class Mdl
    {
        public static DataTable GetOrderView(int shipped,string startDate, string endDate)
        {
            DataTable dt = new DataTable();
            dt=MarketplaceDb.Db.GetOrderInfoDt(shipped,startDate,endDate);
            return dt;
        }
    }
}
