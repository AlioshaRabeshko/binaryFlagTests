using System;
using Xunit;
using IIG.BinaryFlag;

namespace BinaryFlagTests {
    public class BinaryFlagTests {

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
        public void ResetFlagTest(ulong length) {
            var binaryFlag = new MultipleBinaryFlag(length);
            for (ulong i = 0; i < length; i++) {
                binaryFlag.ResetFlag(i);
                Assert.False(binaryFlag.GetFlag());
            }
        }

        [Theory]
        [InlineData(2)]
        [InlineData(8345)]
        public void SetFlagTrueTest(ulong length) {
            var binaryFlag = new MultipleBinaryFlag(length, false);
            for (ulong i = 0; i < length; i++) {
                Assert.False(binaryFlag.GetFlag());
                binaryFlag.SetFlag(i); 
            }
            Assert.True(binaryFlag.GetFlag());
        }

        [Theory]
        [InlineData(5, "TFTFF")]
        [InlineData(8, "TFTFFFFF")]
        public void SetFlagRangeTest(ulong length, string expected) {
            var binaryFlag = new MultipleBinaryFlag(length, false);
            binaryFlag.SetFlag(0);
            binaryFlag.SetFlag(2);
            Assert.Equal(binaryFlag.ToString(), expected);
        }

        [Theory]
        [InlineData(5, 2, false)]
        [InlineData(8, 2, true)]
        public void GetFlagTest(ulong length, ulong position, bool value) {
            // Òוסע ןונוג³נÿ÷ ÿךשמ מהטם ח פכאד³ג םו סעמ¿עü, עמ ןמגונעא÷ false
            // ‗ךשמ גס³ פכאדט סעמÿעü - true
            var binaryFlag = new MultipleBinaryFlag(length, true);
            if (value) {
                Assert.True(binaryFlag.GetFlag());
            } else {
                binaryFlag.ResetFlag(position);
                Assert.False(binaryFlag.GetFlag());
            }
        }

        [Theory]
        [InlineData(47, 48)]
        [InlineData(8888888, 8888889)]
        public void ResetFlagOutOfRangeTest(ulong length, ulong position) {
            var binaryFlag = new MultipleBinaryFlag(length);
            Assert.Throws<ArgumentOutOfRangeException>(() => binaryFlag.ResetFlag(position));
        }

        [Theory]
        [InlineData(47, 48)]
        [InlineData(8888888, 8888889)]
        public void SetFlagOutOfRangeTest(ulong length, ulong position) {
            var binaryFlag = new MultipleBinaryFlag(length);
            Assert.Throws<ArgumentOutOfRangeException>(() => binaryFlag.SetFlag(position));
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
