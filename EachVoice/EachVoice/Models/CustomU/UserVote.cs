namespace EachVoice.Models.CustomU
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UserVote
    {
        public int id { get; set; }

        [Required]
        [StringLength(450)]
        public string ucblid { get; set; }

        [Required]
        [StringLength(128)]
        public string ucnetuid { get; set; }

        public int? votebit { get; set; }
    }
}
