using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Btg.Messages
{
    public sealed class CloseWindowMessage : ValueChangedMessage<bool>
    {
        public CloseWindowMessage() : base(true) { }
    }
}
