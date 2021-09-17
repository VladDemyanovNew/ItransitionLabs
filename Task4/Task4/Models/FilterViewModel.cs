using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task4.Models
{
    public class FilterViewModel
    {
        public FilterViewModel(List<string> socialNetworks, string socialNetwork, string status)
        {
            List<string> statuses = new List<string> { "All", "True", "False" };
            socialNetworks.Insert(0, "All");
            SocialNetworks = new SelectList(socialNetworks, "Name");
            SelectedSocialNetwork = socialNetwork;
            Statuses = new SelectList(statuses, "Name");
            SelectedStatus = status;
        }

        public SelectList SocialNetworks { get; private set; }
        public SelectList Statuses { get; private set; }
        public string SelectedSocialNetwork { get; private set; }
        public string SelectedStatus { get; private set; }
    }
}
