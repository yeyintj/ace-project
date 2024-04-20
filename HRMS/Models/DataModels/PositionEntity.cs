using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HRMS.Models.DataModels;

namespace HRMS.Models.DataModels
{
    [Table("Position")]
    public class PositionEntity : BaseEntity
    {
        //[Key]
        //public string Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
    }
}

