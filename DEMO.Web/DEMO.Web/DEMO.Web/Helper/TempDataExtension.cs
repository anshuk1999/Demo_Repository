using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DEMO.Web.Helper
{
    public static class TempDataExtension
    {
        private const string Key = "ModelStateTemp";

        public static void SaveModelState(this Controller controller)
        {
            var errorList = controller.ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToList()
                );

            controller.TempData[Key] = JsonConvert.SerializeObject(errorList);
        }

        public static void RestoreModelState(this Controller controller)
        {
            if (controller.TempData.ContainsKey(Key))
            {
                var errors = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(
                    controller.TempData[Key].ToString()
                );

                foreach (var field in errors)
                {
                    foreach (var error in field.Value)
                    {
                        controller.ModelState.AddModelError(field.Key, error);
                    }
                }
            }
        }
    }
}
