using System;
using CQRSlite.Events;

namespace Messages.Events
{
    public class ArticleApproved : IEvent
    {
        public ArticleApproved(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
        public int Version { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
    }
}