namespace Scripts.Services
{
     public class EventValue<Type>
    {
        private Type _value;
        private Type _oldValue;
        public event ValueHandler onChanged;

        public delegate void ValueHandler(Type newValue, Type oldValue);

        public EventValue(Type value)
        {
            _value = value;
        }

        public Type Value
        {
            get => _value;
            set
            {
                _value = value;
                onChanged?.Invoke(value, _oldValue);
                _oldValue = value;
            }
        }
    }
}