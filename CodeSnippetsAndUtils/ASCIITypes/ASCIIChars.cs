using System;
using System.Collections.Generic;
using System.Text;

namespace CodeSnippetsAndUtils.ASCIITypes
{
    /// <summary>
    /// Provides static readonly fields that each represent a valid ASCII character for ease of use.
    /// </summary>
    public struct ASCIIChars
    {
        #region Control Characters
        public static readonly ASCIIChar Null = new ASCIIChar(0);
        public static readonly ASCIIChar StartOfHeading = new ASCIIChar(1);
        public static readonly ASCIIChar StartOfText = new ASCIIChar(2);
        public static readonly ASCIIChar EndOfText = new ASCIIChar(3);
        public static readonly ASCIIChar EndOfTransmission = new ASCIIChar(4);
        public static readonly ASCIIChar Enquiry = new ASCIIChar(5);
        public static readonly ASCIIChar Acknowledgement = new ASCIIChar(6);
        public static readonly ASCIIChar Bell = new ASCIIChar(7);
        public static readonly ASCIIChar Backspace = new ASCIIChar(8);
        public static readonly ASCIIChar HorizontalTab = new ASCIIChar(9);
        public static readonly ASCIIChar LineFeed = new ASCIIChar(10);
        public static readonly ASCIIChar VerticalTab = new ASCIIChar(11);
        public static readonly ASCIIChar FormFeed = new ASCIIChar(12);
        public static readonly ASCIIChar CarriageReturn = new ASCIIChar(13);
        public static readonly ASCIIChar ShiftOut = new ASCIIChar(14);
        public static readonly ASCIIChar ShiftIn = new ASCIIChar(15);
        public static readonly ASCIIChar DataLinkEscape = new ASCIIChar(16);
        public static readonly ASCIIChar DeviceControl1 = new ASCIIChar(17);
        public static readonly ASCIIChar DeviceControl2 = new ASCIIChar(18);
        public static readonly ASCIIChar DeviceControl3 = new ASCIIChar(19);
        public static readonly ASCIIChar DeviceControl4 = new ASCIIChar(20);
        public static readonly ASCIIChar NegativeAcknowledgement = new ASCIIChar(21);
        public static readonly ASCIIChar SynchronusIdle = new ASCIIChar(22);
        public static readonly ASCIIChar EndOfTransmissionBlock = new ASCIIChar(23);
        public static readonly ASCIIChar Cancel = new ASCIIChar(24);
        public static readonly ASCIIChar EndOfMedium = new ASCIIChar(25);
        public static readonly ASCIIChar Substitute = new ASCIIChar(26);
        public static readonly ASCIIChar Escape = new ASCIIChar(27);
        public static readonly ASCIIChar FileSeparator = new ASCIIChar(28);
        public static readonly ASCIIChar GroupSeparator = new ASCIIChar(29);
        public static readonly ASCIIChar RecordSeparator = new ASCIIChar(30);
        public static readonly ASCIIChar UnitSeparator = new ASCIIChar(31);
        public static readonly ASCIIChar Delete = new ASCIIChar(127);
        #endregion

        #region Printable Characters
        public static readonly ASCIIChar Space = new ASCIIChar(32);
        public static readonly ASCIIChar ExclamationMark = new ASCIIChar(33);
        public static readonly ASCIIChar QuotationMark = new ASCIIChar(34);
        public static readonly ASCIIChar NumberSign = new ASCIIChar(35);
        public static readonly ASCIIChar DollarSign = new ASCIIChar(36);
        public static readonly ASCIIChar PercentSign = new ASCIIChar(37);
        public static readonly ASCIIChar Ampersand = new ASCIIChar(38);
        public static readonly ASCIIChar Apostrophe = new ASCIIChar(39);
        public static readonly ASCIIChar LeftParenthesis = new ASCIIChar(40);
        public static readonly ASCIIChar RightParenthesis = new ASCIIChar(41);
        public static readonly ASCIIChar Asterisk = new ASCIIChar(42);
        public static readonly ASCIIChar PlusSign = new ASCIIChar(43);
        public static readonly ASCIIChar Comma = new ASCIIChar(44);
        public static readonly ASCIIChar HyphenMinus = new ASCIIChar(45);
        public static readonly ASCIIChar Period = new ASCIIChar(46);
        public static readonly ASCIIChar ForwardSlash = new ASCIIChar(47);
        public static readonly ASCIIChar Zero = new ASCIIChar(48);
        public static readonly ASCIIChar One = new ASCIIChar(49);
        public static readonly ASCIIChar Two = new ASCIIChar(50);
        public static readonly ASCIIChar Three = new ASCIIChar(51);
        public static readonly ASCIIChar Four = new ASCIIChar(52);
        public static readonly ASCIIChar Five = new ASCIIChar(53);
        public static readonly ASCIIChar Six = new ASCIIChar(54);
        public static readonly ASCIIChar Seven = new ASCIIChar(55);
        public static readonly ASCIIChar Eight = new ASCIIChar(56);
        public static readonly ASCIIChar Nine = new ASCIIChar(57);
        public static readonly ASCIIChar Colon = new ASCIIChar(58);
        public static readonly ASCIIChar Semicolon = new ASCIIChar(59);
        public static readonly ASCIIChar LessThanSign = new ASCIIChar(60);
        public static readonly ASCIIChar EqualsSign = new ASCIIChar(61);
        public static readonly ASCIIChar GreaterThanSign = new ASCIIChar(62);
        public static readonly ASCIIChar QuestionMark = new ASCIIChar(63);
        public static readonly ASCIIChar AtSign = new ASCIIChar(64);
        public static readonly ASCIIChar A = new ASCIIChar(65);
        public static readonly ASCIIChar B = new ASCIIChar(66);
        public static readonly ASCIIChar C = new ASCIIChar(67);
        public static readonly ASCIIChar D = new ASCIIChar(68);
        public static readonly ASCIIChar E = new ASCIIChar(69);
        public static readonly ASCIIChar F = new ASCIIChar(70);
        public static readonly ASCIIChar G = new ASCIIChar(71);
        public static readonly ASCIIChar H = new ASCIIChar(72);
        public static readonly ASCIIChar I = new ASCIIChar(73);
        public static readonly ASCIIChar J = new ASCIIChar(74);
        public static readonly ASCIIChar K = new ASCIIChar(75);
        public static readonly ASCIIChar L = new ASCIIChar(76);
        public static readonly ASCIIChar M = new ASCIIChar(77);
        public static readonly ASCIIChar N = new ASCIIChar(78);
        public static readonly ASCIIChar O = new ASCIIChar(79);
        public static readonly ASCIIChar P = new ASCIIChar(80);
        public static readonly ASCIIChar Q = new ASCIIChar(81);
        public static readonly ASCIIChar R = new ASCIIChar(82);
        public static readonly ASCIIChar S = new ASCIIChar(83);
        public static readonly ASCIIChar T = new ASCIIChar(84);
        public static readonly ASCIIChar U = new ASCIIChar(85);
        public static readonly ASCIIChar V = new ASCIIChar(86);
        public static readonly ASCIIChar W = new ASCIIChar(87);
        public static readonly ASCIIChar X = new ASCIIChar(88);
        public static readonly ASCIIChar Y = new ASCIIChar(89);
        public static readonly ASCIIChar Z = new ASCIIChar(90);
        public static readonly ASCIIChar LeftSquareBracket = new ASCIIChar(91);
        public static readonly ASCIIChar Backslash = new ASCIIChar(92);
        public static readonly ASCIIChar RightSquareBracket = new ASCIIChar(93);
        public static readonly ASCIIChar Caret = new ASCIIChar(94);
        public static readonly ASCIIChar Underscore = new ASCIIChar(95);
        public static readonly ASCIIChar GraveAccent = new ASCIIChar(96);
        public static readonly ASCIIChar a = new ASCIIChar(97);
        public static readonly ASCIIChar b = new ASCIIChar(98);
        public static readonly ASCIIChar c = new ASCIIChar(99);
        public static readonly ASCIIChar d = new ASCIIChar(100);
        public static readonly ASCIIChar e = new ASCIIChar(101);
        public static readonly ASCIIChar f = new ASCIIChar(102);
        public static readonly ASCIIChar g = new ASCIIChar(103);
        public static readonly ASCIIChar h = new ASCIIChar(104);
        public static readonly ASCIIChar i = new ASCIIChar(105);
        public static readonly ASCIIChar j = new ASCIIChar(106);
        public static readonly ASCIIChar k = new ASCIIChar(107);
        public static readonly ASCIIChar l = new ASCIIChar(108);
        public static readonly ASCIIChar m = new ASCIIChar(109);
        public static readonly ASCIIChar n = new ASCIIChar(110);
        public static readonly ASCIIChar o = new ASCIIChar(111);
        public static readonly ASCIIChar p = new ASCIIChar(112);
        public static readonly ASCIIChar q = new ASCIIChar(113);
        public static readonly ASCIIChar r = new ASCIIChar(114);
        public static readonly ASCIIChar s = new ASCIIChar(115);
        public static readonly ASCIIChar t = new ASCIIChar(116);
        public static readonly ASCIIChar u = new ASCIIChar(117);
        public static readonly ASCIIChar v = new ASCIIChar(118);
        public static readonly ASCIIChar w = new ASCIIChar(119);
        public static readonly ASCIIChar x = new ASCIIChar(120);
        public static readonly ASCIIChar y = new ASCIIChar(121);
        public static readonly ASCIIChar z = new ASCIIChar(122);
        public static readonly ASCIIChar LeftCurlyBracket = new ASCIIChar(123);
        public static readonly ASCIIChar VerticalBar = new ASCIIChar(124);
        public static readonly ASCIIChar RightCurlyBracket = new ASCIIChar(125);
        public static readonly ASCIIChar Tilde = new ASCIIChar(126);
        #endregion
    }
}
