using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task4.Models
{
    public class SortViewModel
    {
        public SortState IdSort { get; private set; }
        public SortState EmailSort { get; private set; }
        public SortState SocialNetworkSort { get; private set; }
        public SortState RegDateSort { get; private set; }
        public SortState LoginDateSort { get; private set; }
        public SortState BlockedSort { get; private set; }
        public SortState Current { get; private set; }

        public SortViewModel(SortState sortOrder)
        {
            IdSort = sortOrder == SortState.IdAsc ? SortState.IdDesc : SortState.IdAsc;
            EmailSort = sortOrder == SortState.EmailAsc ? SortState.EmailDesc : SortState.EmailAsc;
            SocialNetworkSort = sortOrder == SortState.SocialNetworkAsc ? SortState.SocialNetworkDesc : SortState.SocialNetworkAsc;
            RegDateSort = sortOrder == SortState.RegDateAsc ? SortState.RegDateDesc : SortState.RegDateAsc;
            LoginDateSort = sortOrder == SortState.LoginDateAsc ? SortState.LoginDateDesc : SortState.LoginDateAsc;
            BlockedSort = sortOrder == SortState.BlockedAsc ? SortState.BlockedDesc : SortState.BlockedAsc;

            Current = sortOrder;
        }
    }
}
