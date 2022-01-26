using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveTracker.Enums
{

    public enum TournamentVisibility
    {
        ShowAll,
        ShowStarted,
        ShowFive,
        ShowFifteen,
        ShowThirty,
        ShowSixty,
        Queued,
    }

    public enum TemplateManagerMode
    {
        Copy,
        Edit,
        New,
    }

    public enum SessionManagerMode
    {
        Copy,
        Delete,
        Edit,
        New,
    }
}
