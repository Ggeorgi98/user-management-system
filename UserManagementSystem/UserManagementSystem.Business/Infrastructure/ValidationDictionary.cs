using UserManagementSystem.Business.Interfaces;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace UserManagementSystem.Business.Infrastructure
{
    public class ValidationDictionary : IValidationDictionary
    {
        private readonly ModelStateDictionary _modelState;

        public ValidationDictionary(ModelStateDictionary modelState)
        {
            _modelState = modelState;
        }

        public void AddModelError(string key, string message)
        {
            _modelState.AddModelError(key, message);
        }

        public bool IsValid()
        {
            return _modelState.IsValid;
        }

        public ModelStateDictionary GetModelState()
        {
            return _modelState;
        }
    }
}
