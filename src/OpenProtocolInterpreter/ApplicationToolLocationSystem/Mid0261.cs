﻿using System.Collections.Generic;

namespace OpenProtocolInterpreter.ApplicationToolLocationSystem
{
    /// <summary>
    /// Tool tag ID subscribe
    /// <para>Used by the integrator to order a Tool tag ID subscription from the controller.</para>
    /// <para>Message sent by: Controller</para>
    /// <para>Answer: <see cref="Mid0262"/> Tool tag ID or <see cref="Communication.Mid0004"/> Command error, Tool tag ID unknown, Tool tag ID subscription already exist or MID revision unsupported.</para>
    /// </summary>
    public class Mid0261 : Mid, IApplicationToolLocationSystem, IController, ISubscription, IAnswerableBy<Mid0262>, IDeclinableCommand
    {
        public const int MID = 261;

        public IEnumerable<Error> DocumentedPossibleErrors => new Error[] 
        { 
            Error.ToolTagIdUnknown, 
            Error.SubscriptionAlreadyExists, 
            Error.MidRevisionUnsupported 
        };

        public Mid0261() : this(false)
        {

        }

        public Mid0261(bool noAckFlag = false) : this(new Header()
        {
            Mid = MID, 
            Revision = DEFAULT_REVISION, 
            NoAckFlag = noAckFlag
        }) 
        { 
        }

        public Mid0261(Header header) : base(header)
        {

        }
    }
}
