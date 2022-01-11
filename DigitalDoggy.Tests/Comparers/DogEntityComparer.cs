using DigitalDoggy.Domain.Entities;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace DigitalDoggy.Tests.Comparers
{
    class DogEntityComparer : IEqualityComparer<DogEntity>
    {
        public bool Equals(DogEntity x, DogEntity y)
        {
            return x.Name.Equals(y.Name)
                && x.Color.Equals(y.Color)
                && x.TailLength.Equals(y.TailLength)
                && x.Weight.Equals(y.Weight);
        }

        public int GetHashCode([DisallowNull] DogEntity obj)
        {
            return obj.GetHashCode();
        }
    }
}
