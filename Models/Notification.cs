using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public required string Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public required string UserId { get; set; }
       
    }
}
