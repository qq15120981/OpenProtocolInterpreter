﻿using System.Collections.Generic;

namespace OpenProtocolInterpreter.MultipleIdentifiers
{
    /// <summary>
    /// Multiple identifier and result parts unsubscribe
    /// <para>Reset the subscription for the multiple identifiers and result parts.</para>
    /// <para>Message sent by: Integrator</para>
    /// <para>
    /// Answer: <see cref="Communication.Mid0005"/> Command accepted or 
    /// <see cref="Communication.Mid0004"/> Command error, Multiple identifiers and result parts subscription does not exist
    /// </para>
    /// </summary>
    public class Mid0154 : Mid, IMultipleIdentifier, IIntegrator, IUnsubscription, IAcceptableCommand, IDeclinableCommand
    {
        public const int MID = 154;

        public IEnumerable<Error> DocumentedPossibleErrors => new Error[] { };

        public Mid0154() : base(MID, DEFAULT_REVISION) 
        { 
        }

        public Mid0154(Header header) : base(header)
        {
        }
    }
}
