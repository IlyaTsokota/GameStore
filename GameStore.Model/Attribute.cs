
using System.Collections.Generic;

using System.ComponentModel;

using System.ComponentModel.DataAnnotations;


namespace GameStore.Model
{
    public class Attribute
    {
        public int AttributeId { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        [DefaultValue(false)]
        public bool AllowFiltering { get; set; }
        [Required]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual List<AttributeValue> AttributeValues { get; set; }
    }
}
