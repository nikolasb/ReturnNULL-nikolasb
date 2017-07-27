using EachVoice.Models.CustomU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EachVoice.Models
{
    public class MostCommentViewModel
    {
        public UserComment Comment { get; set; }
        public int MostCount { get; set; } 
    }
}