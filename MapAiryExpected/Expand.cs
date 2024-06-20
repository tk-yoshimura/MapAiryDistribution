using MultiPrecision;

namespace MapAiryExpected {
    public struct N24 : IConstant {
        public readonly int Value => 24;
    }

    public struct N48 : IConstant {
        public readonly int Value => 48;
    }

    public struct N80 : IConstant {
        public readonly int Value => 80;
    }

    public struct N96 : IConstant {
        public readonly int Value => 96;
    }

    public struct N112 : IConstant {
        public readonly int Value => 112;
    }

    internal struct Plus1<N> : IConstant where N : struct, IConstant {
        public readonly int Value => checked(default(N).Value + 1);
    }

    internal struct Plus4<N> : IConstant where N : struct, IConstant {
        public readonly int Value => checked(default(N).Value + 4);
    }
}
