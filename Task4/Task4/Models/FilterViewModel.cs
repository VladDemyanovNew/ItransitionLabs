using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task4.Models
{
    public class FilterViewModel
    {
        public FilterViewModel(List<string> socialNetworks, string socialNetwork)
        {
            socialNetworks.Insert(0, "All");
            SocialNetworks = new SelectList(socialNetworks, "Name");
            SelectedSocialNetwork = socialNetwork;
        }

        public SelectList SocialNetworks { get; private set; }
        public string SelectedSocialNetwork { get; private set; }
    }
}
