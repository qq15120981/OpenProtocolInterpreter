﻿using OpenProtocolInterpreter.Converters;
using System.Collections.Generic;
using System.Linq;

namespace OpenProtocolInterpreter.ApplicationSelector
{
    /// <summary>
    /// Selector socket info
    /// <para>
    ///     This message is sent each time a socket is lifted or put back in position. 
    ///     This MID contains the device ID of the selector the information is coming from, 
    ///     the number of sockets of the selector device, and the current status of each socket 
    ///     (lifted or not lifted).
    /// </para>
    /// <para>Message sent by: Controller</para>
    /// <para>Answer: <see cref="Mid0252"/>, Selector socket info acknowledge</para>
    /// </summary>
    public class Mid0251 : Mid, IApplicationSelector, IController, IAcknowledgeable<Mid0252>
    {
        private readonly IValueConverter<int> _intConverter;
        private readonly IValueConverter<IEnumerable<bool>> _boolListConverter;
        public const int MID = 251;

        public int DeviceId
        {
            get => GetField(1, (int)DataFields.DeviceId).GetValue(_intConverter.Convert);
            set => GetField(1, (int)DataFields.DeviceId).SetValue(_intConverter.Convert, value);
        }
        public int NumberOfSockets
        {
            get => GetField(1, (int)DataFields.NumberOfSockets).GetValue(_intConverter.Convert);
            set => GetField(1, (int)DataFields.NumberOfSockets).SetValue(_intConverter.Convert, value);
        }
        public List<bool> SocketStatus { get; set; }

        public Mid0251() : this(new Header()
        {
            Mid = MID,
            Revision = DEFAULT_REVISION
        })
        {
            
        }

        public Mid0251(Header header) : base(header)
        {
            _intConverter = new Int32Converter();
            _boolListConverter = new SocketStatusConverter(new BoolConverter());
            if (SocketStatus == null)
                SocketStatus = new List<bool>();
        }

        public override string Pack()
        {
            GetField(1, (int)DataFields.SocketStatus).Size = NumberOfSockets;
            GetField(1, (int)DataFields.SocketStatus).Value = _boolListConverter.Convert(SocketStatus);
            return base.Pack();
        }

        public override Mid Parse(string package)
        {
            Header = ProcessHeader(package);

            GetField(1, (int)DataFields.SocketStatus).Size = Header.Length - 30;
            ProcessDataFields(package);
            SocketStatus = _boolListConverter.Convert(GetField(1, (int)DataFields.SocketStatus).Value).ToList();
            return this;
        }

        protected override Dictionary<int, List<DataField>> RegisterDatafields()
        {
            return new Dictionary<int, List<DataField>>()
            {
                {
                    1, new List<DataField>()
                            {
                                new DataField((int)DataFields.DeviceId, 20, 2, '0', DataField.PaddingOrientations.LeftPadded),
                                new DataField((int)DataFields.NumberOfSockets, 24, 2, '0', DataField.PaddingOrientations.LeftPadded),
                                new DataField((int)DataFields.SocketStatus, 28, 0)
                            }
                }
            };
        }

        protected enum DataFields
        {
            DeviceId,
            NumberOfSockets,
            SocketStatus
        }
    }
}
