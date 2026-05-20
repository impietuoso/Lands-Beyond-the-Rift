using System;
using UnityEngine;

namespace TurnBasedRPG
{
    public class TypeDropdownAttribute : PropertyAttribute {
        public Type type;

        public TypeDropdownAttribute(Type type) {
            this.type = type;
        }
    }
}
