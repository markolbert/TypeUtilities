﻿#region copyright

// Copyright (c) 2021, 2022, 2023 Mark A. Olbert 
// https://www.JumpForJoySoftware.com
// DecoratedTypeTester.cs
//
// This file is part of JumpForJoy Software's TypeUtilities.
// 
// TypeUtilities is free software: you can redistribute it and/or modify it 
// under the terms of the GNU General Public License as published by the 
// Free Software Foundation, either version 3 of the License, or 
// (at your option) any later version.
// 
// TypeUtilities is distributed in the hope that it will be useful, but 
// WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY 
// or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License 
// for more details.
// 
// You should have received a copy of the GNU General Public License along 
// with TypeUtilities. If not, see <https://www.gnu.org/licenses/>.

#endregion

using System;
using System.Linq;

namespace J4JSoftware.DependencyInjection;

public class DecoratedTypeTester<T>( bool allowInherited, Type requiredAttribute ) : ITypeTester
    where T : class
{
    private readonly Type? _requiredAttribute =
        typeof( Attribute ).IsAssignableFrom( requiredAttribute ) ? requiredAttribute : null;

    public bool MeetsRequirements( Type toCheck )
    {
        if( _requiredAttribute == null )
            return false;

        var typeAttributes = toCheck.GetCustomAttributes( allowInherited );

        // ReSharper disable once UseMethodIsInstanceOfType
        return typeAttributes.Any( x => _requiredAttribute.IsAssignableFrom( x.GetType() ) );
    }
}
