﻿using System.Collections.Generic;

namespace OpenProtocolInterpreter.MultipleIdentifiers
{
    /// <summary>
    /// Identifier download request
    /// <para>Used by the integrator to send an identifier to the controller.</para>
    /// <para>Message sent by: Integrator</para>
    ///<para>Answer: <see cref="Communication.Mid0005"/> Command accepted or <see cref="Communication.Mid0004"/> Command error, Identifier input source not granted</para> 
    /// </summary>
    public class Mid0150 : Mid, IMultipleIdentifier, IIntegrator, IAcceptableCommand, IDeclinableCommand
    {
        private const int LAST_REVISION = 1;
        public const int MID = 150;

        public IEnumerable<Error> DocumentedPossibleErrors => new Error[] { Error.IDENTIFIER_INPUT_SOURCE_NOT_GRANTED };

        public string IdentifierData
        {
            get => GetField(1, (int)DataFields.IDENTIFIER_DATA).Value;
            set
            {
                var field = GetField(1, (int)DataFields.IDENTIFIER_DATA);
                field.Size = value.Length < 100 ? value.Length : 100;
                field.SetValue(value);
            }
        }

        public Mid0150() : this(new Header()
        {
            Mid = MID,
            Revision = LAST_REVISION
        }) 
        { 
        }

        public Mid0150(Header header) : base(header)
        {
        }

        public Mid0150(string identifierData) : this()
        {
            IdentifierData = identifierData;
        }

        public override Mid Parse(string package)
        {
            Header = ProcessHeader(package);
            GetField(1, (int)DataFields.IDENTIFIER_DATA).Size = Header.Length - 20;
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
                        new DataField((int)DataFields.IDENTIFIER_DATA, 20, 0, false)
                    }
                }
            };
        }

        public enum DataFields
        {
            IDENTIFIER_DATA
        }
    }
}
