using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNet.models
{
    public enum OrderStatus
    {
        NotStarted,
        Loading,
        InProgress,
        Arrived,
        Unloading,
        Cancelled,
        Done
    }
    public class Order
    {
        public int Id { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int ProductID { get; set; }
    }
}
