using System;

namespace NetCellLeaderboards.Runtime
{
    public static class ThrowIf
    {
        public static void StringParameterIsEmpty(string parameterName, string parameterValue)
        {
            if (string.IsNullOrEmpty(parameterValue) || string.IsNullOrWhiteSpace(parameterValue))
            {
                throw new ArgumentException($"Parameter {parameterName} is empty string. This is not allowed. Raw value: \"{parameterValue}\"");
            }
        }
    }
}