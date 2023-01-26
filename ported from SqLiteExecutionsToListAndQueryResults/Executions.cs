using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExecutionsClass
{
    public class Executions
    {
        public Int64 Id { get; set; }

        public Int64 Account { get; set; }

        public Int64? BarIndex { get; set; }

        public Double? Commission { get; set; }
        //public float? Commission { get; set; }

        public Int64? Exchange { get; set; }

        public String ExecutionId { get; set; }

        public Double? Fee { get; set; }

        public Int64 Instrument { get; set; }

        public Int64? IsEntry { get; set; }
        public bool? IsEntryB { get; set; }

        public Int64? IsEntryStrategy { get; set; }

        public Int64? IsExit { get; set; }
        public Boolean? IsExitB { get; set; }

        public Int64? IsExitStrategy { get; set; }

        public Int64? LotSize { get; set; }

        public Int64? MarketPosition { get; set; }

        public Double? MaxPrice { get; set; }

        public Double? MinPrice { get; set; }

        public String Name { get; set; }

        public String OrderId { get; set; }

        public Int64? Position { get; set; }

        public Int64? PositionStrategy { get; set; }

        public Double? Price { get; set; }

        public Int64? Quantity { get; set; }

        public Double? Rate { get; set; }

        public Int64? StatementDate { get; set; }

        public Int64? Time { get; set; }

        public Object ServerName { get; set; }
        public Executions() { }
        public Executions(long id, long account, long? barIndex, double? commission, long? exchange, string executionId, double? fee, long instrument, long? isEntry, long? isEntryStrategy, long? isExit, long? isExitStrategy, long? lotSize, long? marketPosition, double? maxPrice, double? minPrice, string name, string orderId, long? position, long? positionStrategy, double? price, long? quantity, double? rate, long? statementDate, long? time, object serverName)
        {
            Id = id;
            Account = account;
            BarIndex = barIndex;
            Commission = commission;
            Exchange = exchange;
            ExecutionId = executionId;
            Fee = fee;
            Instrument = instrument;
            IsEntry = isEntry;
            IsEntryStrategy = isEntryStrategy;
            IsExit = isExit;
            IsExitStrategy = isExitStrategy;
            LotSize = lotSize;
            MarketPosition = marketPosition;
            MaxPrice = maxPrice;
            MinPrice = minPrice;
            Name = name;
            OrderId = orderId;
            Position = position;
            PositionStrategy = positionStrategy;
            Price = price;
            Quantity = quantity;
            Rate = rate;
            StatementDate = statementDate;
            Time = time;
            ServerName = serverName;
        }
    }
}
