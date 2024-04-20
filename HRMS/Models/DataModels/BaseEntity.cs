using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using System.Net.Sockets;

namespace HRMS.Models.DataModels
{
    //[Table("BasicInfo")]

    public abstract class BaseEntity 
	{
        [Key]
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public bool IsInActive { get; set; }
        public string Ip { get; set; } 



    }
}

