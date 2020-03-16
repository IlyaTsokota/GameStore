
using System.ComponentModel.DataAnnotations;


namespace GameStore.Model
{
    public class AttributeValue
    {
        public int AttributeValueId { get; set; }
        [MaxLength(40)]
        public string Value { get; set; }
        [Required]
        public int AttributeId { get; set; }
        [Required]
        public int ProductId { get; set; }

        public virtual Attribute Attribute { get; set; }

        public virtual Product Product { get; set; }
    }
}
