using MinimalApiDemo.Exceptions;

namespace MinimalApiDemo.Endpoints.Orders
{
    public class OrderIds
    {
        public List<int> Ids { get; set; }

        public static bool TryParse(string? value, IFormatProvider? provider, out OrderIds orderIds)
        {
            if (!value.Contains('-'))
                throw new NormalException();

            orderIds = new OrderIds { Ids = [] };
            try
            {
                if (value is not null && value.Contains('-'))
                {
                    orderIds.Ids = value.Split('-').Select(int.Parse).ToList();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
