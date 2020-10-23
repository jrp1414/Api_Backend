namespace APIAngular.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Student")]
    public partial class Student
    {
        public int StudentId { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(20)]
        public string MobileNo { get; set; }

        [StringLength(50)]
        public string EmailId { get; set; }

        public int? AddressId { get; set; }

        public virtual Address Address { get; set; }
    }
}
