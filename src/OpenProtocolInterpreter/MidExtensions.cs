﻿using OpenProtocolInterpreter.Communication;
using System;
using System.Linq;

namespace OpenProtocolInterpreter
{
    /// <summary>
    /// Mid Extensions functions
    /// </summary>
    public static class MidExtensions
    {
        /// <summary>
        /// <see cref="Mid.Pack()"/> then concatenate NUL charactor to it`s end
        /// </summary>
        /// <param name="mid">Mid instance</param>
        /// <returns>Mid's package in string with NUL character</returns>
        public static string PackWithNul(this Mid mid)
        {
            if (mid == default)
            {
                return default;
            }

            var value = mid.Pack();
            return string.Concat(value, '\0');
        }

        /// <summary>
        /// <see cref="Mid.PackBytes()"/> then concatenate NUL charactor to it`s end
        /// </summary>
        /// <param name="mid">Mid instance</param>
        /// <returns>Mid's package in bytes with NUL character</returns>
        public static byte[] PackBytesWithNul(this Mid mid)
        {
            if (mid == default)
            {
                return default;
            }

            var bytes = mid.PackBytes();
            return bytes.Concat(new byte[] { 0x00 }).ToArray();
        }

        /// <summary>
        /// Generates reply <see cref="Mid"/> instance accordingly with a received mid.
        /// </summary>
        /// <typeparam name="TAckMid"><see cref="Mid"/> type of corresponding acknowledge</typeparam>
        /// <param name="instance"><see cref="Mid"/> instance implementing IAcknowledgeable</param>
        /// <returns>A new <see cref="Mid"/> instance of TMid type</returns>
        public static TAckMid GetAcknowledge<TAckMid>(this IAcknowledgeable<TAckMid> instance) where TAckMid : Mid, IAcknowledge, new()
        {
            var mid = new TAckMid();
            if(instance is Mid acknowledgeableMid)
                mid.Header.Revision = acknowledgeableMid.Header.Revision;

            return mid;
        }

        /// <summary>
        /// Generates reply <see cref="Mid"/> instance accordingly with a received mid.
        /// </summary>
        /// <typeparam name="TAnswerMid"><see cref="Mid"/> type that is the answer to respective Mid</typeparam>
        /// <returns>A new <see cref="Mid"/> instance</returns>
        public static TAnswerMid GetReply<TAnswerMid>(this IAnswerableBy<TAnswerMid> _) where TAnswerMid : Mid, new()
        {
            return new TAnswerMid();
        }

        /// <summary>
        /// Generates reply <see cref="Mid"/> instance accordingly with a received mid.
        /// </summary>
        /// <typeparam name="TAnswerMid"><see cref="Mid"/> type that is the answer to respective Mid</typeparam>
        /// <param name="revision">Desired reply revision</param>
        /// <returns>A new <see cref="Mid"/> instance</returns>
        public static TAnswerMid GetReply<TAnswerMid>(this IAnswerableBy<TAnswerMid> mid, int revision) where TAnswerMid : Mid, new()
        {
            var reply = mid.GetReply();
            reply.Header.Revision = revision;
            return reply;
        }

        /// <summary>
        /// Generates a Communication positive acknowledge mid (<see cref="Mid0005"/>) instance for the accepted mid.
        /// </summary>
        /// <typeparam name="TAcceptedMid"><see cref="Mid"/> instance and <see cref="IAcceptableCommand"/> implementer</typeparam>
        /// <param name="mid"></param>
        /// <returns>A new <see cref="Mid0005"/> instance</returns>
        public static Mid0005 GetAcceptCommand<TAcceptedMid>(this TAcceptedMid mid) where TAcceptedMid : Mid, IAcceptableCommand
        {
            return new Mid0005(mid.Header.Mid);
        }

        /// <summary>
        /// Generates a Communication Negative Acknowledge mid (<see cref="Mid0004"/>) instance for the failed mid and with the informed error code.
        /// </summary>
        /// <typeparam name="TDeclinedMid"><see cref="Mid"/> instance and <see cref="IDeclinableCommand"/> implementer</typeparam>
        /// <param name="mid">Current failed mid</param>
        /// <param name="error"><see cref="Mid0004"/> error code</param>
        /// <returns>A new <see cref="Mid0004"/> instance</returns>
        public static Mid0004 GetDeclineCommand<TDeclinedMid>(this TDeclinedMid mid, Error error) where TDeclinedMid : Mid, IDeclinableCommand
        {
            return new Mid0004(mid.Header.Mid) { ErrorCode = error };
        }

        /// <summary>
        /// Assert that error code is a possible error for the current failed mid and generates a Communication Negative Acknowledge mid (<see cref="Mid0004"/>) 
        /// instance for the failed mid and with the informed error code.
        /// </summary>
        /// <typeparam name="TDeclinedMid"><see cref="Mid"/> instance and <see cref="IDeclinableCommand"/> implementer</typeparam>
        /// <param name="mid">Current failed mid</param>
        /// <param name="error"><see cref="Mid0004"/> error code</param>
        /// <returns>A new <see cref="Mid0004"/> instance</returns>
        /// <exception cref="ArgumentException">Throws if error is not a possible error for the current failed Mid</exception>
        public static Mid0004 AssertAndGetDeclineCommand<TDeclinedMid>(this TDeclinedMid mid, Error error) where TDeclinedMid : Mid, IDeclinableCommand
        {
            if (!mid.DocumentedPossibleErrors.Contains(error))
            {
                throw new ArgumentException($"{error} is not a possible error for {mid.Header.Mid}");
            }

            return mid.GetDeclineCommand(error);
        }
    }
}
