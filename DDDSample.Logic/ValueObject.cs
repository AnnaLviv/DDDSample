namespace DDDSample.Logic
{
    public abstract class ValueObject<T> where T: ValueObject<T>
    {
        public override bool Equals(object obj)
        {
            var valueObject = obj as T;
            if (ReferenceEquals(valueObject, null))
                return false;
            return EqualsCore(valueObject);
        }

        protected abstract bool EqualsCore(T valueObject);

        public override int GetHashCode()
        {
            return GetHashCodeCore();
        }

        protected abstract int GetHashCodeCore();

        public static bool operator ==(ValueObject<T> valueObjectOne, ValueObject<T> valueObjectTwo)
        {
            if (ReferenceEquals(valueObjectOne, null) && ReferenceEquals(valueObjectTwo, null))
                return true;

            if (ReferenceEquals(valueObjectOne, null) || ReferenceEquals(valueObjectTwo, null))
                return true;

            return valueObjectOne.Equals(valueObjectTwo);
        }

        public static bool operator !=(ValueObject<T> valueObjectOne, ValueObject<T> valueObjectTwo)
        {
            return !(valueObjectOne == valueObjectTwo);
        }
    }
}
