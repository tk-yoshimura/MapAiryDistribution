﻿using MapAiryPadeApprox;
using MultiPrecision;
using MultiPrecisionIntegrate;

namespace MapAiryEvalExpected {
    internal class EvalEntropy {
        static void Main() {
            using StreamWriter sw = new("../../../../results/entropy_precision120.csv");

            static MultiPrecision<Pow2.N16> info(MultiPrecision<Pow2.N16> x) {
                MultiPrecision<Pow2.N16> px = PDFPadeN16.Value(x);

                if (px == 0) {
                    return 0;
                }

                return -px * MultiPrecision<Pow2.N16>.Log(px);
            };

            (MultiPrecision<Pow2.N16> nvalue, MultiPrecision<Pow2.N16> nerror, _) =
                GaussKronrodIntegral<Pow2.N16>.AdaptiveIntegrate(info, -20, 0,
                1e-120, GaussKronrodOrder.G32K65, discontinue_eval_points: 262144
            );

            Console.WriteLine($"{nvalue}\n{nerror:e20}");

            (MultiPrecision<Pow2.N16> pvalue, MultiPrecision<Pow2.N16> perror, _) =
                GaussKronrodIntegral<Pow2.N16>.AdaptiveIntegrate(info, 0, MultiPrecision<Pow2.N16>.PositiveInfinity,
                1e-120, GaussKronrodOrder.G32K65, discontinue_eval_points: 262144
            );

            Console.WriteLine($"{pvalue}\n{perror:e20}");

            MultiPrecision<Pow2.N16> value = nvalue + pvalue;
            MultiPrecision<Pow2.N16> error = nerror + perror;

            sw.WriteLine($"entropy,error");
            sw.WriteLine($"{value},{error:e20}");

            sw.Close();

            Console.WriteLine("END");
            Console.Read();
        }
    }
}
