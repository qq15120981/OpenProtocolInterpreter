﻿using OpenProtocolInterpreter.Converters;
using System.Collections.Generic;

namespace OpenProtocolInterpreter.IOInterface
{
    /// <summary>
    /// Status externally monitored inputs
    /// <para>
    ///    Status for the eight externally monitored digital inputs. This message is sent to the subscriber every
    ///    time the status of at least one of the inputs has changed.
    /// </para>
    /// <para>Message sent by: Controller</para>
    /// <para>Answer: <see cref="Mid0212"/> Status externally monitored inputs acknowledge</para>
    /// </summary>
    public class Mid0211 : Mid, IIOInterface, IController, IAcknowledgeable<Mid0212>
    {
        private readonly IValueConverter<bool> _boolConverter;
        public const int MID = 211;
        
        public bool StatusDigInOne
        {
            get => GetField(1,(int)DataFields.StatusDigIn1).GetValue(_boolConverter.Convert);
            set => GetField(1,(int)DataFields.StatusDigIn1).SetValue(_boolConverter.Convert, value);
        }
        public bool StatusDigInTwo
        {
            get => GetField(1,(int)DataFields.StatusDigIn2).GetValue(_boolConverter.Convert);
            set => GetField(1,(int)DataFields.StatusDigIn2).SetValue(_boolConverter.Convert, value);
        }
        public bool StatusDigInThree
        {
            get => GetField(1,(int)DataFields.StatusDigIn3).GetValue(_boolConverter.Convert);
            set => GetField(1,(int)DataFields.StatusDigIn3).SetValue(_boolConverter.Convert, value);
        }
        public bool StatusDigInFour
        {
            get => GetField(1,(int)DataFields.StatusDigIn4).GetValue(_boolConverter.Convert);
            set => GetField(1,(int)DataFields.StatusDigIn4).SetValue(_boolConverter.Convert, value);
        }
        public bool StatusDigInFive
        {
            get => GetField(1,(int)DataFields.StatusDigIn5).GetValue(_boolConverter.Convert);
            set => GetField(1,(int)DataFields.StatusDigIn5).SetValue(_boolConverter.Convert, value);
        }
        public bool StatusDigInSix
        {
            get => GetField(1,(int)DataFields.StatusDigIn6).GetValue(_boolConverter.Convert);
            set => GetField(1,(int)DataFields.StatusDigIn6).SetValue(_boolConverter.Convert, value);
        }
        public bool StatusDigInSeven
        {
            get => GetField(1,(int)DataFields.StatusDigIn7).GetValue(_boolConverter.Convert);
            set => GetField(1,(int)DataFields.StatusDigIn7).SetValue(_boolConverter.Convert, value);
        }
        public bool StatusDigInEight
        {
            get => GetField(1,(int)DataFields.StatusDigIn8).GetValue(_boolConverter.Convert);
            set => GetField(1,(int)DataFields.StatusDigIn8).SetValue(_boolConverter.Convert, value);
        }

        public Mid0211() : this(new Header()
        {
            Mid = MID, 
            Revision = DEFAULT_REVISION
        })
        {
            
        }

        public Mid0211(Header header) : base(header)
        {
            _boolConverter = new BoolConverter();
        }

        protected override Dictionary<int, List<DataField>> RegisterDatafields()
        {
            return new Dictionary<int, List<DataField>>()
            {
                {
                    1, new List<DataField>()
                    {
                        new DataField((int)DataFields.StatusDigIn1, 20, 1, false),
                        new DataField((int)DataFields.StatusDigIn2, 21, 1, false),
                        new DataField((int)DataFields.StatusDigIn3, 22, 1, false),
                        new DataField((int)DataFields.StatusDigIn4, 23, 1, false),
                        new DataField((int)DataFields.StatusDigIn5, 24, 1, false),
                        new DataField((int)DataFields.StatusDigIn6, 25, 1, false),
                        new DataField((int)DataFields.StatusDigIn7, 26, 1, false),
                        new DataField((int)DataFields.StatusDigIn8, 27, 1, false)
                    }
                }
            };
        }

        protected enum DataFields
        {
            StatusDigIn1,
            StatusDigIn2,
            StatusDigIn3,
            StatusDigIn4,
            StatusDigIn5,
            StatusDigIn6,
            StatusDigIn7,
            StatusDigIn8
        }
    }
}
