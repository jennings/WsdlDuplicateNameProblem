using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WsdlDuplicateName.SalesItemService;

namespace WsdlDuplicateName
{
    class Program
    {
        static void Main(string[] args)
        {
            SalesServiceClient client = new SalesServiceClient();
            var date = ToSimpleDate(new DateTime());

            // throws InvalidOperationException
            // Message == "There was an error reflecting 'start'."
            client.setSalesItemsV3(1, 1, null, date, date);
        }

        static hsSimpleDate ToSimpleDate(DateTime time)
        {
            return new hsSimpleDate
            {
                year = time.Year,
                month = time.Month,
                day = time.Day,
            };
        }
    }
}
