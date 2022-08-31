using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storyteller.Repository.Entities
{
    public class Story
    {
        public int ID { get; set; }
        public Guid StoryGuid { get; set; }
        public Guid UserGuid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ImgPath { get; set; }
    }
}
