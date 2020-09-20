namespace TestWebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Side
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int GamesID { get; set; }

        public int PlayerID { get; set; }

        [StringLength(5)]
        public string YourColor { get; set; }

        public int? IsYourMove { get; set; }

        public virtual Game Game { get; set; }

        public virtual Player Player { get; set; }
    }
}
