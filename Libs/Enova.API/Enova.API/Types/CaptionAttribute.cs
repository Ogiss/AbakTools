using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Types
{
    [AttributeUsage(AttributeTargets.All)]
    public class CaptionAttribute : Attribute
    {
        // Fields
        public readonly string Text;

        // Methods
        public CaptionAttribute(string text)
        {
            this.Text = text;
        }
    }
}
