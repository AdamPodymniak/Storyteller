using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storyteller.Repository.Entities
{
    public class Text
    {
        public int ID { get; set; }
        public int SlideID { get; set; }
        public string TextValue { get; set; }
    }
}
