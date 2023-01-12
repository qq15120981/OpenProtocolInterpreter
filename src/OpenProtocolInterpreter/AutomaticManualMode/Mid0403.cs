﻿using System.Collections.Generic;

namespace OpenProtocolInterpreter.AutomaticManualMode
{
    /// <summary>
    /// Automatic/Manual mode unsubscribe
    /// <para>Reset the subscription for the automatic/manual mode.</para>
    /// <para>Message sent by: Integrator</para>
    /// <para>Answer: <see cref="Communication.Mid0005"/> Command accepted or <see cref="Communication.Mid0004"/> Command error, Automatic/Manual mode subscribe does not exist</para>
    /// </summary>
    public class Mid0403 : Mid, IAutomaticManualMode, IIntegrator, IUnsubscription, IAcceptableCommand, IDeclinableCommand
    {
        private const int LAST_REVISION = 1;
        public const int MID = 403;

        public IEnumerable<Error> DocumentedPossibleErrors => new Error[] { Error.AUTOMATIC_MANUAL_MODE_SUBSCRIBE_DOESNT_EXISTS };


        public Mid0403() : base(MID, LAST_REVISION) { }

        public Mid0403(Header header) : base(header)
        {

        }
    }
}
