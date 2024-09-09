using Microsoft.AspNetCore.Mvc;
using space_invaders_score_api.Entities;

namespace space_invaders_score_api.Utilities
{
    public class Validation
    {
        private readonly List<string> keys = new List<string>()
        {
            "aB1!cD2@eF3#gH4&",
            "xY5@zW6#vU7!tS8*",
            "M9*N0#kL1@jI2!oH_",
            "P3*Q4!rS5#T6@uV7-",
            "W8#X9!yZ0@aB1&cD2"
        };
        public Validation() { }

        public ReturnValidation KeyValidation(string key)
        {
            ReturnValidation returnValidation = new();

            if (string.IsNullOrEmpty(key))
            {
                returnValidation.Result = false;
                returnValidation.Message = "Key cannot be null or empty.";
                return returnValidation;
            } 

            if (!keys.Contains(key))
            {
                returnValidation.Result = false;
                returnValidation.Message = "Invalid key provided.";
                return returnValidation;
                
            }

            returnValidation.Result = true;

            return returnValidation;
        }
    }
}
