﻿using OpenMod.API.Commands;
using SDG.Unturned;
using System;
using System.Threading.Tasks;

namespace OpenMod.Unturned.Locations
{
    public class UnturnedLocationCommandParameterResolveProvider : ICommandParameterResolveProvider
    {
        private readonly IUnturnedLocationDirectory m_LocationDirectory;

        public UnturnedLocationCommandParameterResolveProvider(IUnturnedLocationDirectory locationDirectory)
        {
            m_LocationDirectory = locationDirectory;
        }

        public bool Supports(Type type)
        {
            return type == typeof(UnturnedLocation) || type == typeof(LocationNode) || type == typeof(LocationDevkitNode);
        }

        public Task<object?> ResolveAsync(Type type, string input)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (string.IsNullOrEmpty(input))
            {
                return Task.FromResult((object?) null);
            }

            if (!Supports(type))
            {
                throw new ArgumentException("The given type is not supported", nameof(type));
            }

            var location = m_LocationDirectory.FindLocation(input, exact: false);

            if (type == typeof(UnturnedLocation))
            {
                return Task.FromResult<object?>(location);
            }
            else if (type == typeof(LocationNode))
            {
                return Task.FromResult<object?>(location?.Node);
            }

            return Task.FromResult<object?>(location?.DevkitNode);
        }
    }
}
