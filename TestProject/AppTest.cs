using CalcProject.App;

namespace TestProject
{
    [TestClass]
    public class AppTest
    {
        private Resources Resources = new Resources();

        public AppTest()
        {
            RomanNumber.Resources = Resources;
        }

        [TestMethod]
        public void TestCalc()
        {
            CalcProject.App.Calc calc = new(Resources);
            // ���� � ����������� �������� - ��'��� ���� ��������
            Assert.IsNotNull(calc);
        }
        [TestMethod]
        public void RomanNumberParse()
        {
            Assert.AreEqual(RomanNumber.Parse("I"), 1, "I == 1");
            Assert.AreEqual(RomanNumber.Parse("IV"), 4, "IV == 4");
            Assert.AreEqual(RomanNumber.Parse("XV"), 15);
            Assert.AreEqual(RomanNumber.Parse("XXX"), 30);
            Assert.AreEqual(RomanNumber.Parse("CM"), 900);
            Assert.AreEqual(RomanNumber.Parse("MCMXCIX"), 1999);
            Assert.AreEqual(RomanNumber.Parse("CD"), 400);
            Assert.AreEqual(RomanNumber.Parse("CDI"), 401);
            Assert.AreEqual(RomanNumber.Parse("LV"), 55);
            Assert.AreEqual(RomanNumber.Parse("XL"), 40);
        }
        [TestMethod]
        public void RomanNumberParse1Digit()
        {
            Assert.AreEqual(0, RomanNumber.Parse("N"));  // Zero digit
            Assert.AreEqual(1, RomanNumber.Parse("I"));
            Assert.AreEqual(5, RomanNumber.Parse("V"));
            Assert.AreEqual(10, RomanNumber.Parse("X"));
            Assert.AreEqual(50, RomanNumber.Parse("L"));
            Assert.AreEqual(100, RomanNumber.Parse("C"));
            Assert.AreEqual(500, RomanNumber.Parse("D"));
            Assert.AreEqual(1000, RomanNumber.Parse("M"));
        }

        [TestMethod]
        public void RomanNumberParse2Digits()
        {
            Assert.AreEqual(2, RomanNumber.Parse("II"));
            Assert.AreEqual(4, RomanNumber.Parse("IV"));
            Assert.AreEqual(9, RomanNumber.Parse("IX"));
            Assert.AreEqual(15, RomanNumber.Parse("XV"));
            Assert.AreEqual(20, RomanNumber.Parse("XX"));
            Assert.AreEqual(40, RomanNumber.Parse("XL"));
            Assert.AreEqual(90, RomanNumber.Parse("XC"));
            Assert.AreEqual(110, RomanNumber.Parse("CX"));
            Assert.AreEqual(400, RomanNumber.Parse("CD"));
            Assert.AreEqual(900, RomanNumber.Parse("CM"));
            Assert.AreEqual(2000, RomanNumber.Parse("MM"));
        }

        [TestMethod]
        public void RomanNumberParse3MoreDigits()
        {
            Assert.AreEqual(3, RomanNumber.Parse("III"));
            Assert.AreEqual(12, RomanNumber.Parse("XII"));
            Assert.AreEqual(104, RomanNumber.Parse("CIV"));
            Assert.AreEqual(2001, RomanNumber.Parse("MMI"));
            Assert.AreEqual(13, RomanNumber.Parse("XIII"));
            Assert.AreEqual(1999, RomanNumber.Parse("MCMXCIX"));
            Assert.AreEqual(2022, RomanNumber.Parse("MMXXII"));
        }

        [TestMethod]
        public void RomanNumberParseInvalidDigit()
        {
            // RomanNumber.Parse("A") // ArgumentException "Invalid digit 'A'"
            /*
             ���������� ���������� ������, �� ������� ����������:
            ���� ����������, �� ������ Assert.SomeTest( Parse("A") ),
            ������ �� �����������, ������� ���������� ������� �� 
            ���� ���������� ���������, � ������� SomeTest �� ���� ����� ��.

            ��� ���������� ��������� ��������� �����:
            ������ ������ ����������� � ������������ � ����� ����������
             ����� "����������" ������� � ���������� �� ����������.
             */

            // �������� �� ������������, ������� ����������
            //  ����� "������" ���� (Exception != ArgumentException)
            // Assert.ThrowsException<Exception>( () => RomanNumber.Parse("A") );

            // �������� �� ������������, ������� ���� ����������
            // Assert.ThrowsException<ArgumentException>( () => RomanNumber.Parse("X") );
            
            var exc = // Assert.Throws ������� ����������, �� ���� � ��������
                Assert.ThrowsException<ArgumentException>(
                    () => RomanNumber.Parse("A")
                );
            // ��������� ������ ���������� �������� ��������� ���� �����������
            Assert.AreEqual(
                Resources.InvalidDigitMessage('A'),  // "Invalid digit 'A'", 
                exc.Message);

            // ��� ���������� ����� ������� ��� �����:
            Assert.AreEqual(
                Resources.InvalidDigitMessage('1'),  // "Invalid digit '1'",
                Assert.ThrowsException<ArgumentException>(
                    () => RomanNumber.Parse("1")
                ).Message
            );

            // �������� - ������� ����� (����������) ��� �������
            // 0 G � % @
        }

        [TestMethod]
        public void RomanNumberParseInvalidNumber()
        {
            // ���������� �����. �������� ����� -
            // ���������� ������� �� �������, � ����,
            // � �������, �������� ������ (� ��� �����)
            // "XIX1"  " XX" "X X"  "XX "  "cxx"  "hello"
            Assert.IsTrue(
                Assert.ThrowsException<ArgumentException>(
                    () => RomanNumber.Parse("3XI2X1")
                ).Message.StartsWith(Resources.InvalidDigitMessage('\0')[..12])
            );
            // Zero digit (N) could not be a part of number
            Assert.AreEqual(
                Resources.InvalidDigitMessage('N'),  // "Invalid digit 'N'",
                Assert.ThrowsException<ArgumentException>(
                    () => RomanNumber.Parse("XXN")
                ).Message
            );
            Assert.AreEqual(
                Resources.InvalidDigitMessage('N'),  // "Invalid digit 'N'",
                Assert.ThrowsException<ArgumentException>(
                    () => RomanNumber.Parse("XNX")
                ).Message
            );
            Assert.AreEqual(
                Resources.InvalidDigitMessage('N'),  // "Invalid digit 'N'",
                Assert.ThrowsException<ArgumentException>(
                    () => RomanNumber.Parse("NXX")
                ).Message
            );
        }

        [TestMethod]
        public void RomanNumberParseEmpty()
        {
            // ������� �� ������� Parse �� ������� �����
            // �� �� ������ �������� (null)
            // ��������� �������, ���� ����� �������� 0
            // ³�����:
            //  ���� - �� "N", ��� ����� ������� "N" � �����
            //   ����� �� ����������� ("N"+, "XN"-, "NX"-, "XNX"-)
            //  ������� ����� �� ��������  ArgumentException
            //   � ������������ "Empty string not allowed"
            //  �� ������ �������� (null) �� ���� ���� 
            //   ���������� ArgumentNullException ��� �����������

            Assert.ThrowsException<ArgumentException>(
                    () => RomanNumber.Parse(""));

            Assert.ThrowsException<ArgumentNullException>(
                    () => RomanNumber.Parse(null!));
        }

        [TestMethod]
        public void RomanNumberCtor()
        {
            RomanNumber number;
            number = new RomanNumber(10);
            Assert.IsNotNull(number);
            number = new RomanNumber(0);
            Assert.IsNotNull(number);
        }

        [TestMethod]
        public void RomanNumberToString()
        {
            RomanNumber number;
            number = new RomanNumber(10);
            Assert.AreEqual("X", number.ToString());

            number = new RomanNumber(0);
            Assert.AreEqual("N", number.ToString());

            Assert.AreEqual("XX", new RomanNumber(20).ToString());
            Assert.AreEqual("XL", new RomanNumber(40).ToString());
            Assert.AreEqual("XC", new RomanNumber(90).ToString());
            Assert.AreEqual("MCMXCIX", new RomanNumber(1999).ToString());
        }

        [TestMethod]
        public void RomanNumberParseToStringCrossTest()
        {
            // ��������: ������� ��������� ����� 0-2000
            // �� ������: n -> toString() -> Parse() == n
            RomanNumber number = new(0);
            for(int i = 0; i <= 2000; i++)
            {
                number.Value = i;
                Assert.AreEqual(i, RomanNumber.Parse(number.ToString()));
            }
        }

        [TestMethod]
        public void RomanNumberType()
        {
            /*
             �������� ��'���� ���� ���� Value �� Reference
             ��� ����� �� �������������, ��� �������� ���������
             */
            RomanNumber n1 = new(10);
            RomanNumber n2 = n1;
            n2.Value = 12;
            Assert.AreSame(n1, n2);  // AreSame - ��������� (��� ��������� �� ���� ��'���)
            // AreNotSame - ��� ���������
            RomanNumber n3 = n1 with { Value = 20 };  // with - ����������, ������ ��� record
            Assert.AreNotSame(n1, n3);    // ��� ��������� - ���� ����������
            RomanNumber n4 = n1 with {};  // ������ ���� (��� ��� �������)
            Assert.AreNotSame(n1, n4);    // ��������� ���
            Assert.AreEqual(n1, n4);      // ��� ��� �� ������
        }

        [TestMethod]
        public void RomanNumberNegatives()
        {
            Assert.AreEqual(-10, RomanNumber.Parse("-X"));
            Assert.AreEqual(-15, RomanNumber.Parse("-XV"));
            Assert.AreEqual(-9, RomanNumber.Parse("-IX"));

            Assert.ThrowsException<ArgumentException>(() => RomanNumber.Parse("-"));
            Assert.ThrowsException<ArgumentException>(() => RomanNumber.Parse("--D"));
            Assert.ThrowsException<ArgumentException>(() => RomanNumber.Parse("-X-D"));
            Assert.ThrowsException<ArgumentException>(() => RomanNumber.Parse("X-D"));
            Assert.ThrowsException<ArgumentException>(() => RomanNumber.Parse("XD-"));
            Assert.ThrowsException<ArgumentException>(() => RomanNumber.Parse("-N"));

            RomanNumber rn = new(-11);
            Assert.AreEqual("-XI", rn.ToString());
            rn.Value = -40;
            Assert.AreEqual("-XL", rn.ToString());
            rn.Value = -0;
            Assert.AreEqual("N", rn.ToString());
        }
    }

    [TestClass]
    public class OperationsTest
    {
        [TestMethod]
        public void AddTest()
        {
            RomanNumber rn2 = new(2);
            RomanNumber rn3 = new(3);
            RomanNumber rn5 = new(5);
            Assert.AreEqual(5, rn2.Add(rn3).Value);
            Assert.AreEqual(rn5, rn2.Add(rn3));
            // ��������: �������� ���������� (�����) ��� �������
            // - ���������� rn2.Add(rn3) -- "V"
            // - ���������� ������ � ����������� ����������
            //    � �������� ��������� / � �������� ������� / � ��'�����
            // - �� � ���� ��� 0/"N"/new()
            // - ��������� ������ � ��� ����� ��'����� : rn2.Add(rn2)
            // - � ���������� ��'�����  rn2.Add(new(3)) ; new(3).Add(rn2)
            // - ���������� ������ � null

            Assert.AreEqual("V", rn2.Add(rn3).ToString());

            RomanNumber rn_2 = new(-2);
            RomanNumber rn_3 = new(-3);
            RomanNumber rn_5 = new(-5);
            Assert.AreEqual(1, rn_2.Add(rn3).Value);
            Assert.AreEqual(rn_5, rn_2.Add(rn_3));
            Assert.AreEqual("-VIII", rn_5.Add(rn_3).ToString());

            RomanNumber rn0 = new(0);
            Assert.AreEqual(-2, rn_2.Add(rn0).Value);
            Assert.AreEqual(-3, rn0.Add(rn_3).Value);

            Assert.AreEqual(-4, rn_2.Add(rn_2).Value);

            Assert.AreEqual(0, rn_2.Add(new RomanNumber(2)).Value);
            Assert.AreEqual(rn5, new RomanNumber(3).Add(rn2));

            Assert.ThrowsException<ArgumentNullException>(() => rn2.Add((RomanNumber)null!));
        }

        [TestMethod]
        public void AddIntTest()
        {
            RomanNumber rn2 = new(2);
            RomanNumber rn3 = new(3);
            RomanNumber rn5 = new(5);
            Assert.AreEqual(rn3, rn2.Add(1));
            Assert.AreEqual(rn3, rn5.Add(-2));
            Assert.AreEqual(rn5, rn2.Add(3));
        }

        [TestMethod]
        public void AddStringTest()
        {
            RomanNumber n2 = new(2);
            RomanNumber n3 = new(3);
            RomanNumber n5 = new(5);
            RomanNumber n_5 = new(-5);
            RomanNumber n0 = new(0);
            Assert.AreEqual(n3, n2.Add("I"));
            Assert.AreEqual(n3, n5.Add("-II"));
            Assert.AreEqual(n5, n2.Add("III"));
            Assert.AreEqual(n_5, n_5.Add("N"));
            Assert.AreEqual(n5, n0.Add("V"));
            var m = Assert.ThrowsException<ArgumentNullException>(
                    () => n2.Add((null as String)!));

            // ������ ����� � ������������ ��������� ������� "XR"
        }

        [TestMethod]
        public void ComplexOperationsTest()
        {
            Calc calc = new(new Resources());

            if (!RomanNumber.Operations.ContainsKey("-"))
            {   // ����� �������� � ��������
                Assert.AreEqual(5, 
                    RomanNumber.Operations["-"](new(15), new(10)).Value);

                Assert.AreEqual(5,
                    calc.EvalExpression("XXIX - IV").Value);
            }
        }
    }
}
/*
 TDD - Test Driven Development - ����������� �������� �������
ϳ���� �� ������������� (����������� ��) ����� � ����
 ����� ��� ��'���� �'��������� �����, �� �� ���������.
 �� ����� ������� �� ����������� ������� ��������, ��������
 �� �����, �� ����� ������ ��'����.
������� �� �� - ��������� �� ���� ������������ �������,
 ������ ������� - ����������� �����
 */