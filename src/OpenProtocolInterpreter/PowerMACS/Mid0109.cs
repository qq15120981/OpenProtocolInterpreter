﻿using System.Collections.Generic;

namespace OpenProtocolInterpreter.PowerMACS
{
    /// <summary>
    /// Last Power MACS tightening result data unsubscribe
    /// <para>Reset the last Power MACS tightening result subscription for the rundowns result.</para>
    /// <para>Message sent by: Integrator</para>
    /// <para>
    ///     Answer: <see cref="Communication.Mid0005"/> Command accepted or 
    ///         <see cref="Communication.Mid0004"/> Command error, Subscription does not exist
    /// </para>
    /// </summary>
    public class Mid0109 : Mid, IPowerMACS, IIntegrator, IUnsubscription, IAcceptableCommand, IDeclinableCommand
    {
        public const int MID = 109;

        public IEnumerable<Error> DocumentedPossibleErrors => new Error[] { Error.SubscriptionDoesntExists };

        public Mid0109() : this(DEFAULT_REVISION)
        {

        }

        public Mid0109(int revision) : base(MID, revision) { }

        public Mid0109(Header header) : base(header)
        {
        }
    }
}
