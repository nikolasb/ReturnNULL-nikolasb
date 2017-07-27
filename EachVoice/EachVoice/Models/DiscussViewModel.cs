
using EachVoice.Models.CustomU;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Web;

namespace EachVoice.Models
{
    public class DiscussViewModel
    {
       public List<UserComment> list { get; set; }
       

       public UserComment comment { get; set; }
    }
}