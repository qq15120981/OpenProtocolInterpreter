﻿namespace OpenProtocolInterpreter.MotorTuning
{
    /// <summary>
    /// Motor tuning result data acknowledge
    /// <para>Acknowledgement of motor tuning result data.</para>
    /// <para>Message sent by: Integrator</para>
    /// <para>Answer: None</para>
    /// </summary>
    public class Mid0502 : Mid, IMotorTuning, IIntegrator, IAcknowledge
    {
        public const int MID = 502;

        public Mid0502() : base(MID, DEFAULT_REVISION) { }

        public Mid0502(Header header) : base(header)
        {
        }
    }
}
