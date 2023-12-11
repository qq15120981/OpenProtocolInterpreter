﻿using System.Collections.Generic;

namespace OpenProtocolInterpreter.Communication
{
    /// <summary>
    /// Application Data Message unsubscribe
    /// <para>
    ///     Unsubscribe the data. This message is used for ALL unsubscribe.
    ///     When used it substitutes the use of all MID special subscription messages.
    /// </para>
    /// <para>
    ///     NOTE! The Header Revision field is the revision of the MID 0009 itself NOT the revision of the data
    ///     MID that is wanted to be subscribed for.
    /// </para>
    /// <para>Message sent by: Integrator</para>
    /// <para>
    /// Answer: <see cref="Mid0005"/> Command accepted with the MID subscribed for or <see cref="Mid0004"/> Command error, 
    ///         MID revision unsupported or Invalid data code and the MID subscribed for
    /// </para>
    /// </summary>
    public class Mid0009 : Mid, ICommunication, IIntegrator
    {
        public const int MID = 9;

        public string UnsubscriptionMid
        {
            get => GetField(1, DataFields.UnsubscriptionMid).Value;
            set => GetField(1, DataFields.UnsubscriptionMid).SetValue(value);
        }
        public int ExtraDataRevision
        {
            get => GetField(1, DataFields.ExtraDataRevision).GetValue(OpenProtocolConvert.ToInt32);
            set => GetField(1, DataFields.ExtraDataRevision).SetValue(OpenProtocolConvert.ToString, value);
        }
        public int ExtraDataLength
        {
            get => GetField(1, DataFields.ExtraDataLength).GetValue(OpenProtocolConvert.ToInt32);
            set => GetField(1, DataFields.ExtraDataLength).SetValue(OpenProtocolConvert.ToString, value);
        }
        public string ExtraData
        {
            get => GetField(1, DataFields.ExtraData).Value;
            set => GetField(1, DataFields.ExtraData).SetValue(value);
        }

        public Mid0009() : this(new Header()
        {
            Mid = MID,
            Revision = DEFAULT_REVISION
        })
        {

        }

        public Mid0009(Header header) : base(header)
        {
        }

        public override Mid Parse(string package)
        {
            Header = ProcessHeader(package);
            GetField(1, DataFields.ExtraData).Size = Header.Length - 29;
            ProcessDataFields(package);
            return this;
        }

        protected override Dictionary<int, List<DataField>> RegisterDatafields()
        {
            return new Dictionary<int, List<DataField>>()
            {
                {
                    1, new List<DataField>()
                            {
                                DataField.Number(DataFields.UnsubscriptionMid, 20, 4, false),
                                DataField.Number(DataFields.ExtraDataRevision, 24, 3, false),
                                DataField.Number(DataFields.ExtraDataLength, 27, 2, false),
                                DataField.Volatile(DataFields.ExtraData, 29, false)
                            }
                }
            };
        }

        protected enum DataFields
        {
            UnsubscriptionMid,
            ExtraDataRevision,
            ExtraDataLength,
            ExtraData
        }
    }
}
