using GameStore.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Service
{
    public interface IAttributeValueService
    {
        List<AttributeValue> GetAttributeValues(int productId);

        List<string> GetDistinctValues(int attributeId, string term);

        void EditAttributeValues(List<AttributeValue> attributeValues);

    }
}
