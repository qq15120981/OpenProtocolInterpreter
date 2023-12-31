﻿using System.Collections.Generic;

namespace OpenProtocolInterpreter.ParameterSet
{
    /// <summary>
    /// Parameter set data upload request
    /// <para>Request to upload parameter set data from the controller.</para>
    /// <para>Message sent by: Integrator</para>
    /// <para>
    /// Answer: <see cref="Mid0013"/> Parameter set data upload reply, or 
    ///         <see cref="Communication.Mid0004"/> Command error, Parameter set not present
    /// </para>
    /// </summary>
    public class Mid0012 : Mid, IParameterSet, IIntegrator, IAnswerableBy<Mid0013>, IDeclinableCommand
    {
        public const int MID = 12;

        public IEnumerable<Error> DocumentedPossibleErrors => new Error[] { Error.ParameterSetIdNotPresent };

        public int ParameterSetId
        {
            get => GetField(1, DataFields.ParameterSetId).GetValue(OpenProtocolConvert.ToInt32);
            set => GetField(1, DataFields.ParameterSetId).SetValue(OpenProtocolConvert.ToString, value);
        }
        public int ParameterSetFileVersion
        {
            get => GetField(3, DataFields.PSetFileVersion).GetValue(OpenProtocolConvert.ToInt32);
            set => GetField(3, DataFields.PSetFileVersion).SetValue(OpenProtocolConvert.ToString, value);
        }

        public Mid0012() : this(DEFAULT_REVISION)
        {

        }

        public Mid0012(int revision) : this(new Header()
        {
            Mid = MID,
            Revision = revision
        })
        {
        }

        public Mid0012(Header header) : base(header)
        {
        }

        protected override Dictionary<int, List<DataField>> RegisterDatafields()
        {
            return new Dictionary<int, List<DataField>>()
            {
                {
                    1, new List<DataField>()
                            {
                                DataField.Number(DataFields.ParameterSetId, 20, 3, false)
                            }
                },
                {
                    3, new  List<DataField>()
                            {
                                DataField.Number(DataFields.PSetFileVersion, 23, 8, false)
                            }
                },
            };
        }

        protected enum DataFields
        {
            //Revision 1-2
            ParameterSetId,
            //Revision 3-4
            PSetFileVersion
        }
    }
}
