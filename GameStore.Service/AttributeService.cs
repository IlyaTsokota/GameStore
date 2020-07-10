using GameStore.Data.Infrastructure;
using GameStore.Data.Repositories;
using GameStore.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Attribute = GameStore.Model.Attribute;

namespace GameStore.Service
{
   public class AttributeService : IAttributeService
    {
        private readonly IAttributeRepository _attributeRepository;

        private readonly IUnitOfWork _unitOfWork;

        public AttributeService(IAttributeRepository AttributeRepository, IUnitOfWork unitOfWork)
        {
            _attributeRepository = AttributeRepository;
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<ValidationResult> CanAddAttribute(Attribute newAttribute)
        {
            Attribute attribute;
            if (newAttribute.AttributeId == 0)
            {
                attribute = _attributeRepository.Get(
                    g => g.Name == newAttribute.Name
                         && g.CategoryId == newAttribute.CategoryId);
            }
            else
            {
                attribute = _attributeRepository.Get(
                    g => g.Name == newAttribute.Name
                         && g.CategoryId == newAttribute.CategoryId
                         && g.AttributeId != newAttribute.AttributeId);
            }

            if (attribute != null)
            {
                yield return new ValidationResult("Атрибут с данным названием уже существует");
            }
        }


        public void CreateAttribute(Attribute attribute)
        {
            _attributeRepository.Add(attribute);
            _unitOfWork.Commit();
        }

        public void DeleteAttribute(Attribute attribute)
        {
            _attributeRepository.Delete(attribute);
            _unitOfWork.Commit();
        }

        public void UpdateAttribute(Attribute attribute)
        {
            _attributeRepository.Update(attribute);
            _unitOfWork.Commit();
        }
        public List<Attribute> GetAttributesForFiltering(int categoryId)
        {
            var attributes = _attributeRepository.GetMany(x => x.CategoryId == categoryId
                                                                && x.AttributeValues.Count > 0
                                                                && x.AllowFiltering).ToList();
            foreach (var attribute in attributes)
            {
                attribute.AttributeValues = attribute.AttributeValues
                   .Where(x => !string.IsNullOrEmpty(x.Value))
                   .GroupBy(x => x.Value)
                   .Select(x => new AttributeValue { AttributeId = attribute.AttributeId, Value = x.Key })
                   .OrderBy(x => x.Value).ToList();
            }

            return attributes.ToList();
        }
        public Attribute GeAttribute(int id) => _attributeRepository.GetById(id);

        public List<Attribute> GetAttributes() => _attributeRepository.GetAll().ToList();

      
    }
}
