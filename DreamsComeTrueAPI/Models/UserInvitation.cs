using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DreamsComeTrueAPI.Models.Enums;

namespace DreamsComeTrueAPI.Models
{
    public class UserInvitation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public User UserInvitating { get; set; }
        public User InvitedUser { get; set; }
        public DateTime Date { get; set; }
        public InvitationType InvitationType { get; set; } = InvitationType.Waiting;
    }
}