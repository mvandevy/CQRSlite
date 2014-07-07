using System;

namespace CQRSlite.Messages
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class OnMessageSendAttribute : Attribute
    {
        private readonly SendChannel[] _channels;

        public OnMessageSendAttribute(params SendChannel[] channels)
        {
            _channels = channels;
        }

        public SendChannel[] Channels { get { return _channels; } }
    }

    public enum SendChannel
    {
        InProc,
        InterProc
    }
}