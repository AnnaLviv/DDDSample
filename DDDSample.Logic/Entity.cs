namespace DDDSample.Logic
{
    /// <summary>
    /// We do not use IEquatable here, because it was designed to be used 
    /// in value types(structs), reference types(classes).
    /// It does not provide any additional value in classes.
    /// If we use it in classes - that violates YAGNI principle
    /// </summary>
    public abstract class Entity
    {
        public long Id { get; private set; }
        public override bool Equals(object obj)
        {
            var other = obj as Entity;
            if (ReferenceEquals(other, null))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (GetType() != other.GetType())
                return false;

            if (Id == 0 || other.Id == 0)
                return false;

            return Id == other.Id;
        }

        public static bool operator ==(Entity entityOne, Entity entityTwo)
        {
            if (ReferenceEquals(entityOne, null) && ReferenceEquals(entityTwo, null))
                return true;

            if (ReferenceEquals(entityOne, null) || ReferenceEquals(entityTwo, null))
                return true;

            return entityOne.Equals(entityTwo);
        }

        public static bool operator !=(Entity entityOne, Entity entityTwo)
        {
            return !(entityOne == entityTwo);
        }

        public override int GetHashCode()
        {
            return (GetType().ToString() + Id).GetHashCode();
        }

    }
}
