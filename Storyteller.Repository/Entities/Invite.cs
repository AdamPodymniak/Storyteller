using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storyteller.Repository.Entities
{
    public class Invite
    {
        public int ID { get; set; }
        public Guid Invitation { get; set; }
        public bool Used { get; set; }
        public string Role { get; set; }
    }
}
