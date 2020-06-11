﻿using System;
using System.Collections.Generic;
using OpenMod.API.Commands;

namespace OpenMod.Core.Commands
{
    public static class CommandRegistrationHelper
    {
        public static List<ICommandRegistration> GetChildren(ICommandRegistration registration, ICollection<ICommandRegistration> commandRegistrations)
        {
            var children = new List<ICommandRegistration>();
            foreach (var commandRegistration in commandRegistrations)
            {
                if (commandRegistration.ParentId != null && commandRegistration.ParentId.Equals(registration.Id, StringComparison.OrdinalIgnoreCase))
                {
                    children.Add(commandRegistration);
                }
            }

            return children;
        }
    }
}