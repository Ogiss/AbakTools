using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API
{
    public interface IProgressForm
    {
        bool SecondProgressEnable { get; set; }
        string Text1 { get; set; }
        string Text2 { get; set; }
        void ResetProgress(bool secondProgress = false);
        void PerformStep(int step, bool secondProgress = false);

    }
}
