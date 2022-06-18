using System.Text;

namespace Vue.Infrastructure.VueService
{
    internal class VueRequestBuilder
    {
        private readonly StringBuilder _stringBuilder;
        private bool firstAppend = true;

        private char AppendingChar
        {
            get
            {
                if (firstAppend)
                {
                    firstAppend = false;
                    return '?';
                }
                return '&';
            }
        }

        internal VueRequestBuilder(VueEndpoint vueEndpoint)
        {
            _stringBuilder = new StringBuilder();
            switch (vueEndpoint)
            {
                case VueEndpoint.PERFORMANCES:
                    _stringBuilder.Append("performances.json");
                    break;
                case VueEndpoint.MOVIES:
                    _stringBuilder.Append("movies.json");
                    break;
                case VueEndpoint.LOGIN:
                    _stringBuilder.Append("user/login.json");
                    break;
                case VueEndpoint.CART:
                    _stringBuilder.Append("api/cart.php");
                    break;
                case VueEndpoint.AUTOCOMPLETE:
                    _stringBuilder.Append("autocomplete.json");
                    break;
                default: 
                    throw new ArgumentOutOfRangeException(nameof(vueEndpoint));
            }
        }

        public VueRequestBuilder WithRange(int range)
        {
            if (range < 1)
                throw new ArgumentOutOfRangeException(nameof(range));
            _stringBuilder.Append($"{AppendingChar}range={range}");
            return this;
        }

        public VueRequestBuilder WithType(string type)
        {
            if (string.IsNullOrEmpty(type))
                throw new ArgumentException(nameof(type));
            _stringBuilder.Append($"{AppendingChar}type={type}");
            return this;
        }

        public VueRequestBuilder WithFilters(params (string, string)[] filters)
        {
            if (!filters.Any())
                _stringBuilder.Append($"{AppendingChar}filters=");
            else
            {
                foreach ((string filter, string value) in filters)
                {
                    // &filters[cinema_id][]=in&filters[cinema_id][]=23 
                    _stringBuilder.Append($"{AppendingChar}filters[{filter}][]={value}");
                }
            }
            return this;
        }

        public VueRequestBuilder WithLimit(int limit)
        {
            if (limit < 1)
                throw new ArgumentOutOfRangeException(nameof(limit));
            _stringBuilder.Append($"{AppendingChar}limit={limit}");
            return this;
        }

        public VueRequestBuilder WithMovieId(int movieId)
        {
            if (movieId < 1)
                throw new ArgumentOutOfRangeException(nameof(movieId));
            _stringBuilder.Append($"{AppendingChar}movie_id={movieId}");
            return this;
        }

        public VueRequestBuilder WithCinemaIds(params int[] cinemaIds)
        {
            if (!cinemaIds.Any())
                _stringBuilder.Append($"{AppendingChar}cinema_ids%5B%5D=");
            else
            {
                foreach (int cinemaId in cinemaIds)
                {
                    //cinema_ids[]=23
                    _stringBuilder.Append($"{AppendingChar}cinema_ids%5B%5D={cinemaId}");
                }
            }
            return this;
        }

        public VueRequestBuilder WithAction(string action)
        {
            if (string.IsNullOrEmpty(action))
                throw new ArgumentException(nameof(action));
            _stringBuilder.Append($"{AppendingChar}action={action}");
            return this;
        }

        public VueRequestBuilder WithPerformanceId(int performanceId)
        {
            if (performanceId < 1)
                throw new ArgumentOutOfRangeException(nameof(performanceId));
            _stringBuilder.Append($"{AppendingChar}performance_id={performanceId}");
            return this;
        }

        public VueRequestBuilder WithDateOffset(DateTime dateTime = default)
        {
            if (default == dateTime)
                _stringBuilder.Append($"{AppendingChar}dateOffset=");
            else
            {
                string dateOffset = dateTime.ToString("yyyy-MM-dd+HH:mm:00");
                _stringBuilder.Append($"{AppendingChar}dateOffset={dateOffset}");
            }
            return this;
        }

        public VueRequestBuilder WithReservation()
        {
            //reservation[performanceId]: 11075771
            //reservation[seat][number]: 79
            //reservation[seat][row]: 4
            //reservation[seat][seat]: 5
            //reservation[seat][area]: 1
            //reservation[seat][type]: 1
            //reservation[priceCategory][id]: 2200101651
            //reservation[priceCategory][performance_id]: 11075771
            //reservation[priceCategory][price_oid]: 10000000999YBADPUC
            //reservation[priceCategory][price_name]: Standaard
            //reservation[priceCategory][price]: 11.5
            //reservation[priceCategory][reservation_fee]: 1.00
            //reservation[priceCategory][ticket_fee]: 0.00
            //reservation[priceCategory][allowed_for]: 0
            //reservation[priceCategory][minimum_quantity]: 1
            //reservation[priceCategory][maximum_quantity]: 0
            //reservation[priceCategory][ticket_type_oid]: 
            //reservation[priceCategory][seating_area_oid]: 1
            //reservation[priceCategory][_price]: € 11.50
            //reservation[priceCategory][type]: REGULAR
            throw new NotImplementedException();
        }

        public VueRequestBuilder WithSeatNumber(int seatNumber)
        {
            //https://www.vuecinemas.nl/services/api/cart.php?action=cancel&seat_number=79&performance_id=11075771
            //action: cancel
            //seat_number: 79
            //performance_id: 11075771
            throw new NotImplementedException();
        }

        public VueRequestBuilder WithQuery(string query)
        {
            //type: aggregate
            //limit: 20
            //query: top
            throw new NotImplementedException();
        }

        public string Build()
        {
            return _stringBuilder.ToString();
        }
    }
}
