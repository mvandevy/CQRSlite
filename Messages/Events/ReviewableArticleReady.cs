using System;
using CQRSlite.Events;

namespace Messages.Events
{
    public class ReviewableArticleReady : IEvent
    {
        public readonly string Title;

        public ReviewableArticleReady(Guid id, string title)
        {
            Id = id;
            Title = title;
        }

        public Guid Id { get; set; }
        public int Version { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
    }
}
