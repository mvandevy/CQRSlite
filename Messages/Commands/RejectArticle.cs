using System;
using CQRSlite.Commands;

namespace Messages.Commands
{
    public class RejectArticle : ICommand
    {
        public readonly Guid Id;

        public RejectArticle(Guid id)
        {
            Id = id;
        }
        public int ExpectedVersion { get; set; }
    }
}
