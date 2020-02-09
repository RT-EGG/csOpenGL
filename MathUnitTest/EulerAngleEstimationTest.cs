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
        public void TestMain()
        {
            foreach (var angle in GetTestCases()) {
                Verify(angle);
            }
            return;
        }

        public void Verify(IROEulerAngle inAngle)
        {
            TMatrix44 rotmat = TMatrix44.MakeRotateMatrixYaw(inAngle.YawRad)
                             * TMatrix44.MakeRotateMatrixPitch(inAngle.PitchRad)
                             * TMatrix44.MakeRotateMatrixRoll(inAngle.RollRad);

            IROEulerAngle estimated = TEulerAngle.EstimateFrom(rotmat);
            TMatrix44 actual = TMatrix44.MakeRotateMatrixYaw(estimated.YawRad)
                             * TMatrix44.MakeRotateMatrixPitch(estimated.PitchRad)
                             * TMatrix44.MakeRotateMatrixRoll(estimated.RollRad);

            Assert.AreEqual(rotmat, actual, $"Yaw = { inAngle.YawDeg }, Pitch = { inAngle.PitchDeg }, Roll = { inAngle.RollDeg }", new object[] { inAngle.YawDeg, inAngle.PitchDeg, inAngle.RollDeg });
            return;
        }

        private IEnumerable<IROEulerAngle> GetTestCases()
        {
            IList<IROEulerAngle> result = new List<IROEulerAngle>();
            result.Add(TEulerAngle.CreateFromDegree(180.0, 0.0, 0.0));
            result.Add(TEulerAngle.CreateFromDegree(0.0, 180.0, 0.0));
            result.Add(TEulerAngle.CreateFromDegree(0.0, 0.0, 180.0));
            result.Add(TEulerAngle.CreateFromDegree(0.0, 90.0, 180.0));
            result.Add(TEulerAngle.CreateFromDegree(0.0, -90.0, 180.0));
            result.Add(TEulerAngle.CreateFromDegree(90.0, 0.0, 90.0));
            result.Add(TEulerAngle.CreateFromDegree(0.0, 90.0, 90.0));
            result.Add(TEulerAngle.CreateFromDegree(0.0, 90.0, -90.0));
            result.Add(TEulerAngle.CreateFromDegree(0.0, -90.0, -90.0));
            result.Add(TEulerAngle.CreateFromDegree(0.0, -90.0, -90.0));

            return result;
        }
        private const int RandomCaseCount = 10;
    }
}
