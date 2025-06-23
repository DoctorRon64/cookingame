namespace Interfaces {
    public interface IHasProgress {
        public Signal<ProgressNormalize> OnProgressChanged { get; }
        public struct ProgressNormalize {
            public float ProgressNormalizeFloat;
        }
    }
}