﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace J4JSoftware.DependencyInjection;

public class DecoratedTypeTester<T> : ConstructorTesterBase<T>
    where T : class
{
    private readonly bool _allowInherited;
    private readonly Type? _requiredAttribute;

    public DecoratedTypeTester( bool allowInherited, Type requiredAttribute )
    {
        _allowInherited = allowInherited;

        _requiredAttribute = typeof(Attribute).IsAssignableFrom(requiredAttribute) ? requiredAttribute : null;
    }

    public override bool MeetsRequirements( Type toCheck )
    {
        if( !base.MeetsRequirements( toCheck ) || _requiredAttribute == null )
            return false;

        var typeAttributes = toCheck.GetCustomAttributes( _allowInherited );

        // ReSharper disable once UseMethodIsInstanceOfType
        return typeAttributes.Any( x => _requiredAttribute.IsAssignableFrom( x.GetType() ) );
    }
}
