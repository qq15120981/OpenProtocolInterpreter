﻿using System.Collections.Generic;

namespace OpenProtocolInterpreter.ParameterSet
{
    /// <summary>
    /// Set Parameter set batch size
    /// <para>This message gives the possibility to set the batch size of a parameter set at run time.</para>
    /// <para>Message sent by: Integrator</para>
    /// <para>
    ///     Answer: <see cref="Communication.Mid0005"/> Command accepted or 
    ///     <see cref="Communication.Mid0004"/> Command error, Invalid data
    /// </para>
    /// </summary>
    public class Mid0019 : Mid, IParameterSet, IIntegrator, IAcceptableCommand, IDeclinableCommand
    {
        public const int MID = 19;

        public IEnumerable<Error> DocumentedPossibleErrors => new Error[] { Error.InvalidData };

        public int ParameterSetId
        {
            get => GetField(1, DataFields.ParameterSetId).GetValue(OpenProtocolConvert.ToInt32);
            set => GetField(1, DataFields.ParameterSetId).SetValue(OpenProtocolConvert.ToString, value);
        }
        public int BatchSize
        {
            get => GetField(1, DataFields.BatchSize).GetValue(OpenProtocolConvert.ToInt32);
            set => GetField(1, DataFields.BatchSize).SetValue(OpenProtocolConvert.ToString, value);
        }

        public Mid0019() : this(new Header()
        {
            Mid = MID,
            Revision = DEFAULT_REVISION
        })
        {
        }

        public Mid0019(Header header) : base(header)
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
            var batchSizeField = GetField(1, DataFields.BatchSize);
            if (Header.Revision > 1)
            {
                batchSizeField.Size = 4;
            }
            else
            {
                batchSizeField.Size = 2;
            }
        }

        protected override Dictionary<int, List<DataField>> RegisterDatafields()
        {
            return new Dictionary<int, List<DataField>>()
            {
                {
                    1, new List<DataField>()
                            {
                                DataField.Number(DataFields.ParameterSetId, 20, 3, false),
                                DataField.Number(DataFields.BatchSize, 23, 2, false),
                            }
                }
            };
        }

        protected enum DataFields
        {
            ParameterSetId,
            BatchSize
        }
    }
}
