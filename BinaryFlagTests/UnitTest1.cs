using System;
using Xunit;
using IIG.BinaryFlag;

namespace BinaryFlagTests {
    public class BinaryFlag {

        [Theory]
        [InlineData(2)]
        [InlineData(420)]
        [InlineData(17179868704)]
        public void InitTest(ulong length) {
            var binaryFlag = new MultipleBinaryFlag(length);
            Assert.True(binaryFlag.GetFlag());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(17179868705)]
        [InlineData(171798687050)]
        public void InitOutOfRangeTest(ulong length) {
            Assert.Throws<ArgumentOutOfRangeException>(() => new MultipleBinaryFlag(length));
        }

        [Theory]
        [InlineData(15, true)]
        [InlineData(228, false)]
        public void InitWithValueTest(ulong length, bool value) {
            var binaryFlag = new MultipleBinaryFlag(length, value);
            Assert.Equal(value, binaryFlag.GetFlag());
        }

        [Theory]
        [InlineData(2)]
        [InlineData(8345)]
        public void SetFalseTest(ulong length) {
            var binaryFlag = new MultipleBinaryFlag(length);
            for (ulong i = 0; i < length; i++) {
                binaryFlag.ResetFlag(i);
                Assert.False(binaryFlag.GetFlag());
            }
        }

        [Theory]
        [InlineData(2)]
        [InlineData(8345)]
        public void SetTrueTest(ulong length) {
            var binaryFlag = new MultipleBinaryFlag(length, false);
            for (ulong i = 0; i < length; i++) {
                Assert.False(binaryFlag.GetFlag());
                binaryFlag.SetFlag(i); 
            }
            Assert.True(binaryFlag.GetFlag());
        }

        [Theory]
        [InlineData(47)]
        [InlineData(8888888)]
        public void ResetOutOfRangeTest(ulong length) {
            var binaryFlag = new MultipleBinaryFlag(length);
            Assert.Throws<ArgumentOutOfRangeException>(() => binaryFlag.ResetFlag(length + 1));
        }

        [Theory]
        [InlineData(47)]
        [InlineData(8888888)]
        public void SetOutOfRangeTest(ulong length) {
            var binaryFlag = new MultipleBinaryFlag(length);
            Assert.Throws<ArgumentOutOfRangeException>(() => binaryFlag.SetFlag(length + 1));
        }

        [Theory]
        [InlineData(10, true)]
        [InlineData(10, false)]
        public void ToStringTest(ulong length, bool value) {
            var binaryFlag = new MultipleBinaryFlag(length, value);
            string expected = "";
            for (ulong i = 0; i < length; i++) {
                if (value) expected = expected + "T";
                else expected = expected + "F";
            }
            Assert.Equal(expected, binaryFlag.ToString());
        }
    }
}
