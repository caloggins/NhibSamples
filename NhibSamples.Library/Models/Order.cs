namespace NhibSamples.Library.Models
{
    using System;

    public class Order
    {
        public virtual long OrderId { get; set; }
        public virtual string Comments { get; set; }
        public virtual DateTime DatePlaced { get; set; }
        public virtual byte[] Version { get; set; }

        public override string ToString()
        {
            return string.Format("Order ({0}) - {1}", OrderId, DatePlaced);
        }
    }
}