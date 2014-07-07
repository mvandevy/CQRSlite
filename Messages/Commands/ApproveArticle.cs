using System;
using CQRSlite.Commands;

namespace Messages.Commands
{
    public class ApproveArticle : ICommand
    {
        public readonly Guid Id;

        public ApproveArticle(Guid id)
        {
            Id = id;
        }

        public int ExpectedVersion { get; set; }
    }
}
