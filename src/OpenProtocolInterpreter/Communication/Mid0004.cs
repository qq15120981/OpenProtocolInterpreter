﻿using System.Collections.Generic;

namespace OpenProtocolInterpreter.Communication
{
    /// <summary>
    /// Application Communication negative acknowledge
    /// <para>
    ///     This message is used by the controller when a request, command or subscription for any reason has 
    ///     not been performed. 
    ///     The data field contains the message ID of the message request that failed as well as an error code.
    ///     It can also be used by the integrator to acknowledge received subscribed data/events upload and will
    ///     then do all the special subscription data acknowledges obsolete.
    /// </para>
    /// <para>
    ///     When using the communication acknowledgement of MID 0007 and <see cref="Mid0006"/> together with sequence 
    ///     numbering this is an application level message only.
    /// </para>
    /// <para>Message sent by: Controller</para>
    /// <para>Answer: None</para>
    /// </summary>
    public class Mid0004 : Mid, ICommunication, IController
    {
        public const int MID = 4;

        public int FailedMid
        {
            get => GetField(1, DataFields.Mid).GetValue(OpenProtocolConvert.ToInt32);
            set => GetField(1, DataFields.Mid).SetValue(OpenProtocolConvert.ToString, value);
        }
        public Error ErrorCode
        {
            get => (Error)GetField(1, DataFields.ErrorCode).GetValue(OpenProtocolConvert.ToInt32);
            set => GetField(1, DataFields.ErrorCode).SetValue(OpenProtocolConvert.ToString, value);
        }

        public Mid0004() : this(DEFAULT_REVISION)
        {

        }

        public Mid0004(Header header) : base(header)
        {
        }

        public Mid0004(int revision) : this(new Header()
        {
            Mid = MID,
            Revision = revision
        })
        {

        }

        public override string Pack()
        {
            HandleRevision();
            return base.Pack();
        }

        public override Mid Parse(string package)
        {
            Header = ProcessHeader(package);
            HandleRevision();
            ProcessDataFields(package);
            return this;
        }

        private void HandleRevision()
        {
            var errorCodeField = GetField(1, DataFields.ErrorCode);
            errorCodeField.Size = Header.Revision > 1 ? 3 : 2;
        }

        protected override Dictionary<int, List<DataField>> RegisterDatafields()
        {
            return new Dictionary<int, List<DataField>>()
            {
                {
                    1, new List<DataField>()
                            {
                                DataField.Number(DataFields.Mid, 20, 4, false),
                                DataField.Number(DataFields.ErrorCode, 24, 2, false)
                            }
                }
            };
        }


        protected enum DataFields
        {
            Mid,
            ErrorCode
        }
    }
}
