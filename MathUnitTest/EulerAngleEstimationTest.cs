using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using rtUtility.rtMath;

namespace MathUnitTest
{
    [TestClass]
    public class EulerAngleEstimationTest
    {
        [TestMethod]
        public void TestSpecifyCases()
        {
            foreach (var @case in EnumerateSpecifyTestCases()) {
                Verify(@case);
            }
            return;
        }

        [TestMethod]
        public void TestRandomCases()
        {
            Random randomizer = new Random();
            for (int i = 0; i < RandomCasesCount; ++i) {
                Verify(GenerateRandomAngle(randomizer));
            }
            return;
        }

        private void Verify(IROEulerAngle inValue)
        {
            TMatrix44 expected = TMatrix44.MakeRotateMatrixYaw(inValue.YawRad)
                               * TMatrix44.MakeRotateMatrixPitch(inValue.PitchRad)
                               * TMatrix44.MakeRotateMatrixRoll(inValue.RollRad);

            IROEulerAngle estimated = TEulerAngle.EstimateFrom(expected);
            TMatrix44 actual = TMatrix44.MakeRotateMatrixYaw(estimated.YawRad)
                             * TMatrix44.MakeRotateMatrixPitch(estimated.PitchRad)
                             * TMatrix44.MakeRotateMatrixRoll(estimated.RollRad);

            if (!expected.Equals(actual)) {
                Assert.Fail($"Y={ inValue.YawDeg }; X={ inValue.PitchDeg }; Z={ inValue.RollDeg }");
            }
            return;
        }

        private IROEulerAngle GenerateRandomAngle(Random inGenerator)
        {
            return new TEulerAngle(inGenerator.NextDouble() * 360.0 - 180.0,
                                   inGenerator.NextDouble() * 360.0 - 180.0,
                                   inGenerator.NextDouble() * 360.0 - 180.0);
        }

        private IEnumerable<IROEulerAngle> EnumerateSpecifyTestCases()
        {
            IList<IROEulerAngle> result = new List<IROEulerAngle>();
            result.Add(TEulerAngle.FromDegrees(0.0, 0.0, 0.0));
            result.Add(TEulerAngle.FromDegrees(180.0, 0.0, 0.0));
            result.Add(TEulerAngle.FromDegrees(0.0, 180.0, 0.0));
            result.Add(TEulerAngle.FromDegrees(0.0, 0.0, 180.0));
            result.Add(TEulerAngle.FromDegrees(0.0, 90.0, 0.0));
            result.Add(TEulerAngle.FromDegrees(0.0, -90.0, 0.0));
            result.Add(TEulerAngle.FromDegrees(0.0, 90.0, 180.0));
            result.Add(TEulerAngle.FromDegrees(0.0, -90.0, -180.0));
            result.Add(TEulerAngle.FromDegrees(0.0, 90.0, 90.0));
            result.Add(TEulerAngle.FromDegrees(0.0, 90.0, -90.0));
            result.Add(TEulerAngle.FromDegrees(0.0, -90.0, 90.0));
            result.Add(TEulerAngle.FromDegrees(0.0, -90.0, -90.0));

            return result;
        }

        private const int RandomCasesCount = 1000;
    }
}
