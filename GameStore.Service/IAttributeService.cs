
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Attribute = GameStore.Model.Attribute;

namespace GameStore.Service
{
      public interface IAttributeService
    {
        Attribute GeAttribute(int id);

        List<Attribute> GetAttributes();

        IEnumerable<ValidationResult> CanAddAttribute(Attribute newAttribute);

        List<Attribute> GetAttributesForFiltering(int categoryId);

        void CreateAttribute(Attribute attribute);

        void UpdateAttribute(Attribute attribute);

        void DeleteAttribute(Attribute attribute);

    }
}
