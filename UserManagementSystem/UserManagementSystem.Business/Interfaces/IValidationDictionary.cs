using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace UserManagementSystem.Business.Interfaces
{
    public interface IValidationDictionary
    {
        bool IsValid();

        void AddModelError(string key, string message);

        ModelStateDictionary GetModelState();
    }
}
