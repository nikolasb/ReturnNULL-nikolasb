using EachVoice.Models.CustomU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EachVoice.Models
{
    public class MostApprovedBillViewModel
    {
        public int aVote { get; set; }
        public UserVote vote { get; set; }
    }
}