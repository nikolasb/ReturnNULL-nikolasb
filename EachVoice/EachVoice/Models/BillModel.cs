using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EachVoice.Models
{
    public class BillModel
    {
        public string id;
        public string title;
        public int relevance;

        public BillModel(string id, string title, int relevance)
        {
            this.id = id;
            this.title = title;
            this.relevance = relevance;
        }
    }
}