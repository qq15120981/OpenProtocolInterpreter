﻿using OpenProtocolInterpreter.Converters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenProtocolInterpreter.Job
{
    /// <summary>
    /// Job data upload request
    /// <para>Request to upload the data for a specific Job from the controller.</para>
    /// <para>Message sent by: Integrator</para>
    /// <para>Answer: <see cref="Mid0033"/> Job data upload or <see cref="Communication.Mid0004"/> Command error, Job ID not present</para>
    /// </summary>
    public class Mid0032 : Mid, IJob, IIntegrator, IAnswerableBy<Mid0033>, IDeclinableCommand
    {
        private readonly IValueConverter<int> _intConverter;
        public const int MID = 32;

        public IEnumerable<Error> DocumentedPossibleErrors => new Error[] { Error.JobIdNotPresent };

        public int JobId
        {
            get => GetField(1, (int)DataFields.JobId).GetValue(_intConverter.Convert);
            set => GetField(1, (int)DataFields.JobId).SetValue(_intConverter.Convert, value);
        }

        public Mid0032() : this(DEFAULT_REVISION)
        {

        }

        public Mid0032(Header header) : base(header)
        {
            _intConverter = new Int32Converter();
            HandleRevision();
        }

        public Mid0032(int revision) : this(new Header()
        {
            Mid = MID,
            Revision = revision
        })
        {
        }

        public override Mid Parse(string package)
        {
            Header = ProcessHeader(package);
            HandleRevision();
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
                                new DataField((int)DataFields.JobId, 20, 2, '0', DataField.PaddingOrientations.LeftPadded, false),
                            }
                }
            };
        }

        private void HandleRevision()
        {
            if (Header.Revision == 1)
                GetField(1, (int)DataFields.JobId).Size = 2;
            else
                GetField(1, (int)DataFields.JobId).Size = 4;
        }

        protected enum DataFields
        {
            JobId
        }
    }
}