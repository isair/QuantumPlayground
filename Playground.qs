namespace Quantum.Playground {
    open Microsoft.Quantum.Intrinsic;
    open Microsoft.Quantum.Canon;

    operation Set (desired: Result, q1: Qubit) : Unit {
        if (desired != M(q1)) {
            X(q1);
        }
    }

    // Playground main operation.
    // Returns an int array along with its size.
    operation PlaygroundMain () : (Int, Int[]) {
        mutable results = [0, 0];
        using ((q0, q1) = (Qubit(), Qubit())) {
            H(q1);

            CNOT(q1, q0);

            let v0 = M(q0);
            let v1 = M(q1);

            if (v0 == One) {
                set results w/= 0 <- 1;
            }
            if (v1 == One) {
                set results w/= 1 <- 1;
            }

            Set(Zero, q0);
            Set(Zero, q1);
        }
        return (2, results);
    }
}
