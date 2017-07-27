namespace EachVoice.Models.CustomU
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class RepVote
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string UserID { get; set; }

        [Required]
        [StringLength(50)]
        public string RepName { get; set; }

        [Required]
        [StringLength(50)]
        public string RepID { get; set; }

        public int Approval { get; set; }
    }
}
