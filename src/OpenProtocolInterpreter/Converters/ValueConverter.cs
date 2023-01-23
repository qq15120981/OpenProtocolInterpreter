﻿namespace OpenProtocolInterpreter.Converters
{
    public class ValueConverter
    {
        public string GetTruncatePadded(char paddingChar, int size, DataField.PaddingOrientations orientation, string value)
        {
            if (value == null)
                return string.Empty.PadLeft(size, paddingChar);

            if(value.Length > size)
            {
                value = value.Substring(0, size);
            }

            if (orientation == DataField.PaddingOrientations.RIGHT_PADDED)
                return value.PadRight(size, paddingChar);

            return value.PadLeft(size, paddingChar);
        }
    }
}
