﻿using System.Collections.Generic;

namespace OpenProtocolInterpreter.ParameterSet
{
    /// <summary>
    /// Lock at batch done subscribe
    /// <para>A subscription for the Lock at batch done relay status.</para>
    /// <para>Message sent by: Integrator</para>
    /// <para>
    ///     Answer: <see cref="Communication.Mid0005"/> Command accepted or 
    ///     <see cref="Communication.Mid0004"/> Command error
    /// </para>
    /// <para>Message: <see cref="Mid0022"/> relay status immediately after <see cref="Communication.Mid0005"/> Command accepted</para>
    /// </summary>
    public class Mid0021 : Mid, IParameterSet, IIntegrator, ISubscription, IAcceptableCommand, IDeclinableCommand, IAnswerableBy<Mid0022>
    {
        public const int MID = 21;

        public IEnumerable<Error> DocumentedPossibleErrors => new Error[] { Error.LockAtBatchDoneSubscriptionAlreadyExists };

        public Mid0021() : this(false)
        {

        }

        public Mid0021(bool noAckFlag = false) : base(MID, DEFAULT_REVISION, noAckFlag) { }

        public Mid0021(Header header) : base(header)
        {
        }
    }
}
