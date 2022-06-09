using System;
using System.Collections.Generic;

namespace PNB.Domain.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool? Gender { get; set; }
        public decimal? Money { get; set; }
        public decimal? TotalMoney { get; set; }
        public string Address { get; set; }
        public string Address1 { get; set; }
        public string Company { get; set; }
        public string TaxNumber { get; set; }
        public string LastIp { get; set; }
        public bool IsSystemAccount { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
        public DateTime? CannotLoginUntilDate { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public string RoleId { get; set; }
        public string Avatar { get; set; }
        public string TokenLogin { get; set; }
    }
}
