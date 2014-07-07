using System;
using CQRSlite.Commands;
using CQRSlite.Messages;

namespace Messages.Commands
{
    [OnMessageSend(SendChannel.InterProc)]
    public class SubmitArticle : ICommand
    {
        public readonly string Title;
        public readonly string Body;

        public SubmitArticle(Guid id, string title, string body)
        {
            Id = id;
            Title = title;
            Body = body;
        }

        public Guid Id { get; set; }
        public int ExpectedVersion { get; set; }
    }

    
}
