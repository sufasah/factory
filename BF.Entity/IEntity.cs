using System.Collections.Generic;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System;

namespace BF.Entity
{
    public abstract class IEntity
    {
        public int Id { get; set; }
        public int GetId(){
            return Id;
        }
        public bool PublicPropertiesEqual(IQueryCollection which) 
        {
            var type = this.GetType();
            var keys = which.Keys;
            foreach (PropertyInfo pi in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if(keys.Contains(pi.Name)){
                    string selfValue = type.GetProperty(pi.Name).GetValue(this, null).ToString();
                    string whichValue = which[pi.Name].First();
                    if (selfValue != whichValue && (selfValue == null || !string.Equals(selfValue,whichValue,StringComparison.CurrentCultureIgnoreCase)))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}