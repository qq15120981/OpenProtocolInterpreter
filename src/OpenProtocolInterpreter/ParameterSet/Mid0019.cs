﻿using OpenProtocolInterpreter.Converters;
using System;
using System.Collections.Generic;
using System.Linq;

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
        private readonly IValueConverter<int> _intConverter;
        public const int MID = 19;

        public IEnumerable<Error> DocumentedPossibleErrors => new Error[] { Error.InvalidData };

        public int ParameterSetId
        {
            get => GetField(1,(int)DataFields.ParameterSetId).GetValue(_intConverter.Convert);
            set => GetField(1,(int)DataFields.ParameterSetId).SetValue(_intConverter.Convert, value);
        }
        public int BatchSize
        {
            get => GetField(1,(int)DataFields.BatchSize).GetValue(_intConverter.Convert);
            set => GetField(1,(int)DataFields.BatchSize).SetValue(_intConverter.Convert, value);
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
            _intConverter = new Int32Converter();
        }

        protected override Dictionary<int, List<DataField>> RegisterDatafields()
        {
            return new Dictionary<int, List<DataField>>()
            {
                {
                    1, new List<DataField>()
                            {
                                new DataField((int)DataFields.ParameterSetId, 20, 3, '0', DataField.PaddingOrientations.LeftPadded, false),
                                new DataField((int)DataFields.BatchSize, 23, 2, '0', DataField.PaddingOrientations.LeftPadded, false),
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
