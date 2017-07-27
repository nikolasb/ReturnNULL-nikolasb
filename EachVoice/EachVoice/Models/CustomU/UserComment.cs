namespace EachVoice.Models.CustomU
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UserComment
    {
        public int id { get; set; }

        [Required]
        [StringLength(450)]
        public string blid { get; set; }

        [Required]
        public string comt { get; set; }

        [StringLength(128)]
        public string netuid { get; set; }

        [Required]
        public string bltitle { get; set; }
    }
}
